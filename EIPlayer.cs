using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ExpandedInventory
{
    public class EIPlayer : ModPlayer
    {
        public static ModKeybind NextPage;
        public static ModKeybind PrevPage;

        private List<Item>[] itemPages;
        private int numOfPages;
        private int currentPage;

        /// <summary>
        /// Loads the given page into the player's inventory. Saves the previous inventory to the current page.
        /// </summary>
        /// <param name="nextPageNum"> The page number to load</param>
        /// <param name="currPageNum"> The previous page number to save to</param>
        private void goToPage(int nextPageNum, int currPageNum)
        {
            for (int i = 0; i < itemPages[nextPageNum - 1].Count; i++)
            {
                // Ignore hotbar, ammo, money, and trash slots
                if(i < 10 || i > 49)
                {
                    continue;
                }

                Item newItem = itemPages[nextPageNum - 1][i];
                Item oldItem = Player.inventory[i];
                Player.inventory[i] = newItem;
                itemPages[currPageNum - 1].RemoveAt(i);
                itemPages[currPageNum - 1].Insert(i, oldItem);
            }
        }

        /// <summary>
        /// Increments/Decrements the page by 1 depending on the argument. Will update the player's inventory.
        /// </summary>
        /// <param name="isForward">True if move to the next page, False to move to the previous page</param>
        public void ChangePage(bool isForward)
        {
            int previousPage = currentPage;

            if (isForward && currentPage < numOfPages)
            {
                currentPage++;
                goToPage(currentPage, previousPage);
            }
            else if (!isForward && currentPage > 1)
            {
                currentPage--;
                goToPage(currentPage, previousPage);
            }
        }

        public string GetPageString()
        {
            return currentPage.ToString() + "/" + numOfPages.ToString();
        }
        public override void Initialize()
        {
            numOfPages = ExpandedInventory.Config.NumberOfPages;
            currentPage = 1;

            itemPages = new List<Item>[numOfPages];

            for(int i = 0; i < numOfPages; i++)
            {
                itemPages[i] = new List<Item>();

                for(int x = 0; x < 59; x++)
                {
                    itemPages[i].Add(new Item());
                }
            }
        }

        public override void Load()
        {
            NextPage = KeybindLoader.RegisterKeybind(Mod, "Next Item Page", Keys.P);
            PrevPage = KeybindLoader.RegisterKeybind(Mod, "Previous Item Page", Keys.O);
        }

        public override void LoadData(TagCompound tag)
        {
            if(tag.ContainsKey("CurrentPage"))
            {
                currentPage = tag.Get<int>("CurrentPage");
            }

            for(int i = 1; i <= numOfPages; i++)
            {
                if (tag.ContainsKey("InventoryPage" + i.ToString())) {
                    List<Item> itemList = tag.Get<List<Item>>("InventoryPage" + i.ToString());

                    if(itemList != null && itemList.Count != 0)
                        itemPages[i - 1] = itemList;
                }
            }
        }

        public override void OnEnterWorld(Player player)
        {
            if (currentPage != 1)
            {
                currentPage = 1;
                goToPage(1, currentPage);
            }
        }

        public override void SaveData(TagCompound tag)
        {

            tag["CurrentPage"] = currentPage;
            for(int i = 1; i <= numOfPages;i++)
            {
                tag["InventoryPage" + i.ToString()] = itemPages[i - 1];
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (NextPage.JustPressed)
                ChangePage(true);
            else if (PrevPage.JustPressed)
                ChangePage(false);
        }
    }
}
