using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MultiplayerGameTest
{
    class PlayerShip : GameObject
    {
        public PlayerShip(Vector2 position, Vector2 velocity, Vector2 direction, Texture2D sprite)
            : base(position, velocity, direction, sprite)
        { 
            
        }
        public override void Update(float deltaTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                rotation -= 0.1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                rotation += 0.1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.Velocity += new Vector2((float)Math.Cos((double)rotation) * 5, (float)Math.Sin((double)rotation) * 5);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.Velocity -= new Vector2((float)Math.Cos((double)rotation) * 2, (float)Math.Sin((double)rotation) * 2);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                this.Velocity -= new Vector2((float)Math.Sin((double)-rotation) * 2, (float)Math.Cos((double)-rotation) * 2);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                this.Velocity -= new Vector2((float)Math.Sin((double)rotation) * 2, -(float)Math.Cos((double)rotation) * 2);
            }
            GameManager.cameraPosition += this.Velocity * deltaTime;
            GameManager.GameObjects.Add(new Particle(new Vector2((float)Math.Cos((double)rotation)*-50+this.Position.X, (float)Math.Sin((double)rotation)*-50+this.Position.Y) + GameManager.cameraPosition,this.Velocity, Vector2.Zero, GameManager.spr_particle));
            base.Update(deltaTime);
        }

        public override void Draw(SpriteBatch spriteBatch,float deltaTime)
        {
            spriteBatch.Draw(Sprite, new Vector2(Position.X - Sprite.Width/2,Position.Y - Sprite.Height/2), new Rectangle(0, 0, Sprite.Width, Sprite.Height), Color.White, rotation, new Vector2(Sprite.Width / 2, Sprite.Height / 2),1.0f, SpriteEffects.None, 1);
        }
    }

     
}
