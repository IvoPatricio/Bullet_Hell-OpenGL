using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulletGame
{
    public class Player
    {
        public Vector2 Position { get; private set; }
        private Texture2D texture;

        public Player(Vector2 startPosition, Texture2D texture)
        {
            this.Position = startPosition;
            this.texture = texture;
        }

        public virtual void Update()
        {
            Vector2 movement = Vector2.Zero;
            KeyboardState keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Right))
                movement.X += 25;
            if (keystate.IsKeyDown(Keys.Left))
                movement.X -= 25;
            if (keystate.IsKeyDown(Keys.Up))
                movement.Y -= 25;
            if (keystate.IsKeyDown(Keys.Down))
                movement.Y +=25;
            if (movement != Vector2.Zero)
                movement.Normalize();
            Position += movement;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }

    public class Warrior : Player
    {
        public int Health { get; private set; }
        private Texture2D _textureClass;

        public Warrior(Vector2 startPosition, Texture2D texture) : base(startPosition, texture)
        {
            this.Health = 150;
        }

        public override void Update()
        {
            base.Update();
        }
    }

    public class Archer : Player
    {
        public int Health { get; private set; }
        private Texture2D _textureClass;

        public Archer(Vector2 startPosition, Texture2D texture) : base(startPosition, texture)
        {
            this.Health = 100;
        }

        public override void Update()
        {
            base.Update();
        }
    }

    public class Wizard : Player
    {
        public int Health { get; private set; }
        private Texture2D _textureClass;

        public Wizard(Vector2 startPosition, Texture2D texture)
            : base(startPosition, texture)
        {
            this.Health = 80;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}