using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
                if(e.IsDestroyed){
                    entities.Remove(e);
                }
                e.Update();

                var t1 = e.GetComponent<Transform>();
                if(t1) {
                    Debug.WriteLine("Hello1");
                    for(int j = i + 1; j < entities.Count; j++) {
                        Entity o = entities[j];
                        var t2 = o.GetComponent<Transform>();
                        if (t2) {
                            if (t1.Intersects(t2)){
                                Debug.WriteLine("t1 touch t2");
                            }
                        }
                    }
                }
                
                
            }
        }
    
        public void Draw(SpriteBatch batch){
            for (int i = 0; i < entities.Count;i++){
                Entity e = entities[i];
                e.Draw(batch);
            }
    }
        }    
        }
