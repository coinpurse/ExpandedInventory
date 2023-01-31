using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using ReLogic.Content;
using Terraria.ModLoader.UI;
using Terraria.GameContent.UI.Elements;

namespace ExpandedInventory.UI
{
    internal class EIUIState : UIState
    {
        public EIPageButton NextPageButton;
        public EIPageButton PrevPageButton;
        public EIPageNumberText PageNumberText;

        private decimal xPos;
        private decimal yPos;
        private decimal xPosChest;
        private decimal yPosChest;
        public override void OnInitialize()
        {
            NextPageButton = new EIPageButton(ModContent.Request<Texture2D>("ExpandedInventory/Resources/NextPageButton"), true);
            PrevPageButton = new EIPageButton(ModContent.Request<Texture2D>("ExpandedInventory/Resources/PreviousPageButton"), false);
            PageNumberText = new EIPageNumberText();

            setButtonProperties(NextPageButton, true);
            setButtonProperties(PrevPageButton, false);
            
            Append(NextPageButton);
            Append(PrevPageButton);
            Append(PageNumberText);
        }


        private void setButtonProperties(EIPageButton button, bool direction)
        {
            button.Width.Set(30, 0f);
            button.Height.Set(35, 0f);
            button.OnClick += (a, b) => changePage(button.IsNextButton);
        }

        public override void Update(GameTime gameTime)
        {
            this.AddOrRemoveChild(NextPageButton, Main.playerInventory);
            this.AddOrRemoveChild(PrevPageButton, Main.playerInventory);
            this.AddOrRemoveChild(PageNumberText, Main.playerInventory);

            yPos = Main.screenHeight * (ExpandedInventory.ClientConfig.PositionY / 100m);
            xPos = Main.screenWidth * (ExpandedInventory.ClientConfig.PositionX / 100m);
            yPosChest = Main.screenHeight * (ExpandedInventory.ClientConfig.PositionYWithChest / 100m);
            xPosChest = Main.screenWidth * (ExpandedInventory.ClientConfig.PositionXWithChest / 100m);

            
            EIPlayer modPlayer = Main.LocalPlayer.GetModPlayer<EIPlayer>();
            if(modPlayer.Player.chest != -1 || Main.npcShop > 0)
            {
                NextPageButton.Top.Pixels = (float)yPosChest;
                PrevPageButton.Top.Pixels = (float)yPosChest;
                PageNumberText.Top.Pixels = (float)yPosChest;

                NextPageButton.Left.Set((float)xPosChest + 50, 0f);
                PrevPageButton.Left.Set((float)xPosChest - 50, 0f);
                PageNumberText.Left.Set((float)xPosChest, 0f);
            }
            else
            {
                NextPageButton.Top.Pixels = (float)yPos;
                PrevPageButton.Top.Pixels = (float)yPos;
                PageNumberText.Top.Pixels = (float)yPos;

                NextPageButton.Left.Set((float)xPos + 50, 0f);
                PrevPageButton.Left.Set((float)xPos - 50, 0f);
                PageNumberText.Left.Set((float)xPos, 0f);
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        private void changePage(bool isNextButton)
        {
            EIPlayer modPlayer = Main.LocalPlayer.GetModPlayer<EIPlayer>();

            if (modPlayer != null)
            {
                modPlayer.ChangePage(isNextButton);
            }
        }
    }
}
