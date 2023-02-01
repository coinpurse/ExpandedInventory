using Terraria.ModLoader;

namespace ExpandedInventory
{
    public class ExpandedInventory : Mod
	{
		internal static EIConfig Config;
        internal static EIClientConfig ClientConfig;
		public ExpandedInventory()
		{

		}
        public override void Unload()
        {
            base.Unload();
            Config = null;
            ClientConfig = null;
        }
    }
}