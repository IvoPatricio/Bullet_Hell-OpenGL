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
        private double _frameTime = 0.15;
        private int _maxHealth = 100;
        private int _health;
        private int _hitboxHeight;
        private int _hitboxWidth;
        private int _hitboxOffsetY;
        private int _hitboxOffsetX;
        private Rectangle _hitbox;
        private bool _idleCheck = true;
        private Direction _currentDirection = Direction.Down;
        private Sprites _sprites;
        public Vector2 _position { get; private set; }
        private Texture2D _playerWalkTexture2D;
        private Texture2D _playerIdleTexture2D;
        private Texture2D _hitboxTexture;
        private readonly Dictionary<Vector2, int> _collisions;
        public Player(Vector2 startPosition, Sprites sprites, Dictionary<Vector2, int> collisions)
        {
            this._collisions = collisions;
            this._position = startPosition;
            this._sprites = sprites;
            this._health = _maxHealth;

            //HITBOX Values
            this._hitboxHeight = 52;
            this. _hitboxWidth = 32;
            this._hitboxOffsetY = ((64 - _hitboxHeight) / 2) + 4;
            this._hitboxOffsetX = (64 - _hitboxWidth) / 2;
        }

        public virtual void LoadContent(ContentManager content, GraphicsDevice GraphicsDevice)
        {
            _playerWalkTexture2D = content.Load<Texture2D>("PlayerWalkSheat");
            _playerIdleTexture2D = content.Load<Texture2D>("PlayerIdleSheat");
            _hitboxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _hitboxTexture.SetData(new[] { Color.White });
        }
        public virtual Vector2 Update(GameTime gameTime)
        {
            Vector2 movement = UpdatePlayerMovementDirection();
            UpdatePlayerMovementAndCollision(movement);
            UpdatePlayerAnimation(gameTime);
            UpdatePlayerHitbox();
            return _position;
        }

        private Vector2 UpdatePlayerMovementDirection()
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
            return movement;
        }

        private void UpdatePlayerMovementAndCollision(Vector2 movement)
        {
            if (movement != Vector2.Zero)
            {
                movement.Normalize();
                movement *= 5;
                Vector2 newPosition = _position;
                newPosition.X += movement.X;
                CheckPlayerHitBox(newPosition);
                if (CheckPlayerMapCollision() == false)
                {
                    _position = newPosition;
                }
                else
                {
                    if (_currentDirection == Direction.Right)
                    {
                        _position = new Vector2(newPosition.X - 14, _position.Y);
                    }
                    else if (_currentDirection == Direction.Left)
                    {
                        _position = new Vector2(newPosition.X + 14, _position.Y);
                    }
                    Console.WriteLine("Collision detected - X AXIS");
                }
                newPosition.Y += movement.Y;
                CheckPlayerHitBox(newPosition);
                if (CheckPlayerMapCollision() == false)
                {
                    _position = newPosition;
                }
                else
                {
                    Console.WriteLine("Collision detected- Y AXIS");
                }
            }
        }

        private void UpdatePlayerAnimation(GameTime gameTime)
        {
            _timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (_timeSinceLastFrame >= _frameTime)
            {
                if (_idleCheck == false)
                {
                    _currentFrameWalk = (_currentFrameWalk + 1) % 4;
                    _currentFrameIdle = 0;
                    _timeSinceLastFrame = 0;
                }
                else
                {
                    _currentFrameIdle = (_currentFrameIdle + 1) % 2;
                    _currentFrameWalk = 0;
                    _timeSinceLastFrame = 0;
                }
            }
        }

        private void CheckPlayerHitBox(Vector2 HitboxCheck)
        {
            _hitboxWidth = 32;
            _hitboxOffsetX = (64 - _hitboxWidth) / 2;
            if (_currentDirection == Direction.Right || _currentDirection == Direction.Left) //Looking RIGHT or LEFT
            {
                _hitboxWidth = 28;
                _hitboxOffsetX = (64 - _hitboxWidth) / 2;
                if (_currentDirection == Direction.Right)
                {
                    _hitboxOffsetX -= 2;
                }
                else
                {
                    _hitboxOffsetX += 4;
                }
            }
            _hitbox = new Rectangle((int)HitboxCheck.X + _hitboxOffsetX, (int)HitboxCheck.Y + _hitboxOffsetY, _hitboxWidth, _hitboxHeight);
        }
        
        private void UpdatePlayerHitbox()
        {
            _hitboxWidth = 32;
            _hitboxOffsetX = (64 - _hitboxWidth) / 2;
            if (_currentDirection == Direction.Right || _currentDirection == Direction.Left) //Looking RIGHT or LEFT
            {
                _hitboxWidth = 28;
                _hitboxOffsetX = (64 - _hitboxWidth) / 2;
                if (_currentDirection == Direction.Right)
                {
                    _hitboxOffsetX -= 2;
                }
                else
                {
                    _hitboxOffsetX += 4;
                }
            }
            _hitbox = new Rectangle((int)_position.X + _hitboxOffsetX, (int)_position.Y + _hitboxOffsetY, _hitboxWidth, _hitboxHeight);
        }

        private bool CheckPlayerMapCollision()
        {
            foreach (var tile in _collisions)
            {
                Vector2 col_position = tile.Key;
                int value = tile.Value;
                if (tile.Value == -1)
                {
                    Rectangle dest = new Rectangle((int)col_position.X * 64, (int)col_position.Y * 64, 64, 64);
                    if (_hitbox.Intersects(dest))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int row = GetPlayerSpriteRowByDirection();
            DrawPlayerWalkingIdleTextures(spriteBatch, row);
            DrawPlayerHitbox(spriteBatch);
        }

        private int GetPlayerSpriteRowByDirection()
        {
            int row = _currentDirection switch
            {
                Direction.Down => 0,
                Direction.Up => 1,
                Direction.Right => 2,
                Direction.Left => 3,
                _=> 0
            };
            return row;
        }

        private void DrawPlayerWalkingIdleTextures(SpriteBatch spriteBatch, int row)
        {
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
        private void DrawPlayerHitbox(SpriteBatch spriteBatch)
        {
            //UP & DOWN
            spriteBatch.Draw(_hitboxTexture, new Rectangle(_hitbox.Left, _hitbox.Top, _hitbox.Width, 2), Color.Red);
            spriteBatch.Draw(_hitboxTexture, new Rectangle(_hitbox.Left, _hitbox.Bottom - 2, _hitbox.Width, 2), Color.Red);
            //LEFT & RIGHT
            spriteBatch.Draw(_hitboxTexture, new Rectangle(_hitbox.Left, _hitbox.Top, 2, _hitbox.Height), Color.Red);
            spriteBatch.Draw(_hitboxTexture, new Rectangle(_hitbox.Right - 2, _hitbox.Top, 2, _hitbox.Height), Color.Red);
        }
        private void DrawPlayerHealth(SpriteBatch spriteBatch)
        {
            ;
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