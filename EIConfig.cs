using Steamworks;
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

        [DefaultValue(true)]
        [Label("Allow items to overflow to other pages")]
        [Tooltip("If true, items that won't fit into the target page will overflow to the next available page")]
        public bool AllowItemOverflowToOtherPages { get; set; } = true;

        [DefaultValue(true)]
        [Label("Send pickup items to current page")]
        [Tooltip("Tries to add any pickup items to your current page. If false it will be sent to your first available page.")]
        public bool SendItemPickupToCurrentPage { get; set; } = true;

        [DefaultValue(false)]
        [Label("Stack pickup items across pages")]
        [Tooltip("If true, will try to find and stack items across pages")]
        public bool TryStackLikeItems { get; set; } = false;
        public override void OnLoaded()
        {
            ExpandedInventory.Config = this;
        }
    }
}
