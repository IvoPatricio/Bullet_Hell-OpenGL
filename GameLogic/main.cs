using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System.Text;

namespace BulletGame
{
    public class Game1 : Game
    {
        Song _song;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MainMenu _mainMenu;
        private GameEngine _gameEngine;
        private StringBuilder emailInput = new StringBuilder();
        private StringBuilder passwordInput = new StringBuilder();
        private string message = string.Empty;
        string _GameState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            //_graphics.IsFullScreen = true;
            this.Window.Title = "Dodge Restarter";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.ApplyChanges();
            _GameState = "Main Menu";
            _mainMenu = new();
            _gameEngine = new GameEngine();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //_song = Content.Load<Song>("audio");
            //MediaPlayer.Play(_song);
            _mainMenu.LoadContent(Content);
            _gameEngine.LoadContent(Content, GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (_GameState == "Start")
            {
                _gameEngine.Update(gameTime, GraphicsDevice);
            }
            else if (_GameState == "Main Menu")
            {
                _GameState = _mainMenu.Update();
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
            if (_GameState == "Start")
            {
                _gameEngine.Draw(_spriteBatch);
            }
            else if (_GameState == "Main Menu")
            {
                _mainMenu.Draw_Main_Menu(_spriteBatch);
            }
            else if (_GameState == "Settings")
            {
                _mainMenu.Draw_Settings(_spriteBatch);
            }
            else if (_GameState == "Credits")
            {
                _mainMenu.Draw_Credits(_spriteBatch);
            }
            else if (_GameState == "Quit")
            {
                Exit();
            }
            else if (_GameState == "Help")
            {
                Exit();
            }
            /*_spriteBatch.Begin();
            _spriteBatch.DrawString(Content.Load<SpriteFont>("menuFont"), $"Email: {emailInput}", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("menuFont"), $"Password: {new string('*', passwordInput.Length)}", new Vector2(10, 30), Color.White); // Obfuscate password
            _spriteBatch.DrawString(Content.Load<SpriteFont>("menuFont"), message, new Vector2(10, 50), Color.White);
            _spriteBatch.End();*/
            base.Draw(gameTime);
        }
    }
}
