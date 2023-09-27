using PocketResizer.Utils;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace PocketResizer
{
    public class Plugin : RocketPlugin<Configuration>
    {
        internal static Plugin Instance;
        internal static Configuration Config;

        protected override void Load()
        {
            Instance = this;
            Config = Configuration.Instance;

            if (Level.isLoaded)
                foreach (var steamPlayer in Provider.clients)
                    PocketUtil.Modify(steamPlayer.player);

            Logger.LogWarning($"[{Name}] Plugin loaded successfully!");
        }
        protected override void Unload()
        {
            Instance = null;
            Config = null;
            PocketUtil.RevertPocket();

            Logger.LogWarning($"[{Name}] Plugin unloaded successfully!");
        }
    }
}
