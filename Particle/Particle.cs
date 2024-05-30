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
        public Rectangle Bounds { get; set; } = Rectangle.Empty;
        public Color Color { get; set; } = Color.White;
        public float Life { get; set; } = 1.0f;

        public abstract void Draw(SpriteBatch spriteBatch);

        public override bool Equals(object obj) {
            return obj is Particle particle &&
                   base.Equals(obj) &&
                   ID == particle.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(base.GetHashCode(), ID);
        }

        public static implicit operator bool(Particle e) {
            return (e != null);
        }

        public static bool operator ==(Particle left, Particle right) {
            return EqualityComparer<Particle>.Default.Equals(left, right);
        }

        public static bool operator !=(Particle left,Particle right) {
            return !(left == right);
        }

    }
}
