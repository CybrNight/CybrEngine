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

        private Texture2D _sprite;

        public Transform() { Name = "Transform"; }

        public Transform(Vector2 position) {
            Position = position;
            Unique = true;
        }

        public Transform(float x, float y) : this(new Vector2(x, y)) { }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Scale { get; set; }
        public Rectangle Bounds { get; set; }

        public Texture2D sprite {
            get { return _sprite; }
        }

        public override void Update(){
            Position += Velocity;
        }

        public bool Intersects(Transform other) {
            return Bounds.Intersects(other.Bounds);
        }

        public void SetSprite(string path) {
            _sprite = Assets.GetTexture(path);

            Bounds = _sprite.Bounds;
        }

        public void Draw(SpriteBatch batch) {
            batch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            RasterizerState state = new RasterizerState();
            state.FillMode = FillMode.WireFrame;
            batch.GraphicsDevice.RasterizerState = state;

            //loop this for all sprites!
            batch.Draw(sprite, Position, Color.White);
            batch.End();

            batch.Begin();
            batch.Draw(sprite, Position, null, Color.White, 0f,
            new Vector2(0, 0),
            _scale,
            SpriteEffects.None,
            0f);
            batch.End();
        }

        public override Type ComponentGroup {
            get { return typeof(IComponent); }
        }

        public Entity Entity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    };
}
