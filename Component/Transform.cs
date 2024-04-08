using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class Transform : Component {
        private Vector2 _position;
        private Vector2 _velocity;
        private Vector2 _scale;
        private Texture2D _sprite;

        public Vector2 position {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 velocity {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Vector2 scale{
            get { return scale; }
            set{ scale = value; }
        }

        public Texture2D sprite {
            get { return _sprite; }
        }

        public void SetSprite(string path){
            _sprite = Assets.GetSprite(path);
        }

        public override Type CType {
            get { return typeof(Transform); }
        }
    };
}
