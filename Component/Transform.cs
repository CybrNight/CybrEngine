using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class Transform : Component {
        private Vector2 _position;
        private Vector2 _velocity;

        public Vector2 position {
            get { return _position; } 
            set { _position = value; } 
        }

        public Vector2 velocity{
            get { return _velocity; }
            set { _velocity = value; }
        }
    };
}
