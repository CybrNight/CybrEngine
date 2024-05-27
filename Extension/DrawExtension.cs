using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine{
    public static class DrawExtension {

        private static Texture2D _blankTexture; 

        public static Texture2D BlankTexture(this SpriteBatch s) {
            if(_blankTexture == null) {
                _blankTexture = new Texture2D(s.GraphicsDevice, 1, 1);
                _blankTexture.SetData(new[] { Color.White });
            }
            return _blankTexture;
        } 

        public static void DrawRect(this SpriteBatch s, Rectangle rect, Color color){
            s.Draw(BlankTexture(s), rect, color);
        }
    }
}
