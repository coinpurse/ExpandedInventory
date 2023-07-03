using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ExpandedInventory.Other
{
    public class ItemIndexLibrary
    {
        private Dictionary<int, Dictionary<int,Dictionary<int,ItemReference>>> itemIndex;    // index: item id    index2: page number     index3: slot index
        private Dictionary<int, List<int>> openItemSlots;   // index: page index   List index: slot index

        public ItemIndexLibrary()
        {
            itemIndex = new Dictionary<int, Dictionary<int, Dictionary<int, ItemReference>>>();
            openItemSlots = new Dictionary<int, List<int>>();
        }
        public ItemIndexLibrary(List<Item>[] itemPages, int numberOfPages) : this()
        {
            UpdateIndexLibraryFull(itemPages, numberOfPages);
        }

        public void UpdateIndexLibraryFull(List<Item>[] itemPages, int numberOfPages)
        {
            // Clear out the entirety of item library
            foreach(var itemDict in itemIndex)
            {
                foreach(var itemList in itemDict.Value)
                {
                    itemList.Value.Clear();
                }
                itemDict.Value.Clear();
            }
            itemIndex.Clear();
            openItemSlots.Clear();

            for(int i = 0; i < numberOfPages; i++)
            {
                openItemSlots.Add(i, new List<int>());

                for(int x = 0; x < itemPages[i].Count; x++)
                {
                    if (x < 10 || x > 49)
                    {
                        continue;
                    }

                    if (!itemPages[i][x].IsAir) 
                    {
                        ItemReference itemRef = new ItemReference(itemPages[i][x], i + 1);
                        int itemID = itemRef.Item.netID;
                        int pageNumber = itemRef.Page;

                        if (itemIndex[itemID] == null)
                        {
                            itemIndex[itemID] = new Dictionary<int, Dictionary<int, ItemReference>>();
                        }
                        if (itemIndex[itemID][pageNumber] == null)
                        {
                            itemIndex[itemID][pageNumber] = new Dictionary<int, ItemReference>();
                        }
                        itemIndex[itemID][pageNumber].Add(x,itemRef);
                    }
                    else
                    {
                        openItemSlots[i].Add(x);
                    }
                }
            }
        }

        public void AddIndexToLibrary(Item item, int pageNumber, int slotIndex)
        {
                if (!item.IsAir)
                {
                    ItemReference itemRef = new ItemReference(item, pageNumber);
                    itemIndex[itemRef.Item.netID][itemRef.Page].Add(slotIndex, itemRef);
                }
        }

        public void RemoveIndexFromLibrary(Item item, int pageNumber, int slotIndex)
        {
            if (!item.IsAir)
            {
                itemIndex[item.netID][pageNumber].Remove(slotIndex);
            }
        }

        public bool IsItemSlotAvailable()
        {
            foreach(var pageOfOpenSlots in openItemSlots)
            {
                if(pageOfOpenSlots.Value.Count != 0)
                {
                    return true;
                }
            }

            return false;
        }
        public int GetAvailableItemSlot(bool removeSlot, int firstPageToTry, out int pageNumber)
        {
            if (openItemSlots[firstPageToTry - 1].Count != 0)
            {
                int slotIndex = openItemSlots[firstPageToTry - 1].First<int>();
                if(removeSlot)
                    openItemSlots[firstPageToTry - 1].RemoveAt(slotIndex);
                pageNumber = firstPageToTry;
                return slotIndex;
            }
            else
            {
                foreach(var pageOfOpenSlots in openItemSlots)
                {
                    List<int> page = pageOfOpenSlots.Value;
                    if(page.Count != 0)
                    {
                        int slotIndex = page.First<int>();
                        if(removeSlot)
                            page.RemoveAt(slotIndex);
                        pageNumber = pageOfOpenSlots.Key + 1;
                        return slotIndex;
                    }
                }
            }

            // Should never hit this if we check for items first with isItemSlotAvailable
            pageNumber = 0;
            return -1;
        }

        public bool FindStackableItem(Item item, int firstPageToTry, out int itemSlot, out int pageNumber)
        {
            if (itemIndex[item.netID][firstPageToTry].Count != 0)
            {
                


            }
        }
    }

    public class ItemReference
    {
        public Item Item;
        public int Page;

        public ItemReference(Item item, int page)
        {
            this.Item = item;
            this.Page = page;
        }
        public ItemReference()
        {

        }
    }
}
