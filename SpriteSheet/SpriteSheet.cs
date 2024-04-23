using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class SpriteSheet {

        public Texture2D Source { get; private set; }

        private SpriteSheet(){ }
        public SpriteSheet(string path){
            SetSheet(path);
        }

        private void SetSheet(string path) {
            Source = Assets.GetTexture(path);          
        }

        public void Destroy(){
            Source.Dispose();
        }


    }
}
