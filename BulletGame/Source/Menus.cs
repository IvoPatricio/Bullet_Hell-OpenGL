using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulletGame
{
    public class MainMenu
    {
        private SpriteFont font;
        private int selectedIndex;
        private string[] menuItems = { "Start", "Settings", "Credits", "Quit", "Help"};

        public MainMenu()
        {
            ;
        }

        public virtual void Update()
        {
            ;
        }

        public void Draw()
        {
            ;
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