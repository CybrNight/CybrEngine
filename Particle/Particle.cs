using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Particle : Object{
        
        public Transform Transform { get; set; } = new Transform();
        public Rectangle Bounds { get; set; }
        public Color Color { get; set; }
        public float Life { get; set; }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
