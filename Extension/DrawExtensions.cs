using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine{
    public static class DrawExtensions {

        private static Texture2D _blankTexture; 

        /// <summary>
        /// Creates a blank texture
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Texture2D BlankTexture(this SpriteBatch s) {
            if(_blankTexture == null) {
                _blankTexture = new Texture2D(s.GraphicsDevice, 1, 1);
                _blankTexture.SetData(new[] { Color.White });
            }
            return _blankTexture;
        }

        /// <summary>
        /// Draws rectangle
        /// </summary>
        /// <param name="s"></param>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        /// <param name="layer"></param>
        public static void DrawRect(this SpriteBatch s, Rectangle rect, Color color, Vector2 origin, float layer = 0.0f){
            s.Draw(BlankTexture(s), rect, rect, color, 0f, origin, SpriteEffects.None, layer);
        }

        public static void DrawRect(this SpriteBatch s, Rectangle rect, Color color, Vector2 origin, Vector2 scale, float layer = 0.0f) {
            DrawRect(s, rect, color, Vector2.Zero, layer);
        }

        public static void DrawRect(this SpriteBatch s, Rectangle rect, Color color, float layer = 0.0f) {
            DrawRect(s, rect, color, Vector2.Zero, layer);
        }

        public static Color Invert(this Color color){
            return new Color(255 - color.R, 255 - color.G, 255 - color.B);
        }
    }
}
