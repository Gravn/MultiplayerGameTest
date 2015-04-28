using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiplayerGameTest
{
    public abstract class GameObject
    {
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 direction;
        private Texture2D sprite;
        private Rectangle collisionBox;
        private float mass;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { position = value; }
        }

        public Vector2 Direction
        {
            get { return velocity; }
            set { position = value; }
        }

        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public Rectangle CollisionBox
        {
            get { return collisionBox; }
            set { collisionBox = value; }
        }

        public float rotation;


        public GameObject(Vector2 position,Vector2 velocity,Vector2 direction,Texture2D sprite)
        {
            this.position = position;
            this.velocity = velocity;
            this.direction = direction;
            this.sprite = sprite;
        }

        public GameObject(Vector2 position, Vector2 velocity, float rotation,Texture2D sprite)
        { 
            
        }

        public virtual void Update(float deltaTime)
        {
            CheckCollision();
            UpdateAnimation(deltaTime);
        }

        public void CheckCollision()
        { 
            
        }

        public void UpdateAnimation(float deltaTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch,float deltaTime)
        {
            spriteBatch.Draw(Sprite, new Vector2((int)position.X - sprite.Width/2, (int)position.Y - sprite.Height/2), new Rectangle(0, 0, Sprite.Width, Sprite.Height), Color.White, rotation, new Vector2(sprite.Width / 2, sprite.Height / 2),1.0f, SpriteEffects.None, 1);
        }

        public virtual void OnCollision(GameObject other)
        {

        }

        public void Destroy()
        { 
            
        }


    }
}
