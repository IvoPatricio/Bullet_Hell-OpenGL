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
    public class Map
    {
        private readonly Point _mapTileSize = new(4, 3);
        private readonly Sprite[,] _tiles;
        public Point TileSize{ get; private set;}
        public Point MapSize { get; private set;}

        public Map()
        {
            
        }

        public virtual void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}