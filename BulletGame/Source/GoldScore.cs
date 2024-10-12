using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulletGame
{
    public class GoldScore
    {
        private Texture2D texture;
        private SpriteFont font;

        public GoldScore(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;
            this.font = font;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Microsoft.Xna.Framework.Vector2(0, 0), Color.Yellow);
            spriteBatch.DrawString(font, "Gold", new Microsoft.Xna.Framework.Vector2(20, 0), Color.Yellow);
        }
    }
}