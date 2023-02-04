using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ExpandedInventory
{
    public class EIGlobalItem : GlobalItem
    {
        private bool needsHome;
        public EIGlobalItem() {
            needsHome = false;
        }

        public override bool ItemSpace(Item item, Player player)
        {
            bool sendItemPickupToCurrentPage = ExpandedInventory.Config.SendItemPickupToCurrentPage;
            bool allowItemOverflowToOtherPages = ExpandedInventory.Config.AllowItemOverflowToOtherPages;
            bool tryStackLikeItems = ExpandedInventory.Config.TryStackLikeItems;
            bool currentPageHasSpace = base.ItemSpace(item, player);

            // Always try to stack like items if possible
            if(tryStackLikeItems)
            {
                
            }
            if(sendItemPickupToCurrentPage && currentPageHasSpace)
                return currentPageHasSpace;
            if (!allowItemOverflowToOtherPages)
                return currentPageHasSpace;

        }
    }
}
