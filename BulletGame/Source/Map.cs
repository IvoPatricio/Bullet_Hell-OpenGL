using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            /*_tiles = new Sprite [_mapTileSize.X, _mapTileSize.Y];
            List<Texture2D> textures = new(5);
            for (int i = 1; i < 6; i++)
                textures.Add()*/
        }

        public virtual void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}