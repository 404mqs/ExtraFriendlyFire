using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ExtraConcentratedJuice.ExtraFriendlyFire
{
    public class ExtraFriendlyFireConfig : IRocketPluginConfiguration
    {
        [XmlArrayItem(ElementName = "group")]
        public List<string> groups;

        public bool ignoreAdmin;
        public string ignorePermissionString;

        public void LoadDefaults()
        {
            groups = new List<string>{ "default", "VIP", "Moderator", "NoDamageGroup"};
            ignoreAdmin = true;
            ignorePermissionString = "extrafriendlyfire.ignore";
        }
    }
}
