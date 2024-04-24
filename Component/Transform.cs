using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class Transform : Component {
        private Vector2 _scale = Vector2.One;
        private Vector2 _position = Vector2.Zero;

        public Transform() { Name = "Transform"; }
     

        public Transform(Vector2 position) {
            Position = position;
            Unique = true;
        }

        public Transform(float x, float y) : this(new Vector2(x, y)) { }

        public Vector2 Position { get { return _position; } set { UpdatePosition(value); } }
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public Rectangle Bounds { get; private set; } 

        public override void Update() {
            Position += Velocity;
        }

        private void UpdatePosition(Vector2 pos){
            _position = pos;
            Bounds = new Rectangle(((int)Position.X), ((int)Position.Y), 32, 32);
        }

        public bool Intersects(Transform other) {
            return Bounds.Intersects(other.Bounds);
        }

        public void OnIntersection(Transform other){
            Entity.OnIntersection(other.Entity);
        }

        public override Type ComponentType {
            get { return typeof(IComponent); }
        }
    };
}
