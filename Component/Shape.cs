using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CybrEngine {
    public class Shape : Component {

        private Shape() {
            Name = "Shape";
            SetTexture(Assets.GetTexture("blank"));
        }

        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Offset { get; set; } = Vector2.Zero;
        public Texture2D ShapeTex { get; private set; }
        public Color Color { get; set; } = Color.White;

        public Rectangle Bounds => ShapeTex.Bounds;

        public void SetTexture(Texture2D texture) {
            ShapeTex = texture;
        }

        /// <summary>
        /// Draws stored Texture
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch){
            if(ShapeTex == null) return;

            var transform = Entity.Transform;
            spriteBatch.Draw(ShapeTex, transform.Position, null, Color, 0f,
            transform.Origin,
            transform.Scale,
            SpriteEffects.None,
            0f);
        }
    };
}
