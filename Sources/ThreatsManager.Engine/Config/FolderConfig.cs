using System.Configuration;

namespace ThreatsManager.Engine.Config
{
    public class FolderConfig : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get => (string)this["name"];
            set => this["name"] = value;
        }
    }
}
