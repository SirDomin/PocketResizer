using PocketResizer.Models;
using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketResizer.Utils
{
    internal static class PocketUtil
    {
        internal static PocketModel GetBestPocket(UnturnedPlayer player)
        {
            var permissions = player.GetPermissions().Select(a => a.Name).Where(p =>
                p.ToLower().StartsWith($"{Plugin.Config.PermissionPrefix}.") && !p.Equals($"{Plugin.Config.PermissionPrefix}.",
                    StringComparison.InvariantCultureIgnoreCase));
            var enumerable = permissions as string[] ?? permissions.ToArray();
            if (enumerable.Length == 0)
                return null;
            var bestPocket = new PocketModel(0, 0);
            foreach (var pocket in enumerable)
            {
                var pocketSplit = pocket.Split('.');
                if (pocketSplit.Length != 3)
                {
                    Logger.LogError($"[{Plugin.Instance.Name}] Error: PermissionPrefix must not contain '.'");
                    Logger.LogError($"[{Plugin.Instance.Name}] Invalid permission format: {pocket}");
                    Logger.LogError($"[{Plugin.Instance.Name}] Correct format: 'permPrefix'.'width'.'height'");
                    continue;
                }

                try
                {
                    byte.TryParse(pocketSplit[1], out var w);
                    byte.TryParse(pocketSplit[2], out var h);
                    if (w * h > bestPocket.Width * bestPocket.Height)
                        bestPocket = new PocketModel(w, h);
                }
                catch (Exception ex)
                {
                    bestPocket = new PocketModel(5, 3);

                    Logger.LogError($"[{Plugin.Instance.Name}] Error: " + ex);
                }
            }

            return bestPocket;
        }

        internal static void RevertPocket()
        {
            foreach (var player in Provider.clients.Select(UnturnedPlayer.FromSteamPlayer))
            {
                player.Inventory.items[2].resize(5, 3);
            }
        }

        internal static void Modify(Player player)
        {
            var bestPocket = GetBestPocket(UnturnedPlayer.FromPlayer(player));
            if (bestPocket == null)
                return;

            var items = player.inventory.items[2];
            if (items.height != bestPocket.Height || items.width != bestPocket.Width)
                items.resize((byte)bestPocket.Width, (byte)bestPocket.Height);
        }
    }
}
