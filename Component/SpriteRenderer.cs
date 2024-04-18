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

        public SpriteRenderer(Texture2D tex){
            Tex = tex;
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

        public void Draw(SpriteBatch batch) { 
            batch.Begin();
            batch.Draw(Tex, Transform.Position, null, Color.White, 0f,
            new Vector2(0, 0),
            Scale,
            SpriteEffects.None,
            0f);
            batch.End();
        }

        public override Type ComponentGroup {
            get { return typeof(IDrawComponent); }
        }

        public Entity Entity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    };
}
