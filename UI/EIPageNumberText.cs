using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.GameContent;
using ReLogic.Graphics;

namespace ExpandedInventory.UI
{
    public class EIPageNumberText : UIElement
    {
        public EIPageNumberText()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            EIPlayer modPlayer = Main.LocalPlayer.GetModPlayer<EIPlayer>();
            CalculatedStyle innerDimensions = GetInnerDimensions();
            Vector2 pos = new Vector2(innerDimensions.X, innerDimensions.Y);
            spriteBatch.DrawString(FontAssets.MouseText.Value, modPlayer.GetPageString(), pos, Color.White);
        }
    }
}
