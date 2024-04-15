using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CybrEngine {
    public class Assets {
        private static Dictionary<string, Texture2D> sprites = new Dictionary<string, Texture2D>();
        public static ContentManager Content;

        public static void LoadSprite(string name, string path) {
            Texture2D sprite = Content.Load<Texture2D>(path);
            sprites.Add(name, sprite);
        }

        public static void RemoveSprite(string name) { 
            var sprite = sprites[name];
            if (sprite != null) {
                sprites.Remove(name);
                sprite.Dispose();
            }
        }

        public static Texture2D GetTexture(string name){
            if (sprites.ContainsKey(name)) return sprites[name];
            else {
                throw new NullReferenceException(name + " does not exist in the Asset store");
            }
        }
    }
}
