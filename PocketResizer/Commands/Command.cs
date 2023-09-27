using PocketResizer.Utils;
using Rocket.API;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketResizer
{
    internal class Command : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "pocketReload";

        public string Help => "update pocket sizes";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            foreach (var steamPlayer in Provider.clients)
                PocketUtil.Modify(steamPlayer.player);
        }
    }
}
