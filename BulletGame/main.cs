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
            IsMouseVisible = true;
            _tilemap = LoadMap("../../../maps/map1.csv");
            _textures = new List<Rectangle>() {
                new Rectangle(0, 0, 16, 16),
                new Rectangle(16, 0, 16, 16)
            };
            _camera = new(Vector2.Zero);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.ApplyChanges();
            this._GameState = "Main Menu";
            _menuManager = new MenuManager();
            _mainMenu = new();
            _goldScore = new GoldScore();
            _player = new Player(Vector2.Zero);
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
                _player.Update();
            }
            else if (_GameState == "Settings"){
                Exit();
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
            //_camera.Follow(_player, new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            if (_GameState == "Main Menu")
            {
                _mainMenu.Draw_Main_Menu(_spriteBatch);
            }
            else if (_GameState == "Settings")
                Exit();
            else if (_GameState == "Credits")
                _mainMenu.Draw_Credits(_spriteBatch);
            else if (_GameState == "Quit")
                Exit();
            else if (_GameState == "Help")
                Exit();
            else if (_GameState == "Start")
            {
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
                _player.Draw(_spriteBatch);
                _goldScore.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
