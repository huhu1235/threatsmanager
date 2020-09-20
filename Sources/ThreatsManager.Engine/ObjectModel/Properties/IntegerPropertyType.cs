﻿using System;
using Newtonsoft.Json;
using PostSharp.Patterns.Contracts;
using ThreatsManager.Engine.Aspects;
using ThreatsManager.Interfaces.ObjectModel;
using ThreatsManager.Interfaces.ObjectModel.Properties;
using ThreatsManager.Utilities.Aspects;
using ThreatsManager.Utilities.Aspects.Engine;

namespace ThreatsManager.Engine.ObjectModel.Properties
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    [SimpleNotifyPropertyChanged]
    [AutoDirty]
    [DirtyAspect]
    [IdentityAspect]
    [ThreatModelChildAspect]
    [PropertyTypeAspect]
    [AssociatedPropertyClass("ThreatsManager.Engine.ObjectModel.Properties.PropertyInteger, ThreatsManager.Engine")]
    public class IntegerPropertyType : IIntegerPropertyType
    {
        public IntegerPropertyType()
        {

        }

        public IntegerPropertyType([Required] string name, [NotNull] IPropertySchema schema) : this()
        {
            _id = Guid.NewGuid();
            _schemaId = schema.Id;
            _model = schema.Model;
            _modelId = schema.Model?.Id ?? Guid.Empty;
            Name = name;
            Visible = true;
        }

        #region Default implementation.
        public Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IThreatModel Model { get; }
        public bool Locked { get; set; }
        public Guid SchemaId { get; }
        public int Priority { get; set; }
        public bool Visible { get; set; }

        public event Action<IDirty, bool> DirtyChanged;
        public bool IsDirty { get; }
        public void SetDirty()
        {
        }

        public void ResetDirty()
        {
        }

        public bool IsDirtySuspended { get; }
        public void SuspendDirty()
        {
        }

        public void ResumeDirty()
        {
        }
        #endregion

        #region Additional placeholders required.
        protected Guid _id { get; set; }
        protected Guid _modelId { get; set; }
        protected IThreatModel _model { get; set; }
        protected Guid _schemaId { get; set; }
        #endregion

        #region Specific implementation.
        public override string ToString()
        {
            return Name;
        }

        public IPropertyType Clone([NotNull] IPropertyTypesContainer container)
        {
            IPropertyType result = null;

            if (container is IPropertySchema schema)
            {
                result = new IntegerPropertyType
                {
                    _id = _id,
                    _schemaId = schema.Id,
                    _model = schema.Model,
                    _modelId = schema.Model?.Id ?? Guid.Empty,
                    Name = Name,
                    Description = Description,
                    Visible = Visible,
                    Priority = Priority
                };
                container.Add(result);
            }
            return result;
        }
        #endregion
    }
}
