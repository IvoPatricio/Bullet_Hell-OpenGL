using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

public enum Direction
{
    Right,
    Left,
    Up,
    Down
}


namespace BulletGame
{
    public class Player
    {
        private int _currentFrameWalk = 0;
        private int _currentFrameIdle = 0;
        private double _timeSinceLastFrame = 0;
        private double _frameTime = 0.1;
        private bool _idleCheck = true;
        private Sprites _sprites;
        private Direction _currentDirection = Direction.Down;
        public Vector2 _position { get; private set; }
        private Texture2D _playerWalkTexture2D;
        private Texture2D _playerIdleTexture2D;

        public Player(Vector2 startPosition, Sprites _sprites)
        {
            this._position = startPosition;
            this._sprites = _sprites;
        }

        public virtual void LoadContent(ContentManager content)
        {
            _playerWalkTexture2D = content.Load<Texture2D>("PlayerWalkSheat");
            _playerIdleTexture2D = content.Load<Texture2D>("PlayerIdleSheat");
        }
        public virtual Vector2 Update(GameTime gameTime)
        {
            Vector2 movement = Vector2.Zero;
            KeyboardState keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Right))
            {
                movement.X += 1;
                _currentDirection = Direction.Right;
            }
            if (keystate.IsKeyDown(Keys.Left))
            {
                movement.X -= 1;
                _currentDirection = Direction.Left;
            }
            if (keystate.IsKeyDown(Keys.Up))
            {
                movement.Y -= 1;
                _currentDirection = Direction.Up;
            }
            if (keystate.IsKeyDown(Keys.Down))
            {
                movement.Y += 1;
                _currentDirection = Direction.Down;
            }
            if (movement.X == 0 && movement.Y == 0)
            {
                _idleCheck = true;
            }
            else
            {
                _idleCheck = false;
            }
            if (movement != Vector2.Zero)
            {
                movement.Normalize();
                movement *= 5;
                _position += movement;
            }
            _timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeSinceLastFrame >= _frameTime && _idleCheck != true)
            {
                _currentFrameWalk = (_currentFrameWalk + 1) % 4;
                _timeSinceLastFrame = 0;
                _currentFrameIdle = 0;
            }
            else if (_timeSinceLastFrame >= _frameTime && _idleCheck == true)
            {
                _currentFrameIdle = (_currentFrameIdle + 1) % 2;
                _timeSinceLastFrame = 0;
                _currentFrameWalk = 0;
            }
            return _position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int row = _currentDirection switch
            {
                Direction.Down => 0,
                Direction.Up => 1,
                Direction.Right => 2,
                Direction.Left => 3,
            };
            if (_idleCheck == false)
            {
                Rectangle src = new Rectangle(_currentFrameWalk * 32, row * 32, 32, 32);
                _sprites.Draw(spriteBatch, _playerWalkTexture2D, _position, src, 1);
            }
            else
            {
                Rectangle src = new Rectangle(_currentFrameIdle * 32, row * 32, 32, 32);
                _sprites.Draw(spriteBatch, _playerIdleTexture2D, _position, src, 1);
            }
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