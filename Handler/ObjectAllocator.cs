using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace CybrEngine {
    internal class ObjectAllocator : IResettable {
        private static ObjectAllocator _instance;
        public static ObjectAllocator Instance {
            get {
                if(_instance == null) {
                    _instance = new ObjectAllocator();
                }
                return _instance;
            }
        }

        private List<Entity> objPool;
        private Queue<Entity> objQueue;

        private ComponentAllocator compAlloc;


        private ObjectAllocator() {
            objPool = new List<Entity>();
            objQueue = new Queue<Entity>();
            compAlloc = new ComponentAllocator();
        }

        public void Reset(){
            objPool.Clear();
            objQueue.Clear();
            compAlloc.Reset();
        }

        /// <summary>
        /// Iterates over all Entity in objPool and calls Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            compAlloc.Draw(spriteBatch);

            #if DEBUG
                for (int i = 0; i < objPool.Count; i++) {
                    Entity obj = objPool[i];
                    if (obj is Entity){
                        obj.SendMessage("_Draw", new object[]{spriteBatch});
                    }
                }
            #endif
        }

        /// <summary>
        /// Called every frame tick by CybrGame
        /// </summary>
        public void Update() {
            compAlloc.Update();
            for(int i = 0; i < objPool.Count; i++) {
                var obj = objPool[i];
                if(obj.IsDestroyed) {
                    //Remove Entity, and Destory ComponentList
                    objPool.Remove(obj);
                    compAlloc.RemoveComponents(obj);
                    continue;
                }

                obj.SendMessage("_Update");
                compAlloc.Update();
            }

            //Instantiate all Entites queued from last update
            AddInstantiatedObjects();
        }

        /// <summary>
        /// Runs every physics tick. Runs FixedUpate all on Entity
        /// </summary>
        public void FixedUpdate() {
            var ents = objPool.FindAll(e => e is Entity);
            for(int i = 0; i < ents.Count; i++) {
                var e1 = ents[i];

                //If Entity IsActive, then run _FixedUpdate
                if(e1.IsActive) {
                    e1.SendMessage("_FixedUpdate");

                    //Update Position based on Velocity
                    e1.Transform.Position = new Vector2(e1.Transform.Position.X + e1.Velocity.X * Time.timeScale,
                                             e1.Transform.Position.Y - e1.Velocity.Y * Time.timeScale);

                    //Check all Entity for collision
                    for(int j = 0; j < ents.Count; j++) {
                        var e2 = ents[j];
                        if(e1 != e2 && e2.IsActive) {
                            if(e1.Intersects(e2)) {
                                e1.SendMessage("_OnIntersection", new object[] {e2});
                                e2.SendMessage("_OnIntersection", new object[] { e1 });
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adds all instantiated objects to object pool
        /// </summary>
        private void AddInstantiatedObjects() {
            while(objQueue.Count > 0) {
                var obj = objQueue.Dequeue();
                objPool.Add(obj);
                obj.SendMessage("_Start");
                obj.SetActive(true);
            }
        }

        /// <summary>
        ///  Instantiates new Entity and returns reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="position"></param>
        /// <returns></returns>
        public T Instantiate<T>(Vector2 position) where T : Entity {
            var type = typeof(T);
            Entity newObject = null;
            if (typeof(Entity).IsAssignableFrom(typeof(T))){
                newObject = Entity.GameObjectFactory<T>.Construct(this);
                newObject.Transform.Position = position;

                newObject.SendMessage("_Awake");
                objQueue.Enqueue(newObject);
                return (T)newObject;
            }
            throw new Exception("No Object of type " + typeof(T));
        }

        public T GetObjectOfType<T>() where T : Entity{
            return (T)objPool.Find(e => e is T);
        }

        public List<Entity> GetObjectsOfType<T>() where T : Entity{
            return objPool.FindAll(e => e is T);
        }

        public Entity AddInstance(Entity gameObject){
            gameObject.SendMessage("_Awake");
            objQueue.Enqueue(gameObject);
            return gameObject;
        }

        /// <summary>
        /// Adds new Component to Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T AddComponent<T>(Entity entity) where T : Component {
            return compAlloc.AddComponent<T>(entity);
        }

        /// <summary>
        /// Gets Component from Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetComponent<T>(Entity id) where T : Component {
            return compAlloc.GetComponent<T>(id);
        }

        public List<T> GetComponents<T>(Entity id) where T : Component {
            return compAlloc.GetComponents<T>(id);
        }

        public void SendMessage(string name, object[] args = null) {
            Messager.SendMessage(this, name, args);
        }
    }
}
