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
    public class EntityHandler : Handler {

        /// <summary>
        /// Holds physics logic separate from ObjectHandler
        /// Facilitates
        /// </summary>
        public class PhysicsHandler : Handler {


            public override void FixedUpdate() {
                //Update all entity positions


                for(int i = 0; i < entities.Count; i++) {
                    Entity e = entities[i];

                    if(e.IsActive) {
                        e.FixedUpdate();
                        e.Position = new Vector2(e.Position.X + e.Velocity.X * Time.deltaTime,
                                                 e.Position.Y - e.Velocity.Y * Time.deltaTime);
                    }

                }

                //Check for entity intersections
                for(int i = 0; i < entities.Count; i++) {
                    Entity e1 = entities[i];
                    for(int j = 0; j < entities.Count; j++) {
                        Entity e2 = entities[j];
                        if(e1 != e2) {
                            if(e1.Intersects(e2)) {
                                e1.OnIntersection(e2);
                                e2.OnIntersection(e1);
                            }
                        }
                    }
                }
            }
        }

        private static List<Entity> entities = new List<Entity>();
        private static Queue<Entity> creationQueue = new Queue<Entity>();

        public EntityHandler() {
            Handlers.AddHandler<PhysicsHandler>();
        }

        /// <summary>
        /// Method that handles updating all Entities
        /// </summary>
        public override void Update() {
            //Instantiate all Entites queued from last update
            if(entities.Count < 500)
                InstantiateQueuedEntities();

            int startSize = entities.Count;
            for(int i = 0; i < entities.Count; i++) {
                Entity e = entities[i];

                if(e.IsDestroyed) {
                    //Remove Entity, and Destory ComponentList
                    int index = e.ComponentIndex;
                    entities.Remove(e);
                    continue;
                }

                //If Entity Active, Update()
                if(e.IsActive) {
                    int index = e.ComponentIndex;
                    e.Update();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < entities.Count; i++){ 
                Entity e = entities[i];
                if(e.IsActive) {
                    int index = e.ComponentIndex;
                }
            }
        }


        /// <summary>
        /// Method to handle instantiating all queued objects from last update
        /// </summary>
        private void InstantiateQueuedEntities() {
            while(creationQueue.Count > 0) {
                var obj = creationQueue.Dequeue();
                obj.Construct();
                entities.Add(obj);
                obj.Start();
            }

        }

        /// <summary>
        /// Adds new Entity to creationQueue, creates new ComponentList entry and returns reference to new Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Instantiate<T>() where T : Entity {
            T entity = Builder.Entity<T>();
            creationQueue.Enqueue(entity);
            return entity;
        }

        /// <summary>
        /// Adds new Entity object to creationQueue wih position
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="position"></param>
        /// <returns></returns>
        public T Instantiate<T>(Vector2 position) where T : Entity {
            T entity = Instantiate<T>();
            entity.Position = position;
            return entity;
        }

        /*
         * ==== DEPRECATED =====
        /// <summary>
        /// Returns specified component for CINDEX passed from entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CINDEX"></param>
        /// <returns></returns>
        public T GetComponent<T>(int CINDEX) where T : Component {
            if(!components.ContainsKey(CINDEX)) {
                return null;
            }

            ComponentList cList = components[CINDEX];
            return cList.GetComponent<T>();
        }


        /// <summary>
        /// Returns List of all Component with type T on Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CINDEX"></param>
        /// <returns></returns>
        public List<T> GetComponents<T>(int CINDEX) where T : Component {
            if(CINDEX >= components.Count) {
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
            if(!components.ContainsKey(CINDEX)) {
                return null;
            }

            //Create new instance of Component T
            T newComp = (T)Activator.CreateInstance(typeof(T), true);
            return components[CINDEX].AddComponent(newComp);
        }*/
    }
}
