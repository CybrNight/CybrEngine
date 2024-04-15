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
    public class ObjectHandler {

        private static List<Entity> objectList = new List<Entity>();
        private static Queue<Entity> creationQueue = new Queue<Entity>();

        private static List<ComponentList> components = new List<ComponentList>();

        private static ObjectHandler _instance;
        public static ObjectHandler Instance {
            get {
                if(_instance == null) {
                    _instance = new ObjectHandler();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Method to handle instantiating all queued objects from last cycle
        /// </summary>
        private void InstantiateQueuedObjects(){
            while (creationQueue.Count > 0) {
                var obj = creationQueue.Dequeue();
                obj.AddComponenent(new Transform());
                obj.Awake();
                objectList.Add(obj);
                obj.Start();
            }
            
        }

        /// <summary>
        /// Adds new Entity to creationQueue, creates new ComponentList entry and returns reference to new Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Instantiate(Type eType) {
            Entity entity = (Entity)Activator.CreateInstance(eType);
            creationQueue.Enqueue(entity); 
            components.Add(new ComponentList(entity)); 
        }

        /// <summary>
        /// Returns specified component for CINDEX passed from entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CINDEX"></param>
        /// <returns></returns>
        public T GetComponent<T>(int CINDEX) where T : Component {
            if (CINDEX >= components.Count)
                return null;

            ComponentList cList = components[CINDEX];
            return cList.GetComponent<T>();
        }

        /// <summary>
        /// Adds new Component to Entity (CINDEX)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CINDEX"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public T AddComponent<T>(int CINDEX, T component) where T : Component {
            if(CINDEX >= components.Count){
                Debug.WriteLine(CINDEX);
                return null;
            }

            ComponentList cList = components[CINDEX];
            return cList.AddComponent(component); ;
        }

        /// <summary>
        /// Method that handles updating all entities
        /// </summary>
        public void Update() {
            InstantiateQueuedObjects(); //Instanatiae all new entities


            for(int i = 0; i < objectList.Count; i++) {
                Entity e = objectList[i];

                //Cleanup entity if marked for destruction
                if(e.IsDestroyed) {
                    int index = e.ComponentIndex;
                    objectList.Remove(e);
                    components[index].Destroy();
                    components.RemoveAt(index);
                }

                e.Update();
            }
        }

        public void Draw(SpriteBatch batch) {

            for(int i = 0; i < components.Count; i++) {
                var c = components[i];
                c.DrawAll(batch);
            }
        }
    }
}
