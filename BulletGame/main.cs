using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BulletGame
{
    public class Game1 : Game
    {
        Texture2D _ballTexture;
        Texture2D _goldCoin;
        Texture2D _textureatlas;
        SpriteFont _font;
        SpriteFont _mainFont;
        Song _song;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MainMenu _mainMenu;
        private Player _player;
        private GoldScore _goldScore;
        private Camera _camera;

        private Dictionary<Vector2, int> _tilemap;
        private List<Rectangle> _textures;

        int _indexGameStates;
        private string[] _gameStates = {"Main Menu", "Playing", "Paused", "Quit", "Help", "Settings", "Game Over"};

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
                y++;
            }
            return result;
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            //_graphics.IsFullScreen = true;
            this.Window.Title = "Dodge Restarter";
            IsMouseVisible = true;
            _tilemap = LoadMap("../../../maps/map1.csv");
            _textures = new List<Rectangle>() {
                new Rectangle(0, 0, 16, 16),
                new Rectangle(16, 0, 16, 16)
            };
            _camera = new(Vector2.Zero);
            this._indexGameStates = 0;
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
            _mainFont = Content.Load<SpriteFont>("menuFont");
            _textureatlas = Content.Load<Texture2D>("floorExamples");
            //_song = Content.Load<Song>("audio");
            //MediaPlayer.Play(_song);
            _goldScore = new GoldScore(_goldCoin, _font);
            _player = new Player(Vector2.Zero, _ballTexture);
            _mainMenu = new(_mainFont);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            if (_gameStates[_indexGameStates] == "Main Menu")
            {
                _mainMenu.Update();
            }
            else
            {
                _player.Update();
            }
            //_camera.Follow(_player, new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            if (_gameStates[_indexGameStates] == "Main Menu")
            {
                _mainMenu.Draw(_spriteBatch);
            }
            else
            {
                _player.Draw(_spriteBatch);
                _goldScore.Draw(_spriteBatch);
                //Rectangle dest1 = new Rectangle(0, 0, 64, 64);
                //Rectangle src1 = _textures[0];
                //_spriteBatch.Draw(_textureatlas, dest1, src1, Color.White);
                foreach (var item in _tilemap)
                {
                    Rectangle dest = new(
                        (int)item.Key.X * 64,
                        (int)item.Key.Y * 64,
                        64,
                        64
                    );
                    if (item.Value > 0 && item.Value <= _textures.Count)
                    {
                        Rectangle src = _textures[item.Value - 1];
                        _spriteBatch.Draw(_textureatlas, dest, src, Color.White);
                    }
                    else
                    {
                        Console.WriteLine($"Error: item.Value out of range");
                    }
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
