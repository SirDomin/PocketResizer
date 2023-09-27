using Rocket.API;

namespace PocketResizer
{
    public class Configuration : IRocketPluginConfiguration
    {
        public string PermissionPrefix;

        public void LoadDefaults()
        {
            PermissionPrefix = "pocket";
        }
    }
}
