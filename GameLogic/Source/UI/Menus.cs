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
    public class MenuManager
    {
        private static MenuManager _instance;
        protected Texture2D _MainBackgroundTexture2D;
        protected Texture2D _PauseBackgroundTexture2D;
        protected SpriteFont _font;
        protected KeyboardState _previousKeyState;
        protected MouseState _previousMouseState;
        protected bool _contentLoaded = false;
        protected int _menuIndex;

        protected MenuManager()
        {
            // Private(Protected) constructor
        }

        private MenuManager(MenuManager other)
        {
            // Private copy constructor
        }

        public static MenuManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MenuManager();
            }
            return _instance;
        }
        public virtual void LoadContent(ContentManager content)
        {
            _MainBackgroundTexture2D = content.Load<Texture2D>("MainBackground");
            _PauseBackgroundTexture2D = content.Load<Texture2D>("PauseBackground");
            _font = content.Load<SpriteFont>("menuFont");
            _menuIndex = 0;
            _contentLoaded = true;
        }
    }
    public class MainMenu : MenuManager
    {
        private string[] _menuItems = {"Start", "Settings", "Credits", "Quit", "Help"};
        private List<Vector2> _menuPositions = new List<Vector2>();

        ~MainMenu()
        {
            ;
        }
        public override void LoadContent(ContentManager content)
        {
            if (_contentLoaded == false)
                base.LoadContent(content);
            int startY = (720 - (_menuItems.Length * 40)) / 2;
            for (int i = 0; i < _menuItems.Length; i++)
            {
                string menuItem = _menuItems[i];
                Vector2 textSize = _font.MeasureString(menuItem);
                float posX = (1280 - textSize.X) / 2;
                float posY = startY + i * 40;
                _menuPositions.Add(new Vector2(posX, posY));
            }
        }
        
        public string Update()
        {
            KeyboardState keystate = Keyboard.GetState();
            MouseState mousestate = Mouse.GetState();

            for (int i = 0; i < _menuItems.Length; i++)
            {
                if (mousestate.X >= _menuPositions[i].X &&
                    mousestate.X <= _menuPositions[i].X + _font.MeasureString(_menuItems[i]).X &&
                    mousestate.Y >= _menuPositions[i].Y &&
                    mousestate.Y <= _menuPositions[i].Y + _font.MeasureString(_menuItems[i]).Y)
                {
                    _menuIndex = i;
                    if (mousestate.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                    {
                        return _menuItems[_menuIndex];
                    }
                }
            }

            if (keystate.IsKeyDown(Keys.Up) && _previousKeyState.IsKeyUp(Keys.Up))
            {
                _menuIndex--;
                if (_menuIndex < 0)
                    _menuIndex = _menuItems.Length - 1;
            }
            else if (keystate.IsKeyDown(Keys.Down) && _previousKeyState.IsKeyUp(Keys.Down))
            {
                _menuIndex++;
                if (_menuIndex >= _menuItems.Length)
                    _menuIndex = 0;
            }
            else if (keystate.IsKeyDown(Keys.Enter) && _previousKeyState.IsKeyUp(Keys.Enter))
            {
                return _menuItems[_menuIndex];
            }
            _previousKeyState = keystate;
            _previousMouseState = mousestate;
            return "Main Menu";
        }

        public void Draw_Main_Menu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            Rectangle destRectangle = new Rectangle(0, 0, 1280, 720);
            spriteBatch.Draw(_MainBackgroundTexture2D, destRectangle, Color.White);
            for (int i = 0; i < _menuItems.Length; i++)
            {
                spriteBatch.DrawString(_font, _menuItems[i], _menuPositions[i], Color.White);
            }
            spriteBatch.DrawString(_font, _menuItems[_menuIndex], _menuPositions[_menuIndex], Color.Yellow);
            spriteBatch.End();
        }

        public String Update_Credits()
        {
            KeyboardState keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Escape) && _previousKeyState.IsKeyUp(Keys.Escape))
            {
                return "Main Menu";
            }
            return "Credits";
        }
        public void Draw_Credits(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, "Credits!", new Vector2(400, 400), Color.Yellow);
            spriteBatch.End();
        }

        public void Draw_Help(SpriteBatch spriteBatch)
        {

        }

        public void Draw_Settings(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            Rectangle destRectangle = new Rectangle(0, 0, 1280, 720);
            spriteBatch.Draw(_PauseBackgroundTexture2D, destRectangle, Color.White);
            spriteBatch.End();
        }
    }
    
    public class PauseMenu : MenuManager
    {
        private SpriteFont font;
        private int menuIndex;
        private string[] menuItems = { "Resume", "Settings", "Main Menu","Quit" };
    }
    public class SettingsMenu : MenuManager
    {
        private SpriteFont font;
        private int menuIndex;
        private string[] menuItems = { "Graphics", "Resolution", "Controls", "Audio" };
        //Graphics
            // Normal / Low-end
        //resolution
            //fullscreen / window 
            // 2560x1440 1920x1080 1600x900 1366x768 1360x768 1280x720 1152x648 1024x576 960x540
        //controls
            //Key Rebinder / Mouse sensitivity / Invert Mouse Y-Axis
        //Audio Settings
            //Master Volume / Music Volume / Sound Effects Volume
    }
    public class GameOverMenu : MenuManager
    {
        private int menuIndex;
        private string[] menuItems = { "Restart", "Return Main Menu", "Credits","Quit" };
    }
}