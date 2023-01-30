using System.Reflection;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace ExpandedInventory
{
	public class ExpandedInventory : Mod
	{
		internal static EIConfig Config;
		public ExpandedInventory()
		{

		}

        public override void Unload()
        {
            base.Unload();
            Config = null;
        }
    }
}