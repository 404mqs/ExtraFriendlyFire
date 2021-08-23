using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using System.Linq;
using Logger = Rocket.Core.Logging.Logger;

namespace MQS.ExtraFriendlyFire
{
    public class MQSPlugin : RocketPlugin<ExtraFriendlyFireConfig>
    {
        public static MQSPlugin Instance;

        protected override void Load()
        {
            Instance = this;

            Logger.LogWarning("++++++++++++++++++++++++++++++++++++++");
            Logger.LogWarning($"[{Name}] loaded! ");
            Logger.LogWarning("Dev: ExtraFriendlyJuice");
            Logger.LogWarning("Updated by: 404mqs");
            Logger.LogWarning("https://discord.gg/Ssbpd9cvgp");
            Logger.LogWarning("++++++++++++++++++++++++++++++++++++++");

            DamageTool.damagePlayerRequested += OnPlayerDamage;
        }

        private void OnPlayerDamage(ref DamagePlayerParameters parameters, ref bool shouldAllow)
        {
            UnturnedPlayer killer = UnturnedPlayer.FromCSteamID(parameters.killer);
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(parameters.player);

            if (player.IsAdmin && Configuration.Instance.IgnoreAdmins)
            {
                return;
            }

            if (killer.IsAdmin && Configuration.Instance.IgnoreAdmins)
            {
                return;
            }

            List<RocketPermissionsGroup> MutualGroups = GetMutualGroups(killer, player);
            List<string> Groups = Configuration.Instance.Groups;

            for (int i = 0; i < MutualGroups.Count; i++)
            {
                if (Groups.Contains(MutualGroups[i].Id))
                {
                    shouldAllow = false;
                }
            }
        }

        protected override void Unload()
        {
            Logger.LogWarning("++++++++++++++++++++++++++++++++++++++");
            Logger.LogWarning($"[{Name}] unloaded! ");
            Logger.LogWarning("++++++++++++++++++++++++++++++++++++++");
        }

        public static List<RocketPermissionsGroup> GetMutualGroups(UnturnedPlayer p1, UnturnedPlayer p2)
        {
            List<RocketPermissionsGroup> p1Groups = R.Permissions.GetGroups(p1, true);
            List<RocketPermissionsGroup> p2Groups = R.Permissions.GetGroups(p2, true);

            return p1Groups.Intersect(p2Groups).ToList();
        }
    }
}