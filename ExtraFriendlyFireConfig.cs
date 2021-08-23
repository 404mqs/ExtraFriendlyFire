using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MQS.ExtraFriendlyFire
{
    public class ExtraFriendlyFireConfig : IRocketPluginConfiguration
    {
        [XmlArrayItem(ElementName = "Group")]
        public List<string> Groups;

        public bool IgnoreAdmins;

        public void LoadDefaults()
        {
            Groups = new List<string> { "Blue", "Red"};
            IgnoreAdmins = false;
        }
    }
}