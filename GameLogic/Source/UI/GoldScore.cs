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
            // Empty constructor
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
            Vector2 goldPosition = new Vector2(10, 10);
            spriteBatch.Draw(_goldCoinTexture2D, goldPosition, Color.Yellow);
            Vector2 textPosition = new Vector2(goldPosition.X + 30, goldPosition.Y);
            spriteBatch.DrawString(_fontSpriteFont, "Gold", textPosition, Color.Yellow);
        }
    }
}