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
    public class Sprites
    {
        public Sprites ()
        {

        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle src, int scale)
        {
            Rectangle dest = new((int)position.X * scale, (int)position.Y * scale, 64, 64);
            spriteBatch.Draw(texture, dest, src, Color.White);
        }
    }
}