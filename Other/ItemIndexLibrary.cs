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
        private Dictionary<string,ItemReference> itemIndex;
        private Dictionary<int, List<int>> openItemSlots;
    }

    public class ItemReference
    {
        private Item item;
        private int page;

        public ItemReference(Item item, int page)
        {
            this.item = item;
            this.page = page;
        }
        public ItemReference()
        {

        }
    }
}
