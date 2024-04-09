using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class Transform : Component, ICDraw, ICUpdate{
        private Vector2 _position;
        private Vector2 _velocity;
        private Vector2 _scale = Vector2.One;
        private Rectangle _bounds = new Rectangle();

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

        public Rectangle bounds{
            get { return _bounds; }
        }

        public bool Intersects(Transform other){
            return bounds.Intersects(other.bounds);
        }

        public void SetSprite(string path){
            _sprite = Assets.GetSprite(path);

            _bounds = _sprite.Bounds;
        }

        public void Update(){
            var delta = Globals.GameTime.ElapsedGameTime;
            position += velocity;
            Debug.WriteLine(velocity + " " + position);


            _bounds = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
        }

        public void Draw(SpriteBatch batch) {
            batch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            RasterizerState state = new RasterizerState();
            state.FillMode = FillMode.WireFrame;
            batch.GraphicsDevice.RasterizerState = state;

            //loop this for all sprites!
            batch.Draw(sprite, position, Color.White);
            batch.End();

            batch.Begin();
            batch.Draw(sprite, position, null, Color.White, 0f,
            new Vector2(0, 0),
            _scale,
            SpriteEffects.None,
            0f);
            batch.End();
        }

        public override Type CType {
            get { return typeof(Transform); }
        }

        public Entity Entity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    };
}
