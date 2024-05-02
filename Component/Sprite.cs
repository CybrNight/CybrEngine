using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CybrEngine {
    public class Sprite : Component, IDrawComponent {

        public Sprite() {
            Name = "Sprite";
        }

        public Transform Transform { get; set; }
        public Vector2 Scale { get; set; } = Vector2.One;

        public Vector2 Offset { get; set; } = Vector2.Zero; 
        public Texture2D Texture { get; private set; }
        public Color Color { get; set; } = Color.White;

        public Rectangle Bounds {
            get { return Texture.Bounds; }
        }

        public void SetTexture(string path) {
            Texture = Assets.GetTexture(path);
        }

        protected override void _Cleanup() {
            Texture = null;
            Transform = null;
        }
    };
}
