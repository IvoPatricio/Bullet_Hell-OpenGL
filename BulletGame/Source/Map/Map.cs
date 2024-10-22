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
        Texture2D _mapTexture2D;
        private readonly Dictionary<Vector2, int> _mg;
        public Map()
        {
            this._mg = LoadMap("../../../maps/levels/testing_mg.csv");
        }
        public void LoadContent(ContentManager content)
        {
            _mapTexture2D = content.Load<Texture2D>("GroundTextures-Sheet");
        }
        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int tiles = 32;
            foreach (var item in _mg)
            {
                Rectangle dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                int x = item.Value % tiles;
                int y = item.Value / tiles;
                Rectangle src = new(x * tiles, y * tiles, tiles, tiles);
                spriteBatch.Draw(_mapTexture2D, dest, src, Color.White);
            }
        }
        private Dictionary<Vector2, int> LoadMap(string filepath)
        {
            Dictionary<Vector2, int> result = new();
            StreamReader reader = new(filepath);
            int y = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');
                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value >= -1)
                        {
                            result[new Vector2(x, y)] = value;
                        }
                    }
                }
                y++;
            }
            return result;
        }
    }
}