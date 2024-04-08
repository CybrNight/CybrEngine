using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal class Handler {

        private static List<Entity> entities = new List<Entity>();

        private static Handler _instance;
        public static Handler Instance {
            get {
                if(_instance == null){
                    _instance = new Handler();
                }
                return _instance;
            }
        }

        private Handler(){

        }

        public void Instantiate(Entity entity){
            entities.Add(entity);
        }

        public void Update(){
            for(int i = 0; i < entities.Count; i++){
                Entity e = entities[i];
                Console.WriteLine(e.position.ToString());
                if(e.IsDestroyed){
                    entities.Remove(e);
                }

                e.Update();
            }
        }
    
        public void Draw(SpriteBatch spriteBatch){
            for (int i = 0; i < entities.Count;i++){
                Entity e = entities[i];
                var t = e.GetComponent<Transform>();

                if (!t){
                    continue;
                }

                Texture2D sprite = t.sprite;
                Vector2 pos = t.position;

                spriteBatch.Begin();
                spriteBatch.Draw(sprite, pos, null, Color.White, 0f,
                new Vector2(sprite.Width / 2, sprite.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
                spriteBatch.End();
            }
    }
        }    
        }
