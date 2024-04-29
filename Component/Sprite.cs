using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class Sprite : Component, IDrawComponent{

        public Sprite(){ 
            Name = "SpriteRenderer";
            
        }
        
        public Transform Transform { get; set; }
        public Vector2 Scale { get; set; } = Vector2.One;

        public Vector2 Offset { get; set; }
        public Texture2D Texture { get; private set; } 

        public Rectangle Bounds{
            get { return Texture.Bounds; }
        }

        public void SetTexture(string path){
            Texture = Assets.GetTexture(path);
        }

        protected override void _Cleanup (){
            Texture = null;
            Transform = null;
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (Texture == null) return;


            spriteBatch.Draw(Texture, Transform.Position, null, Color.White, 0f,
            Transform.Origin,
            Transform.Scale,
            SpriteEffects.None,
            0f);
        }

        public override Type ComponentType {
            get { return typeof(IDrawComponent); }
        }
    };
}
