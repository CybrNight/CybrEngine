using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class Transform {
        
        private Vector2 _position = Vector2.Zero;
        public Vector2 Position { 
            get { return _position; } 
            set {
                _position = value;
                Bounds = new Rectangle(((int)_position.X) + Bounds.X,
                                       ((int)_position.Y) + Bounds.Y,
                                       Bounds.Width, Bounds.Height); 
            } 
        }
        
        public Vector2 Origin { get; set; }

        public Vector2 Velocity { get; set; } 
        public Vector2 Scale { get; set; }
        public Rectangle Bounds { get; set; }

        ~Transform(){

        }

        public Transform(float x, float y) : this(new Vector2(x, y)) { }

        public Transform(Vector2 position = new Vector2()) {
            Position = position;
            Bounds = new Rectangle(0, 0, 32, 32);
            Origin = new Vector2(Bounds.Width / 2, Bounds.Height / 2);
            Scale = Vector2.One;

            Velocity = Vector2.Zero;
        }
    };
}
