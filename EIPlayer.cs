using ExpandedInventory.UI;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
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

        private EIUIState pageUI;

       // public ItemIndexLibrary ItemLibrary;

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
            Recipe.FindRecipes();
        }

        private void saveCurrentPage(int currentPage)
        {
            for(int i = 0; i < itemPages[currentPage - 1].Count; i++)
            {
                if (i < 10 || i > 49)
                {
                    continue;
                }

                itemPages[currentPage - 1].RemoveAt(i);
                itemPages[currentPage - 1].Insert(i, Player.inventory[i]);
            }
        }

        /// <summary>
        /// Increments/Decrements the page by 1 depending on the argument. Will update the player's inventory.
        /// </summary>
        /// <param name="isForward">True if move to the next page, False to move to the previous page</param>
        public void ChangePage(bool isForward)
        {
            int previousPage = currentPage;

            if (isForward)
            {
                if (currentPage >= numOfPages)
                {
                    currentPage = 1;
                }
                else
                {
                    currentPage++;
                }
            }
            else if (!isForward)
            {
                if (currentPage <= 1)
                {
                    currentPage = numOfPages;
                }
                else
                {
                    currentPage--;
                }
            }

            goToPage(currentPage, previousPage);
        }

        public string GetPageString()
        {
            return currentPage.ToString() + "/" + numOfPages.ToString();
        }
        public int GetCurrentPage()
        {
            return currentPage;
        }
        public void SetPageUI(EIUIState eiUIState)
        {
            pageUI = eiUIState;
        }
        public override void Initialize()
        {
            numOfPages = ExpandedInventory.Config.NumberOfPages;
            currentPage = 1;

            itemPages = new List<Item>[numOfPages];
           // ItemLibrary = new ItemIndexLibrary();

            for(int i = 0; i < numOfPages; i++)
            {
                itemPages[i] = new List<Item>();

                for(int x = 0; x < 59; x++)
                {
                    itemPages[i].Add(new Item());
                }
            }
        }

        public override bool CanUseItem(Item item)
        {
            if(Main.playerInventory && pageUI.IsHoveringButtons())
            {
                return false;
            }

            return base.CanUseItem(item);
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

            //ItemLibrary.UpdateIndexLibraryFull(itemPages, numOfPages);
        }

        public override void OnEnterWorld(Player player)
        {
            if (currentPage != 1)
            {
                goToPage(1, currentPage);
                currentPage = 1;
            }
        }

        public override void SaveData(TagCompound tag)
        {
            saveCurrentPage(currentPage);

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
