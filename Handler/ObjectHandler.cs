using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace CybrEngine {
    internal class ObjectHandler : IMessageable {
        private static ObjectHandler _instance;
        public static ObjectHandler Instance {
            get {
                if(_instance == null) {
                    _instance = new ObjectHandler();
                }
                return _instance;
            }
        }

        private List<Object> objPool;
        private Queue<Object> creationQueue;

        private ComponentStore store;


        private ObjectHandler() {
            objPool = new List<Object>();
            creationQueue = new Queue<Object>();
            store = new ComponentStore();
        }

        /// <summary>
        /// Iterates over all Entity in objPool and calls Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            var entities = objPool.FindAll(e => e is Entity);
            for(int i = 0; i < entities.Count; i++) {
                var entity = (entities[i] as Entity);
                var sprite = entity.Sprite;
                if(sprite == null || sprite.Texture == null) continue;

                var transform = entity.Transform;
                spriteBatch.Draw(sprite.Texture, transform.Position, null, sprite.Color, 0f,
                transform.Origin,
                transform.Scale,
                SpriteEffects.None,
                0f);
            }
        }

        /// <summary>
        /// Called every frame tick by CybrGame
        /// </summary>
        public void Update() {
            //Instantiate all Entites queued from last update
            InstantiateQueuedObjects();
            Debug.WriteLine(objPool.Count);

            for(int i = 0; i < objPool.Count; i++) {
                var e = objPool[i];
                if(!e.IsCreated) {
                    e.SendMessage("_Start");
                }

                if(e.IsDestroyed) {
                    //Remove Entity, and Destory ComponentList
                    int index = e.ID;
                    objPool.Remove(e);
                    store.Remove(index);
                    continue;
                } else if(e.IsActive) {
                    e.SendMessage("_Update");
                }
            }
        }

        public void FixedUpdate() {
            var ents = objPool.FindAll(e => e is Entity);
            for(int i = 0; i < ents.Count; i++) {
                var e1 = ents[i] as Entity;

                if(e1.IsActive) {
                    e1.SendMessage("_FixedUpdate()");
                    e1.Position = new Vector2(e1.Position.X + e1.Velocity.X * Time.deltaTime,
                                             e1.Position.Y - e1.Velocity.Y * Time.deltaTime);

                    for(int j = 0; j < ents.Count; j++) {
                        var e2 = ents[j] as Entity;
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

        private void InstantiateQueuedObjects() {
            while(creationQueue.Count > 0) {
                var obj = creationQueue.Dequeue();
                objPool.Add(obj);
                obj.SendMessage("_Awake");
            }
        }

        public T Instantiate<T>() where T : Object {
            T entity = Builder.Object<T>();
            creationQueue.Enqueue(entity);
            return entity;
        }

        public T Instantiate<T>(Vector2 position) where T : Entity {
            T entity = Instantiate<T>();
            entity.Position = position;
            return entity;
        }

        public T AddComponent<T>(int id) where T : Component {
            var component = Builder.Component<T>();
            store.AddComponent(id, component);
            return component;
        }

        public T GetComponent<T>(int id) where T : Component {
            return store.GetComponent<T>(id);
        }

        public List<T> GetComponents<T>(int id) where T : Component {
            return store.GetComponents<T>(id);
        }

        public void SendMessage(string name, object[] args = null) {
            Messager.Instance.SendMessage(this, name, args);
        }
    }
}
