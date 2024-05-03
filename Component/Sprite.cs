using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CybrEngine {
    public class Sprite : Component {

        private Sprite() {
            Name = "Sprite";
        }

        public Vector2 Scale { get; set; } = Vector2.One;

        public Vector2 Offset { get; set; } = Vector2.Zero;
        public Texture2D Texture { get; private set; }
        public Color Color { get; set; } = Color.White;

        public Rectangle Bounds => Texture.Bounds;

        public void SetTexture(Texture2D texture) {
            Texture = texture;
        }

        private void _Cleanup() {
            Texture = null;
        }
    };
}
