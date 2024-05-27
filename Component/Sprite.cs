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

        public override void Draw(SpriteBatch spriteBatch){
            if(Texture == null) return;

            var transform = Entity.Transform;
            spriteBatch.Draw(Texture, transform.Position, null, Color, 0f,
            transform.Origin,
            transform.Scale,
            SpriteEffects.None,
            0f);
        }

        private void _Cleanup() {
            Texture = null;
        }
    };
}
