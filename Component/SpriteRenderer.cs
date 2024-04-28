using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class SpriteRenderer : Component, IDrawComponent{

        public SpriteRenderer(){ 
            Name = "SpriteRenderer";
            
        }
        
        public Transform Transform { get; set; }
        public Vector2 Scale { get; set; } = Vector2.One;

        public Vector2 Offset { get; set; }
        public Texture2D Tex { get; private set; }

        public Rectangle Bounds{
            get { return Tex.Bounds; }
        }

        public void SetTexture(string path){
            Tex = Assets.GetTexture(path);
        }

        public override void Destroy(){
            Tex = null;
            Transform = null;
            base.Destroy();
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Tex, Transform.Position, null, Color.White, 0f,
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
