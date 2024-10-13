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
        private string[] _gameStates = { "Main Menu", "Playing", "Paused", "Quit", "Help", "Settings", "Game Over"};

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            //_graphics.IsFullScreen = true;
            this.Window.Title = "Dodge Restarter";
            IsMouseVisible = true;
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
            _player.Draw(_spriteBatch);
            _goldScore.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
