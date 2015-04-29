using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiplayerGameTest
{
    class Particle : GameObject 
    {
        float timer=0;
        public Particle(Vector2 position, Vector2 velocity, Vector2 direction, Texture2D sprite)
            : base(position, velocity, direction, sprite)
        {
 
        }

        public override void Update(float deltaTime)
        {
            this.Position += this.Velocity * deltaTime;

            timer += deltaTime;

            if(timer >= 5)
            {
                Destroy(this);
            }
            base.Update(deltaTime);
        }

        public virtual void Damper()
        {
            if (this.Velocity.X > 0.1f)
            {
               Velocity -= new Vector2(0.1f,0);
            }

            if (Velocity.X < -0.1f)
            {
                Velocity += new Vector2(0.1f, 0);
            }

            if (Velocity.Y > 0.1f)
            {
                Velocity -= new Vector2(0,0.1f);
            }

            if (Velocity.Y < -0.1f)
            {
                Velocity += new Vector2(0, 0.1f);
            }
        }
    }
}
