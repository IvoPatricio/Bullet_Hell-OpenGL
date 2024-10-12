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
            Position = startPosition;
            this.texture = texture;
        }

        public void Update()
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

            Position += movement;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}