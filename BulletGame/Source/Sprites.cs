using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulletGame
{
    public class Sprite
    {
        private readonly Texture2D _texture;
        public Vector2 Position { get; private set;}
        public Vector2 Origin { get ; private set;}

        public Sprite (Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
            Origin = new(_texture.Width/2, _texture.Height/2);
        }
        public void Draw()
        {

        }

    }
}