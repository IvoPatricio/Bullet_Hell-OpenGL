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
    public class Player
    {
        public Vector2 Position { get; private set; }
        private Texture2D _playerTexture2D;

        public Player(Vector2 startPosition)
        {
            this.Position = startPosition;
        }

        public virtual void LoadContent(ContentManager content)
        {
            _playerTexture2D = content.Load<Texture2D>("playerArtFront");
        }
        public virtual Vector2 Update(GameTime gameTime)
        {
            Vector2 movement = Vector2.Zero;
            KeyboardState keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Right))
                movement.X += 1;
            if (keystate.IsKeyDown(Keys.Left))
                movement.X -= 1;
            if (keystate.IsKeyDown(Keys.Up))
                movement.Y -= 1;
            if (keystate.IsKeyDown(Keys.Down))
                movement.Y += 1;
            if (movement != Vector2.Zero)
                movement.Normalize();
            movement *= 5;
            Position += movement;
            return Position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle dest = new(
                        (int)Position.X,
                        (int)Position.Y,
                        64,
                        64
                    );
            Rectangle src = new Rectangle(0, 0, 32, 32);
            spriteBatch.Draw(_playerTexture2D, dest, src, Color.White);
        }
    }

/*    public class Warrior : Player
    {
        public int Health { get; private set; }
        private Texture2D _textureClass;

        public Warrior(Vector2 startPosition, Texture2D texture) : base(startPosition)
        {
            this.Health = 150;
        }

        public override Vector2 Update()
        {
            base.Update();
        }
    }

    public class Archer : Player
    {
        public int Health { get; private set; }
        private Texture2D _textureClass;

        public Archer(Vector2 startPosition, Texture2D texture) : base(startPosition)
        {
            this.Health = 100;
        }

        public override Vector2 Update()
        {
            base.Update();
        }
    }

    public class Wizard : Player
    {
        public int Health { get; private set; }
        private Texture2D _textureClass;

        public Wizard(Vector2 startPosition, Texture2D texture)
            : base(startPosition)
        {
            this.Health = 80;
        }

        public override Vector2 Update()
        {
            base.Update();
        }
    }*/
}