using Microsoft.Xna.Framework;

namespace CybrEngine {
    public class Transform {
        private Rectangle _bounds;

        public Vector2 Origin { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Scale { get; set; }
        public Rectangle Bounds {
            get { return new Rectangle((int)Position.X, (int)Position.Y, 32*(int)Scale.X, (int)Scale.Y*32); }

        }

        public void Translate(Vector2 translation) {
            Position += translation;
        }

        public Transform(): this(0, 0){

        }

        public Transform(float x = 0, float y = 0) : this(new Vector2(x, y)) { }

        public Transform(Vector2 position) {
            Position = position;
            Origin = new Vector2(Bounds.Width / 2, Bounds.Height / 2);
            Scale = Vector2.One;

            Velocity = Vector2.Zero;
        }
    };
}
