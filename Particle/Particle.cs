using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract partial class Particle : Object {

        public Transform Transform { get; set; } = new Transform();
        public Texture2D Texture { get; private set; }
        public Rectangle Bounds { get; set; } = Rectangle.Empty;
        public Color Color { get; set; } = Color.White;
        public float Life { get; set; } = 1.0f;

        public virtual void Draw(SpriteBatch spriteBatch){ }
        public virtual void Update() { }

        private void _Awake() {

        }

        /// <summary>
        /// Sets Particle Texture to one from Assets based on name
        /// </summary>
        /// <param name="name"></param>
        public void SetTexture(string name) {
            Texture = Assets.GetTexture(name);
        }

        public override bool Equals(object obj) {
            return obj is Particle particle &&
                   base.Equals(obj) &&
                   ID == particle.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(base.GetHashCode(), ID);
        }


        public static bool operator ==(Particle left, Particle right) {
            return EqualityComparer<Particle>.Default.Equals(left, right);
        }

        public static bool operator !=(Particle left, Particle right) {
            return !(left == right);
        }

    }
}