using System;
using System.Collections.Generic;
using System.Linq;
using PostSharp.Patterns.Contracts;
using ThreatsManager.Interfaces;
using ThreatsManager.Interfaces.ObjectModel;
using ThreatsManager.Interfaces.ObjectModel.Properties;

namespace ThreatsManager.Quality.Analyzers
{
    [Extension("B6EFBB56-C526-4ABE-99EC-50E312FBDCDD", "Missing Threat Events Quality Analyzer", 30, ExecutionMode.Simplified)]
    public class NoThreatEvent : IQualityAnalyzer
    {
        public string Label => "Missing Threat Events";
        public string Description => "Most Entities and Flows should have at least an associated Threat Event.";
        public double MultiplicationFactor => 0.75;

        public override string ToString()
        {
            return Label;
        }

        public bool GetThresholds([NotNull] IThreatModel model, 
            Func<IQualityAnalyzer, IPropertiesContainer, bool> isFalsePositive, 
            out double minRed, out double maxRed, out double minYellow, out double maxYellow,
            out double minGreen, out double maxGreen)
        {
            bool result = false;
            minRed = 0;
            maxRed = 0;
            minYellow = double.NaN;
            maxYellow = double.NaN;
            minGreen = 0;
            maxGreen = 0;

            var count = (isFalsePositive(this, model) ? 0 : 1) + 
                        (model.Entities?.Where(x => !isFalsePositive(this, x)).Count() ?? 0) + 
                        (model.DataFlows?.Where(x => !isFalsePositive(this, x)).Count() ?? 0);

            if (count >= 10)
            {
                minGreen = 0.0;
                maxGreen = count * 0.5;
                minYellow = Math.Floor(maxGreen * 10.0) / 10.0 + Math.Floor(count / 10.0) / 10;
                maxYellow = count * 0.75;
                minRed = Math.Floor(maxYellow * 10.0) / 10.0 + Math.Floor(count / 10.0) / 10;
                maxRed = count;

                result = true;
            }
            else if (count > 0)
            {
                minGreen = 0.0;
                maxGreen = count * 0.75;
                minRed = maxGreen + count * 0.02;
                maxRed = count;

                result = true;
            }
            else
            {
                minGreen = 0;
                maxGreen = 1;
                minRed = double.NaN;
                maxRed = double.NaN;
            }

            return result;
        }

        public double Analyze([NotNull] IThreatModel model, 
            Func<IQualityAnalyzer, IPropertiesContainer, bool> isFalsePositive, 
            out IEnumerable<object> instances)
        {
            double result = 0.0;
            instances = null;

            List<object> items = new List<object>();

            if (!(model.ThreatEvents?.Any() ?? false) && !isFalsePositive(this, model))
                items.Add(model);

            var entities = model.Entities?
                .Where(x => !isFalsePositive(this, x) && !(x.ThreatEvents?.Any() ?? false)).ToArray();
            if (entities?.Any() ?? false)
            {
                items.AddRange(entities);
            }

            var flows = model.DataFlows?
                .Where(x => !isFalsePositive(this, x) && !(x.ThreatEvents?.Any() ?? false)).ToArray();
            if (flows?.Any() ?? false)
            {
                items.AddRange(flows);
            }

            if (items.Any())
            {
                result = items.Count;
                instances = items;
            }

            return result;
        }
    }
}
