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
    public class Game1 : Game
    {
        Texture2D _textureatlas;
        Song _song;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MenuManager _menuManager;
        private MainMenu _mainMenu;
        private Player _player;
        private GoldScore _goldScore;
        private Camera _camera;
        private Sprites _sprites;
        private Vector2 _playerPosition;

        private Dictionary<Vector2, int> _tilemap;
        private List<Rectangle> _textures;

        string _GameState;

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
            this._playerPosition = Vector2.Zero;
            IsMouseVisible = true;
            _tilemap = LoadMap("../../../maps/map1.csv");
            _textures = new List<Rectangle>() {
                new Rectangle(0, 0, 16, 16),
                new Rectangle(16, 0, 16, 16)
            };
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.ApplyChanges();
            this._GameState = "Main Menu";
            _sprites = new Sprites();
            _menuManager = new MenuManager();
            _mainMenu = new();
            _goldScore = new GoldScore();
            _player = new Player(_playerPosition, _sprites);
            _camera = new(_playerPosition);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _textureatlas = Content.Load<Texture2D>("floorExamples");
            //_song = Content.Load<Song>("audio");
            //MediaPlayer.Play(_song);
            _player.LoadContent(Content);
            _menuManager.LoadContent(Content);
            _mainMenu.LoadContent(Content);
            _goldScore.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (_GameState == "Main Menu")
            {
                _GameState = _mainMenu.Update();
            }
            else if (_GameState == "Start"){
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                _playerPosition = _player.Update(gameTime);
                _camera.Follow(_playerPosition, new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            }
            else if (_GameState == "Settings"){
                ;
            }
            else if (_GameState == "Credits"){
                _GameState= _mainMenu.Update_Credits();
            }
            else if (_GameState == "Quit"){
                Exit();
            }
            else if (_GameState == "Help"){
                Exit();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (_GameState == "Main Menu")
            {
                _mainMenu.Draw_Main_Menu(_spriteBatch);
            }
            else if (_GameState == "Settings")
                _mainMenu.Draw_Settings(_spriteBatch);
            else if (_GameState == "Credits")
            {
                _mainMenu.Draw_Credits(_spriteBatch);
            }
            else if (_GameState == "Quit")
                Exit();
            else if (_GameState == "Help")
                Exit();
            else if (_GameState == "Start")
            {
                _spriteBatch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null,
                    transformMatrix: Matrix.CreateTranslation(new Vector3(-_camera.position, 0)));
                foreach (var item in _tilemap)
                {
                    Rectangle dest = new((int)item.Key.X * 64, (int)item.Key.Y * 64, 64, 64);
                    if (item.Value > 0 && item.Value <= _textures.Count)
                    {
                        Rectangle src = _textures[item.Value - 1];
                        _spriteBatch.Draw(_textureatlas, dest, src, Color.White);
                    }
                }
                _player.Draw(_spriteBatch);
                _spriteBatch.End();
                //UI elements
                _spriteBatch.Begin();
                _goldScore.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
