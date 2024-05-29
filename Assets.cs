using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace CybrEngine {
    public static class Assets {
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static ContentManager Content;
        public static GraphicsDevice GraphicsDevice;

        public static Entity LoadObject(string path){
            //If object already loaded, return copy instance
            if (objects.ContainsKey(path)){
                return GameObject.Factory<Entity>.Instance(objects[path]);
            }

            var entity = Content.Load<Entity>("object/" + path);
            entity.AddComponent<Sprite>();

            objects[path] = entity;
            return entity;
        }

        public static void AddTexture(string name, Texture2D texture){
            textures[name] = texture;
        }

        public static SpriteFont LoadSpriteFont(string name, string path){
            SpriteFont font = Content.Load<SpriteFont>(path);
            fonts.Add(name, font);
            return font;
        }

        public static SpriteFont GetSpriteFont(string name){
            if (textures.ContainsKey(name)){
                return fonts[name];
            }
            return null;
        }

        public static Texture2D LoadTexture(string name, string path) {
            Texture2D sprite = Content.Load<Texture2D>(path);
            textures.Add(name, sprite);
            return sprite;
        }

        public static void DisposeTexture(string name) {
            var sprite = textures[name];
            if(sprite != null) {
                textures.Remove(name);
                sprite.Dispose();
            }
        }

        public static Texture2D GetTexture(string name) {
            if(textures.ContainsKey(name)){ 
                return textures[name]; 
            }
            return GetTexture("missing_tex");
        }
    }
}
