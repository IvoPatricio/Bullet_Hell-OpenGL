using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BulletGame
{
    public class MainMenu
    {
        private SpriteFont font;
        private int _menuIndex;
        private string[] _menuItems = {"Start", "Settings", "Credits", "Quit", "Help"};
        private List<Vector2> _menuPositions = new List<Vector2>();
        private KeyboardState _previousKeyState;

        public MainMenu(SpriteFont font)
        {
            this.font = font;
            this._menuIndex = 0;

            int startY = (720 - (_menuItems.Length * 40)) / 2;
            for (int i = 0; i < _menuItems.Length; i++)
            {
                string menuItem = _menuItems[i];
                Vector2 textSize = font.MeasureString(menuItem);
                float posX = (1280 - textSize.X) / 2;
                float posY = startY + i * 40;
                _menuPositions.Add(new Vector2(posX, posY));
            }
        }
        
        public virtual string Update()
        {
            KeyboardState keystate = Keyboard.GetState();

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
            return "Main Menu";
        }

        public void Draw_Main_Menu(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _menuItems.Length; i++)
            {
                spriteBatch.DrawString(font, _menuItems[i], _menuPositions[i], Color.White);
            }
            spriteBatch.DrawString(font, _menuItems[_menuIndex], _menuPositions[_menuIndex], Color.Yellow);
        }

        public virtual String Update_Credits()
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
            spriteBatch.DrawString(font, "Credits: Ivo Marques", new Vector2(400, 400), Color.Yellow);
        }

        public void Draw_Help(SpriteBatch spriteBatch)
        {

        }

        public void Draw_Settings(SpriteBatch spriteBatch)
        {

        }
    }
    
    public class PauseMenu
    {
        private SpriteFont font;
        private int menuIndex;
        private string[] menuItems = { "Resume", "Settings", "Main Menu","Quit" };
    }
    public class SettingsMenu
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
    public class GameOverMenu
    {
        private SpriteFont font;
        private int menuIndex;
        private string[] menuItems = { "Restart", "Return Main Menu", "Credits","Quit" };
    }
}