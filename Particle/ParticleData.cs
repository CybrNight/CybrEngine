using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public  class ParticleData {
        public String TexturePath { get; private set; }
        public Rectangle Bounds { get; set; } = Rectangle.Empty;
        public Color Color { get; set; } = Color.White;
        public float Life { get; set; } = 1.0f;
    }
}
