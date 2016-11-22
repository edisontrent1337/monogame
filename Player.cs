using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MonogameWindows
{
    class Player
    {
        private Vector3 position;
        private Vector3 velocity;
        private Vector3 acceleration;

        private float yawAngle = 0f;

        public Player(Vector3 position)
        {
            this.position = position;
        }

        public void update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += velocity * delta;
        }


    }
}
