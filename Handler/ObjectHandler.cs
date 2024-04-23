using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal interface IHandler {
        public bool Enabled { get; set; }
    }

    internal class ObjectHandler : IHandler {

        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Holds physics logic separate from ObjectHandler
        /// Facilitates
        /// </summary>
        public class PhysicsHandler : IHandler {

            public bool Enabled { get; set; } = true;

            private List<Transform> transforms = new List<Transform>();

            public PhysicsHandler() {

            }

            public void Update() {
                //Update all entity positions
                for (int i = 0; i < objectList.Count; i++){
                    Entity e = objectList[i];
                    if (e.Active){
                        e.Position += e.Velocity;
                    }
                }

                //Check for entity intersections
                for(int i = 0; i < objectList.Count; i++) {
                    Entity e1 = objectList[i];
                    for(int j = 0; j < objectList.Count; j++) {
                        Entity e2 = objectList[j];
                        if (e1 != e2){
                            if (e1.Intersects(e2)){
                                e1.OnIntersection(e2);
                                e2.OnIntersection(e1);
                            }
                        }
                    }
                }
            }
        }

        private static List<Entity> objectList = new List<Entity>();
        private static Queue<Entity> creationQueue = new Queue<Entity>();

        private static List<ComponentList> components = new List<ComponentList>();

        private PhysicsHandler pHandler;
        private static ObjectHandler _instance;

        /// <summary>
        // Get static reference to ObjectHandler
        /// </summary>
        public static ObjectHandler Instance {
            get {
                if(_instance == null) {
                    _instance = new ObjectHandler();
                }
                return _instance;
            }
        }

        private ObjectHandler(){
            pHandler = new PhysicsHandler();
        }

        /// <summary>
        /// Method that handles updating all entities
        /// </summary>
        public void Update() {
            InstantiateQueuedObjects(); //Instantiate all new entities

            if (!Enabled){
                return;
            }

            for(int i = 0; i < objectList.Count; i++) {
                Entity e = objectList[i];

                //Cleanup entity if marked for destruction
                if(e.IsDestroyed) {
                    int index = e.ComponentIndex;
                    objectList.Remove(e);
                    components[index].Destroy();
                    components.RemoveAt(index);
                }

                if(e.Active) {
                    e.Update();
                }
            }

            if (pHandler.Enabled){
                pHandler.Update();
            }

            for(int i = 0; i < components.Count; i++) {
                var c = components[i];
                c.UpdateAll();
            }
        }

        public void Draw(SpriteBatch batch) {

            for(int i = 0; i < components.Count; i++) {
                var c = components[i];
                c.DrawAll(batch);
            }
        }

        /// <summary>
        /// Method to handle instantiating all queued objects from last update
        /// </summary>
        private void InstantiateQueuedObjects(){
            while (creationQueue.Count > 0) {
                var obj = creationQueue.Dequeue();
                obj.Awake();
                RegisterComponent(obj.Transform, obj.ComponentIndex);
                objectList.Add(obj);
                obj.Start();
            }
            
        }

        /// <summary>
        /// Adds new Entity to creationQueue, creates new ComponentList entry and returns reference to new Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Instantiate<T>() where T : Entity {
            T entity = (T)Activator.CreateInstance(typeof(T));
            creationQueue.Enqueue(entity); 
            components.Add(new ComponentList(entity));
            return entity;
        }

        public T Instantiate<T>(Vector2 position) where T : Entity {
            T entity = (T)Activator.CreateInstance(typeof(T));
            entity.Position = position;
            creationQueue.Enqueue(entity);
            components.Add(new ComponentList(entity));
            return entity;
        }

        /// <summary>
        /// Returns specified component for CINDEX passed from entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CINDEX"></param>
        /// <returns></returns>
        public T GetComponent<T>(int CINDEX) where T : Component {
            if (CINDEX >= components.Count){
                return null;
            }

            ComponentList cList = components[CINDEX];
            return cList.GetComponent<T>();
        }

        public List<T> GetComponents<T>(int CINDEX) where T : Component {
            if(CINDEX >= components.Count){
                return null;
            }

            ComponentList cList = components[CINDEX];
            return cList.GetComponents<T>();
        }

        /// <summary>
        /// Adds new Component to Entity of type T (CINDEX)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CINDEX"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public T AddComponent<T>(int CINDEX) where T : Component {
            if(CINDEX >= components.Count){
                Debug.WriteLine(CINDEX);
                return null;
            }

            T newComp = (T)Activator.CreateInstance(typeof(T));
            return RegisterComponent(newComp, CINDEX);
        }

        public T RegisterComponent<T>(T component, int CINDEX) where T : Component {
            ComponentList cList = components[CINDEX];
            return cList.AddComponent(component);
        }
    }
}
