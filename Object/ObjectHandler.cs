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
    internal class ObjectHandler {

        private static List<Entity> entities = new List<Entity>();
        private static List<Transform> transforms = new List<Transform>();
        private static List<ComponentList> components = new List<ComponentList>();

        private static int nextId;

        private static ObjectHandler _instance;
        public static ObjectHandler Instance {
            get {
                if(_instance == null) {
                    _instance = new ObjectHandler();
                }
                return _instance;
            }
        }

        private ObjectHandler() {
            nextId = -1;
        }

        public void Instantiate(Entity entity) {
            entities.Add(entity);
        }

        public int AssignId(){
            return ++nextId;
        }

        public T AddComponent<T>(T component) where T : Component {
            
        }

        public void Update() {
            for(int i = 0; i < entities.Count; i++) {
                Entity e = entities[i];
                if(e.IsDestroyed) {
                    entities.Remove(e);
                }
            }

            for(int i = 0; i < transforms.Count; i++) {
                var t1 = transforms[i];
                for(int j = 0; j < transforms.Count; j++) {
                    var t2 = transforms[j];

                    //Check that t1 and t2 are not same entity
                    if(t1 == t2)
                        continue;
                    if(t1.Intersects(t2)) {
                        Debug.WriteLine("t1 intersect t2");
                    }

                }
            }

            for(int i = 0; i < entities.Count; i++) {
                Entity e = entities[i];
                e.Update();
            }

        }

        public void Draw(SpriteBatch batch) {

            for(int i = 0; i < transforms.Count; i++) {
                var t1 = transforms[i];
                t1.Draw(batch);
            }
        }
    }
}
