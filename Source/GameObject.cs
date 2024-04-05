using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace CybrEngine {
    public abstract class GameObject {

        private Vector2 _positiion;
        private Texture2D _sprite;

        public Vector2 position{ 
            get{ return _positiion; }
            set{ _positiion = value; }
        }

        public Texture2D sprite {
            get { return _sprite; }
        }

        public virtual void SetSprite(string name){
            _sprite = Assets.GetSprite(name);
        }


        public GameObject() {

        }

        public abstract void Update();
        public virtual void Draw(SpriteBatch spriteBatch){
            spriteBatch.Begin();
            spriteBatch.Draw(sprite, position, null, Color.White, 0f,
            new Vector2(sprite.Width / 2, sprite.Height / 2),
            Vector2.One,
            SpriteEffects.None,
            0f);
            spriteBatch.End();
        }


        protected bool Destroyed;
        protected bool BeingDestroyed;
        
        public bool IsDestroyed {
            get {
                return Destroyed || BeingDestroyed;
            }
        }

        private void Destory(GameObject gameObject){
            Destroyed = true;
        }
    }
}
