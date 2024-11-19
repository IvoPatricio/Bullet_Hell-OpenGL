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
    public class Camera
    {
        public Vector2 position;

        public Camera(Vector2 startposition)
        {
            this.position = startposition;
        }
        public void Follow(Vector2 targetPosition, Vector2 screenSize)
        {
            position = new Vector2(
                targetPosition.X - (screenSize.X / 2),
                targetPosition.Y - (screenSize.Y / 2)
            );
        }
    }
}