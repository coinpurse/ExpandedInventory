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
        [Label("X position")]
        [Tooltip("X position in percent of screen width.")]
        public int PositionX { get; set; } = 12;

        [DefaultValue(28)]
        [Range(0, 100)]
        [Label("Y position")]
        [Tooltip("Y position in percent of screen height.")]
        public int PositionY { get; set; } = 28;

        [DefaultValue(12)]
        [Range(0, 100)]
        [Label("X position with chest")]
        [Tooltip("X position in percent of screen width. Use this to change the position when a chest is opened.")]
        public int PositionXWithChest { get; set; } = 12;

        [DefaultValue(46)]
        [Range(0, 100)]
        [Label("Y position with chest")]
        [Tooltip("Y position in percent of screen height. Use this to change the position when a chest is opened.")]
        public int PositionYWithChest { get; set; } = 46;

        public override void OnLoaded()
        {
            ExpandedInventory.ClientConfig= this;
        }
    }
}
