﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using PostSharp.Patterns.Contracts;
using ThreatsManager.Engine.Config;
using ThreatsManager.Interfaces;
using ThreatsManager.Interfaces.Extensions;
using ThreatsManager.Interfaces.Extensions.Actions;
using ThreatsManager.Interfaces.Extensions.Panels;
using ThreatsManager.Interfaces.ObjectModel;
using ThreatsManager.Interfaces.ObjectModel.Entities;
using ThreatsManager.Utilities;
using ThreatsManager.Utilities.Aspects;

namespace ThreatsManager.Engine
{
    public partial class Manager : IDesktopAlertAwareExtension
    {
        private static readonly Manager _instance = new Manager();

        #region Event management.
        private Action<string> _showMessage;
        public event Action<string> ShowMessage
        {
            add
            {
                if (_showMessage == null || !_showMessage.GetInvocationList().Contains(value))
                {
                    _showMessage += value;
                }
            }
            remove
            {
                _showMessage -= value;
            }
        }

        private Action<string> _showWarning;
        public event Action<string> ShowWarning
        {
            add
            {
                if (_showWarning == null || !_showWarning.GetInvocationList().Contains(value))
                {
                    _showWarning += value;
                }
            }
            remove
            {
                _showWarning -= value;
            }
        }

        private Action<IPanelFactory, IIdentity> _panelForEntityRequired;
        public event Action<IPanelFactory, IIdentity> PanelForEntityRequired
        {
            add
            {
                if (_panelForEntityRequired == null || !_panelForEntityRequired.GetInvocationList().Contains(value))
                {
                    _panelForEntityRequired += value;
                }
            }
            remove
            {
                _panelForEntityRequired -= value;
            }
        }
        #endregion

        private Manager()
        {
        }

        #region Extensions loading.
        static Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly result = null;

            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var fullyQualifiedName = new AssemblyName(args.Name);
                var existing = assemblies.FirstOrDefault(x =>
                    string.CompareOrdinal(x.GetName().Name, fullyQualifiedName.Name) == 0);
                if (existing != null)
                {
                    result = Assembly.ReflectionOnlyLoadFrom(existing.Location);
                }
                else
                    System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
            }
            catch
            {
            }

            return result;
        }

        private void AddAssemblies(IEnumerable<string> folders, IEnumerable<string> prefixes, 
            IEnumerable<CertificateConfig> certificates, Func<Assembly, bool> except)
        {
            if (folders?.Any() ?? false)
            {
                var platformVersion = Assembly.GetExecutingAssembly().GetName().Version;

                foreach (var folder in folders)
                {
                    var dlls = GetDlls(folder, prefixes)?.ToArray();

                    if (dlls?.Any() ?? false)
                    {
                        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Select(x => x.GetName().Name);

                        foreach (var dll in dlls)
                        {
                            var fileName = Path.GetFileNameWithoutExtension(dll);
                            if (!loadedAssemblies.ContainsCaseInsensitive(fileName))
                            {
                                try
                                {
                                    var assembly = Assembly.ReflectionOnlyLoadFrom(dll);

                                    bool skip = false;
                                    string reason = null;

                                    if (except != null && except(assembly))
                                    {
                                        skip = true;
                                        reason =
                                            $"Assembly {assembly.FullName} has not been loaded because it is not designed for this client.";
                                    }

                                    if (!skip && CheckVersion(platformVersion, assembly))
                                    {
                                        if (certificates?.Any() ?? false)
                                        {
#pragma warning disable SecurityIntelliSenseCS // MS Security rules violation
                                            var certificate = X509Certificate.CreateFromSignedFile(dll);
                                            if (certificate != null &&
                                                certificates.Any(x =>
                                                    string.Compare(x.Thumbprint,
                                                        new X509Certificate2(certificate).Thumbprint,
                                                        StringComparison.InvariantCultureIgnoreCase) == 0))
                                                _extensionsManager.AddExtensionsAssembly(dll);
#pragma warning restore SecurityIntelliSenseCS // MS Security rules violation
                                        }
                                        else
                                        {
                                            _extensionsManager.AddExtensionsAssembly(dll);
                                        }
                                    }
                                    else
                                    {
                                        _showWarning?.Invoke(
                                            reason ??
                                            $"Assembly {assembly.FullName} not loaded because it does not target this version of the Platform.");
                                    }
                                }
                                catch (FileLoadException)
                                {
                                    // Ignore: this could be caused by the fact that the library has been loaded from somewhere else. 
                                }
                                catch
                                {
                                    _showWarning?.Invoke($"Assembly {fileName} not loaded because it is not compatible with the current Platform.");
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool CheckVersion([NotNull] Version platformVersion, [NotNull] Assembly assembly)
        {
            bool result = false;

            var attributes = CustomAttributeData.GetCustomAttributes(assembly);
            if (attributes.Any())
            {
                var typeName = typeof(ExtensionsContainerAttribute).FullName;
                {
                    foreach (var attribute in attributes)
                    {
                        if (string.CompareOrdinal(attribute.AttributeType.FullName, typeName) == 0)
                        {
                            ExtensionsContainerAttribute attrib = null;

                            switch (attribute.ConstructorArguments.Count)
                            {
                                case 1:
                                    attrib = new ExtensionsContainerAttribute(attribute.ConstructorArguments[0].Value.ToString());
                                    break;
                                case 2:
                                    attrib = new ExtensionsContainerAttribute(attribute.ConstructorArguments[0].Value.ToString(),
                                        attribute.ConstructorArguments[1].ToString());
                                    break;
                            }

                            if (attrib?.IsApplicableExtensionsContainer(platformVersion) ?? false)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        private IEnumerable<string> GetDlls([Required] string folder, IEnumerable<string> prefixes)
        {
            IEnumerable<string> result = null;

            if (Directory.Exists(folder))
            {
                var files = Directory.GetFiles(folder, "*.dll", SearchOption.AllDirectories);
                result = files?.Where(x => HasPrefix(x, prefixes));
            }

            return result;
        }

        private static bool HasPrefix([Required] string fileWithPath, IEnumerable<string> prefixes)
        {
            bool result = false;

            var list = prefixes?.ToArray();
            if (list?.Any() ?? false)
            {
                var fileName = Path.GetFileName(fileWithPath);
                foreach (var prefix in list)
                {
                    if (fileName.StartsWith(prefix))
                    {
                        result = true;
                        break;
                    }
                }
            }
            else
            {
                // No prefixes: ok by default.
                result = true;
            }

            return result;
        }

        private void InitializeEngineKnownTypes()
        {
            KnownTypesBinder.AddKnownType(typeof(bool));
            KnownTypesBinder.AddKnownType(typeof(string));
            KnownTypesBinder.AddKnownType(typeof(int));
            KnownTypesBinder.AddKnownType(typeof(decimal));
            KnownTypesBinder.AddKnownType(typeof(System.Drawing.PointF));

            InitializeKnownTypes(Assembly.GetExecutingAssembly());
        }

        private void InitializeKnownTypes([NotNull] Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                InitializeKnownTypes(type);
            }
        }

        private void InitializeKnownTypes([NotNull] Type type)
        {
            if (type.GetCustomAttributes<JsonObjectAttribute>().Any())
            {
                KnownTypesBinder.AddKnownType(type);

                var interfaces = type.GetInterfaces();

                foreach (var i in interfaces)
                {
                    KnownTypesBinder.AddKnownType(i);
                }
            }
        }
        #endregion

        #region Public members.
        public static Manager Instance => _instance;

        public ExtensionsConfigurationManager Configuration => _configuration;

        public void LoadExtensions(ExecutionMode mode, Func<Assembly, bool> except = null)
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;

#pragma warning disable SecurityIntelliSenseCS // MS Security rules violation
            var dir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Extensions");
            if (Directory.Exists(dir))
            {
                var assemblies =
                    Directory.GetFiles(dir, "ThreatsManager.*.dll");
                if (assemblies.Any())
                {
                    foreach (var assembly in assemblies)
                    {
                        _extensionsManager.AddExtensionsAssembly(assembly);
                    }
                }
            }
#pragma warning restore SecurityIntelliSenseCS // MS Security rules violation

            InitializeEngineKnownTypes();

            _configuration = new ExtensionsConfigurationManager();
            var folders = _configuration.Folders;
            var prefixes = _configuration.Prefixes;
            var certificates = _configuration.Certificates;
            AddAssemblies(folders, prefixes, certificates, except);

            _extensionsManager.Load();
            _extensionsManager.SetExecutionMode(mode);
            _configuration.Initialize(_extensionsManager);

            RegisterContextAwareActionsEventHandlers();
        }
        #endregion

        #region Events management.
        private void RegisterContextAwareActionsEventHandlers()
        {
            var actions = _extensionsManager.GetExtensions<IContextAwareAction>()?.Where(x => _configuration.IsEnabled(x.Metadata.Id)).ToArray();

            if (actions?.Any() ?? false)
            {
                foreach (var lazy in actions)
                {
                    var action = lazy.Value;
                    if (action is IPanelOpenerExtension panelCreationRequired)
                        panelCreationRequired.OpenPanel += CreatePanel;

                    if (action is IRemoveIdentityFromModelRequiredAction removeIdentityFromModelRequiredAction)
                        removeIdentityFromModelRequiredAction.IdentityRemovingRequired += RemoveIdentityFromModel;

                    if (action is IDesktopAlertAwareExtension desktopAlertAware)
                    {
                        desktopAlertAware.ShowMessage += s => _showMessage?.Invoke(s);
                        desktopAlertAware.ShowWarning += s => _showWarning?.Invoke(s);
                    }
                }
            }
        }

        private void RemoveIdentityFromModel([NotNull] IIdentity identity)
        {
            if (identity is IEntity entity)
            {
                if (entity.Model.RemoveEntity(entity.Id))
                    _showMessage?.Invoke("Entity removed successfully.");
                else
                    _showWarning?.Invoke("It is not possible to remove the selected Entity.");
            } else if (identity is IDataFlow dataFlow)
            {
                if (dataFlow.Model.RemoveDataFlow(dataFlow.Id))
                    _showMessage?.Invoke("Flow removed successfully.");
                else
                    _showWarning?.Invoke("It is not possible to remove the selected Flow.");
            } else if (identity is ITrustBoundary trustBoundary)
            {
                if (trustBoundary.Model.RemoveGroup(trustBoundary.Id))
                    _showMessage?.Invoke("Trust Boundary removed successfully.");
                else
                    _showWarning?.Invoke("It is not possible to remove the selected Trust Boundary.");
            } else if (identity is IGroup group)
            {
                if (group.Model.RemoveGroup(group.Id))
                    _showMessage?.Invoke("Group removed successfully.");
                else
                    _showWarning?.Invoke("It is not possible to remove the selected Group.");
            }
        }

        private void CreatePanel([NotNull] IPanelFactory factory, [NotNull] IIdentity identity)
        {
            _panelForEntityRequired?.Invoke(factory, identity);
        }
        #endregion
    }
}
