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
        public override void OnInitialize()
        {
            NextPageButton = new EIPageButton(ModContent.Request<Texture2D>("ExpandedInventory/Resources/NextPageButton"), true);
            PrevPageButton = new EIPageButton(ModContent.Request<Texture2D>("ExpandedInventory/Resources/PreviousPageButton"), false);
            PageNumberText = new EIPageNumberText();

            setButtonProperties(NextPageButton, true);
            setButtonProperties(PrevPageButton, false);
            setTextProperties();

            
            Append(NextPageButton);
            Append(PrevPageButton);
            Append(PageNumberText);
        }

        private void setTextProperties()
        {
            PageNumberText.Left.Set(Main.screenWidth - PageNumberText.Width.Pixels - 1500, 0f);
            PageNumberText.Top.Pixels = Main.screenHeight - 630;
        }
        private void setButtonProperties(EIPageButton button, bool direction)
        {
            button.Width.Set(30, 0f);
            button.Height.Set(35, 0f);
            if(direction)
                button.Left.Set(Main.screenWidth - 1450, 0f);
            else
                button.Left.Set(Main.screenWidth - 1550, 0f);
            button.Top.Pixels = Main.screenHeight - 630;
            button.OnClick += (a, b) => changePage(button.IsNextButton);
        }

        public override void Update(GameTime gameTime)
        {
            this.AddOrRemoveChild(NextPageButton, Main.playerInventory);
            this.AddOrRemoveChild(PrevPageButton, Main.playerInventory);
            this.AddOrRemoveChild(PageNumberText, Main.playerInventory);
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
