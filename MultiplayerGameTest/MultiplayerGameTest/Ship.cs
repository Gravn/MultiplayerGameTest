using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiplayerGameTest
{
    class Ship : GameObject
    {
         public Ship(Vector2 position, Vector2 velocity, Vector2 direction, Texture2D sprite)
            : base(position, velocity, direction, sprite)
        { 
            
        }
    }
}
