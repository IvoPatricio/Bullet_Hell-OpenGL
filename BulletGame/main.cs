using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulletGame
{
    public class Game1 : Game
    {
        Texture2D _ballTexture;
        Texture2D _goldCoin;
        SpriteFont _font;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private GoldScore _goldScore;

        private Dictionary<Vector2, int> _tilemap;
        private List<Rectangle> _textures;
        private string[] _gameStates = { "Main Menu", "Playing", "Paused", "Quit", "Help", "Settings", "Game Over"};

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
                        if (value > 0)
                        {
                            result[new Vector2(x, y)] = value;
                        }
                    }
                }
            }
            return result;
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferWidth = 854;
            _graphics.PreferredBackBufferHeight = 480;
            //_graphics.IsFullScreen = true;
            this.Window.Title = "Dodge Restarter";
            IsMouseVisible = true;
            _tilemap = LoadMap("./maps/map1.csv");
            _textures = new()
            {
                new Rectangle()
            }
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _ballTexture = Content.Load<Texture2D>("ball");
            _goldCoin = Content.Load<Texture2D>("diamant");
            _font = Content.Load<SpriteFont>("defaultFont");
            _goldScore = new GoldScore(_goldCoin, _font);
            _player = new Player(Vector2.Zero, _ballTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            
            _player.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            //https://www.youtube.com/watch?v=K_I7dZM0zw4   PIXEL ART SHARP
            _player.Draw(_spriteBatch);
            _goldScore.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
