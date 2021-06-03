﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PostSharp.Patterns.Contracts;
using ThreatsManager.Mitre.Cwe;
using ThreatsManager.Utilities;

namespace ThreatsManager.Mitre.Graph
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ViewNode : Node
    {
        internal ViewNode([NotNull] MitreGraph graph, [NotNull] Cwe.ViewType view) : base(graph, "CWE", view.ID)
        {
            if (view.Status == Cwe.StatusEnumeration.Deprecated || view.Status == Cwe.StatusEnumeration.Obsolete)
                throw new ArgumentException(Properties.Resources.InvalidStatus, "view");
            if (view.Type != ViewTypeEnumeration.Graph)
                throw new ArgumentException(Properties.Resources.InvalidViewType, "view");

            Name = view.Name;
            Description = view.Objective.ConvertToString();
            if (Enum.TryParse<Status>(view.Status.ToString(), out var status))
                Status = status;

            #region Add relationships.
            var count = view.Members?.Items?.Length ?? 0;
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var rel = view.Members.ItemsElementName[i] == ItemsChoiceType1.Has_Member
                        ? RelationshipType.ParentOf
                        : RelationshipType.ChildOf;
                    AddRelationship(rel, "CWE", view.Members.Items[i].CWE_ID, view.Members.Items[i].View_ID);
                }
            }
            #endregion

            #region Add audience.

            var audience = view.Audience?.ToArray();
            if (audience?.Any() ?? false)
            {
                if (Audience == null)
                    Audience = new List<Audience>();

                foreach (var sh in audience)
                {
                    Audience.Add(new Audience(sh.Type.GetXmlEnumLabel(), sh.Description));
                }
            }
            #endregion
        }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; private set; }

        [JsonProperty("audience")]
        public List<Audience> Audience { get; private set; }
    }
}