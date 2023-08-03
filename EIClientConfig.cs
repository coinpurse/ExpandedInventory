using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ExpandedInventory
{
    public class EIClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(12)]
        [Range(0, 100)]
        public int PositionX { get; set; } = 12;

        [DefaultValue(28)]
        [Range(0, 100)]
        public int PositionY { get; set; } = 28;

        [DefaultValue(12)]
        [Range(0, 100)]
        public int PositionXWithChest { get; set; } = 12;

        [DefaultValue(46)]
        [Range(0, 100)]
        public int PositionYWithChest { get; set; } = 46;

        public override void OnLoaded()
        {
            ExpandedInventory.ClientConfig= this;
        }
    }
}
