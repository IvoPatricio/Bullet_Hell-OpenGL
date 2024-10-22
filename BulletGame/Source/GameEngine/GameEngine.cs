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
    public class GameEngine
    {
        private Vector2 _playerPosition;
        private Player _player;
        private Map _map;
        private GoldScore _goldScore;
        private Camera _camera;
        private Sprites _sprites;
        private readonly Dictionary<Vector2, int> _collisions;

        
        public GameEngine ()
        {
            this._playerPosition = new Vector2(2 * 64, 3 * 64);
            _collisions = LoadMap("../../../maps/levels/testing_collisions.csv");
            _sprites = new Sprites();
            _map = new Map();
            _goldScore = new GoldScore();
            _player = new Player(_playerPosition, _sprites, _collisions);
            _camera = new(_playerPosition);
        }

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            _player.LoadContent(Content, GraphicsDevice);
            _goldScore.LoadContent(Content);
            _map.LoadContent(Content);
        }

        public void Update(GameTime gameTime, GraphicsDevice GraphicsDevice)
        {
            _playerPosition = _player.Update(gameTime);
            _camera.Follow(_playerPosition, new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null,
                    transformMatrix: Matrix.CreateTranslation(new Vector3(-_camera.position, 0)));
            _map.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);
            _spriteBatch.End();
            //UI elements
            _spriteBatch.Begin();
            _goldScore.Draw(_spriteBatch);
            _spriteBatch.End();
        }

        private static Dictionary<Vector2, int> LoadMap(string filepath)
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
