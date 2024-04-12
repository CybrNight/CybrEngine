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
        private static List<Transform> transforms = new List<Transform>();
        private static List<IComponentUpdate>

        private static Handler _instance;
        public static Handler Instance {
            get {
                if(_instance == null) {
                    _instance = new Handler();
                }
                return _instance;
            }
        }

        private Handler() {

        }

        public void Instantiate(Entity entity) {
            entities.Add(entity);

            var transform = entity.GetComponent<Transform>();
            if(transform) {
                transforms.Add(transform);
            }
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
