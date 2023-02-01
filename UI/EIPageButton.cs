using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;

namespace ExpandedInventory.UI
{
    public class EIPageButton : UIImageButton
    {
        private Color color;
        public bool IsNextButton;
        private Asset<Texture2D> buttonTexture;

        public EIPageButton(Asset<Texture2D> texture, bool direction) : base(texture)
        {
            IsNextButton = direction;
            color = new Color(50, 38, 135);
            buttonTexture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }


    }
}
