﻿using Newtonsoft.Json;
using PostSharp.Patterns.Contracts;

namespace ThreatsManager.Mitre.Graph
{
    [JsonObject("Audience")]
    public class Audience
    {
        public Audience([Required] string name, string description)
        {
            Name = name;
            Description = description;
        }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("desc")]
        public string Description { get; private set; }
    }
}