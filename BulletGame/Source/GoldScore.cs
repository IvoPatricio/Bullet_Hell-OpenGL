using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;


namespace BulletGame
{
    public class GoldScore
    {
        private Texture2D _goldCoinTexture2D;
        private SpriteFont _fontSpriteFont;

        public GoldScore()
        {
        }

        public void LoadContent(ContentManager content)
        {
            this._goldCoinTexture2D = content.Load<Texture2D>("diamant");;
            this._fontSpriteFont = content.Load<SpriteFont>("defaultFont");
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_goldCoinTexture2D, new Microsoft.Xna.Framework.Vector2(0, 0), Color.Yellow);
            spriteBatch.DrawString(_fontSpriteFont, "Gold", new Microsoft.Xna.Framework.Vector2(20, 0), Color.Yellow);
        }
    }
}