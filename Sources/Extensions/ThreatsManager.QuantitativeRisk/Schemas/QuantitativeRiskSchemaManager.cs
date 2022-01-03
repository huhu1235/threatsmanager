﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PostSharp.Patterns.Contracts;
using ThreatsManager.Interfaces;
using ThreatsManager.Interfaces.ObjectModel;
using ThreatsManager.Interfaces.ObjectModel.Properties;
using ThreatsManager.Interfaces.ObjectModel.ThreatsMitigations;
using ThreatsManager.QuantitativeRisk.Engine;
using ThreatsManager.QuantitativeRisk.Facts;
using ThreatsManager.QuantitativeRisk.Properties;

namespace ThreatsManager.QuantitativeRisk.Schemas
{
    public class QuantitativeRiskSchemaManager
    {
        private readonly IThreatModel _model;

        public QuantitativeRiskSchemaManager([NotNull] IThreatModel model)
        {
            _model = model;
        }
        
        public IPropertySchema GetSchema()
        {
            var result = _model.GetSchema(Resources.QuantitativeRiskSchemaName, Resources.DefaultNamespace) ??
                         _model.AddSchema(Resources.QuantitativeRiskSchemaName, Resources.DefaultNamespace);
            result.AppliesTo = Scope.ThreatModel;
            result.Priority = 20;
            result.Visible = false;
            result.System = true;
            result.NotExportable = true;
            result.AutoApply = false;
            result.RequiredExecutionMode = ExecutionMode.Expert;
            result.Description = Resources.ConfigurationPropertySchemaDescription;

            var currency = result.GetPropertyType("Currency") ?? result.AddPropertyType("Currency", PropertyValueType.SingleLineString);
            currency.Visible = false;
            currency.DoNotPrint = true;
            currency.Description = Resources.CurrencyProperty;

            var context = result.GetPropertyType("Context") ?? result.AddPropertyType("Context", PropertyValueType.SingleLineString);
            context.Visible = false;
            context.DoNotPrint = true;
            context.Description = Resources.FactContextProperty;

            var factProvider = result.GetPropertyType("FactProvider") ?? result.AddPropertyType("FactProvider", PropertyValueType.SingleLineString);
            factProvider.Visible = false;
            factProvider.DoNotPrint = true;
            factProvider.Description = Resources.FactProviderProperty;

            var facts = result.GetPropertyType("Facts") ?? result.AddPropertyType("Facts", PropertyValueType.JsonSerializableObject);
            facts.Visible = false;
            facts.DoNotPrint = true;
            facts.Description = Resources.FactsProperty;

            var thresholds = result.GetPropertyType("Thresholds") ?? result.AddPropertyType("Thresholds", PropertyValueType.JsonSerializableObject);
            thresholds.Visible = false;
            thresholds.DoNotPrint = true;
            thresholds.Description = Resources.ThresholdsProperty;

            return result;
        }

        public IPropertySchema GetEvaluationSchema()
        {
            var result = _model.GetSchema(Resources.EvaluationSchemaName, Resources.DefaultNamespace);
            if (result == null)
            {
                result = _model.AddSchema(Resources.EvaluationSchemaName, Resources.DefaultNamespace);
            }
            result.AppliesTo = Scope.ThreatEventScenario;
            result.AutoApply = false;
            result.Priority = 20;
            result.Visible = false;
            result.System = true;
            result.NotExportable = true;
            result.Description = Resources.EvaluationPropertySchemaDescription;

            var risk = result.GetPropertyType(Resources.RiskProperty);
            if (risk == null)
            {
                risk = result.AddPropertyType(Resources.RiskProperty, PropertyValueType.JsonSerializableObject);
            }

            return result;
        }

        public string Currency
        {
            get
            {
                string result = null;

                var propertyType = GetSchema()?.GetPropertyType("Currency");
                if (propertyType != null)
                {
                    result = (_model.GetProperty(propertyType) ?? _model.AddProperty(propertyType, NumberFormatInfo.CurrentInfo.CurrencySymbol))?.StringValue;
                }

                return result;
            }

            set
            {
                var propertyType = GetSchema()?.GetPropertyType("Currency");
                if (propertyType != null)
                {
                    var property = _model.GetProperty(propertyType) ?? _model.AddProperty(propertyType, null);
                    property.StringValue = value;
                }
            }
        }

        public string FactProviderId
        {
            get
            {
                string result = null;

                var propertyType = GetSchema()?.GetPropertyType("FactProvider");
                if (propertyType != null)
                {
                    result = _model.GetProperty(propertyType)?.StringValue;
                }

                return result;
            }

            set
            {
                var propertyType = GetSchema()?.GetPropertyType("FactProvider");
                if (propertyType != null)
                {
                    var property = _model.GetProperty(propertyType) ?? _model.AddProperty(propertyType, null);
                    property.StringValue = value;
                }
            }
        }

        public Risk GetRisk([NotNull] IThreatEventScenario scenario)
        {
            return (GetProperty(scenario, Resources.RiskProperty) as IPropertyJsonSerializableObject)?.Value as Risk;
        }

        public void SetRisk([NotNull] IThreatEventScenario scenario, Risk risk)
        {
            var property = GetProperty(scenario, Resources.RiskProperty) ?? AddProperty(scenario, Resources.RiskProperty);
            if (property is IPropertyJsonSerializableObject jsonSerializableObject)
            {
                jsonSerializableObject.Value = risk;
            }
        }

        public IDictionary<int, decimal> GetThresholds()
        {
            IDictionary<int, decimal> result = null;

            var propertyType = GetSchema()?.GetPropertyType("Thresholds");
            if (propertyType != null && _model.GetProperty(propertyType) is IPropertyJsonSerializableObject jsonObject && jsonObject.Value is Thresholds thresholds)
            {
                result = thresholds.Items?.ToDictionary(x => x.SeverityId, y => y.Value);
            }

            return result;
        }

        public void SetThresholds(IDictionary<int, decimal> thresholds)
        {
            var propertyType = GetSchema()?.GetPropertyType("Thresholds");

            if (propertyType != null)
            {
                if (thresholds?.Any() ?? false)
                {
                    var property = _model.GetProperty(propertyType) ?? _model.AddProperty(propertyType, null);
                    if (property is IPropertyJsonSerializableObject jsonObject)
                    {
                        jsonObject.Value = new Thresholds()
                        {
                            Items = new List<Threshold>(thresholds.Select(x => new Threshold(x.Key, x.Value)))
                        };
                    }
                }
                else
                {
                    var property = _model.GetProperty(propertyType);
                    if (property != null)
                        property.StringValue = null;
                }
            }
        }

        private IProperty GetProperty([NotNull] IThreatEventScenario scenario, [Required] string propertyName)
        {
            IProperty result = null;

            var schema = GetEvaluationSchema();           
            var propertyType = schema?.GetPropertyType(propertyName);
            if (propertyType != null)
            {
                result = scenario.GetProperty(propertyType);
            }

            return result;
        }

        private IProperty AddProperty([NotNull] IThreatEventScenario scenario, [Required] string propertyName)
        {
            IProperty result = null;

            var schema = GetEvaluationSchema();           
            var propertyType = schema?.GetPropertyType(propertyName);
            if (propertyType != null)
            {
                result = scenario.AddProperty(propertyType, null);
            }

            return result;
        }

        #region Facts management.
        public string Context
        {
            get
            {
                string result = null;

                var schema = GetSchema();
                var propertyType = schema?.GetPropertyType("Context");
                if (propertyType != null)
                {
                    result = _model.GetProperty(propertyType)?.StringValue;
                }

                return result;
            }

            set
            {
                var schema = GetSchema();           
                var propertyType = schema?.GetPropertyType("Context");
                if (propertyType != null)
                {
                    var property = _model.GetProperty(propertyType) ?? _model.AddProperty(propertyType, null);
                    if (property != null)
                        property.StringValue = value;
                }
            }
        }

        public IEnumerable<Fact> GetFacts()
        {
            IEnumerable<Fact> result = null;

            var schema = GetSchema();           
            var propertyType = schema?.GetPropertyType("Facts");
            if (propertyType != null && _model.GetProperty(propertyType) is IPropertyJsonSerializableObject jsonSerializableObject &&
                jsonSerializableObject.Value is FactContainer container)
            {
                result = container.Facts;
            }

            return result;
        }

        public bool AddFact([NotNull] Fact fact)
        {
            bool result = false;

            var schema = GetSchema();           
            var propertyType = schema?.GetPropertyType("Facts");
            if (propertyType != null)
            {
                var property = _model.GetProperty(propertyType) ?? _model.AddProperty(propertyType, null);
                if (property is IPropertyJsonSerializableObject jsonSerializableObject)
                {
                    if (jsonSerializableObject.Value is FactContainer container)
                    {
                        result = container.Add(fact);
                    }
                    else
                    {
                        container = new FactContainer();
                        result = container.Add(fact);
                        jsonSerializableObject.Value = container;
                    }
                }
            }

            return result;
        }

        public bool RemoveFact([NotNull] Fact fact)
        {
            bool result = false;

            var schema = GetSchema();           
            var propertyType = schema?.GetPropertyType("Facts");
            if (propertyType != null)
            {
                var property = _model.GetProperty(propertyType) ?? _model.AddProperty(propertyType, null);
                if (property is IPropertyJsonSerializableObject jsonSerializableObject)
                {
                    if (jsonSerializableObject.Value is FactContainer container)
                    {
                        result = container.Remove(fact);
                    }
                }
            }

            return result;
        }
        #endregion
    }
}