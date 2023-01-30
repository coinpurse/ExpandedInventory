using ExpandedInventory.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExpandedInventory
{
    internal class EISystem: ModSystem
    {
        internal EIUIState eiUI;
        internal UserInterface eiInterface;

        public override void Load()
        {
            eiUI = new EIUIState();
            eiUI.Activate();
            eiInterface = new UserInterface();
            eiInterface.SetState(eiUI);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            eiInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int layerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 3"));
            if (layerIndex != -1)
            {
                layers.Insert(layerIndex, new LegacyGameInterfaceLayer(
                    "ExpandedInventory: Expanded Inventory",
                    delegate
                    {
                        eiInterface?.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
