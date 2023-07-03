using ExpandedInventory.Other;
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
        private enum howToHandleItem
        {
            None,
            StackInto,
            FindHome
        }

        private howToHandleItem itemHandler;
        private int targetSlot;
        private int targetPage;

        public EIGlobalItem() {
            itemHandler = howToHandleItem.None;
            targetSlot = -1;
            targetPage = 0;
        }
        
        public override bool ItemSpace(Item item, Player player)
        {
            bool sendItemPickupToCurrentPage = ExpandedInventory.Config.SendItemPickupToCurrentPage;
            bool allowItemOverflowToOtherPages = ExpandedInventory.Config.AllowItemOverflowToOtherPages;
            bool tryStackLikeItems = ExpandedInventory.Config.TryStackLikeItems;
            bool currentPageHasSpace = base.ItemSpace(item, player);
            EIPlayer modPlayer = player.GetModPlayer<EIPlayer>();
            ItemIndexLibrary itemLibrary = modPlayer.ItemLibrary;
            
            // Always set it to none for default handling
            itemHandler= howToHandleItem.None;

            // Always try to stack like items if possible
            if (tryStackLikeItems)
            {
                int initialPage;
                if (sendItemPickupToCurrentPage)
                    initialPage = modPlayer.GetCurrentPage();
                else
                    initialPage = 1;

                itemLibrary.FindStackableItem(item, 1);
                
            }
            if(sendItemPickupToCurrentPage && currentPageHasSpace)
                return currentPageHasSpace;
            if (!allowItemOverflowToOtherPages)
                return currentPageHasSpace;

            if(itemLibrary.IsItemSlotAvailable())
            {
                itemHandler = howToHandleItem.FindHome;
                if (!sendItemPickupToCurrentPage)
                {
                    targetSlot = itemLibrary.GetAvailableItemSlot(true, 1, out targetPage);
                    return true;
                }
                else
                {
                    targetSlot = itemLibrary.GetAvailableItemSlot(true, modPlayer.GetCurrentPage(), out targetPage);
                    return true;
                }
                
            }
            else
            {
                return currentPageHasSpace;
            }

        }
    }
}
