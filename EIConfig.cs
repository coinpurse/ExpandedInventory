using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ExpandedInventory
{
    public class EIConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;


        [DefaultValue(2)]
        [Label("Number of Pages")]
        [ReloadRequired]
        public int NumberOfPages { get; set; } = 2;

        public override void OnLoaded()
        {
            ExpandedInventory.Config = this;
        }
    }
}
