using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private List<GameObject> objPool;
        private Queue<GameObject> objQueue;

        private ComponentAllocator compAlloc;


        private ObjectHandler() {
            objPool = new List<GameObject>();
            objQueue = new Queue<GameObject>();
            compAlloc = new ComponentAllocator();
        }

        /// <summary>
        /// Iterates over all Entity in objPool and calls Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            compAlloc.Draw(spriteBatch);
        }

        /// <summary>
        /// Called every frame tick by CybrGame
        /// </summary>
        public void Update() {
            //Instantiate all Entites queued from last update
            Debug.WriteLine(objPool.Count);


            AddInstantiatedObjects();

            compAlloc.Update();
            for(int i = 0; i < objPool.Count; i++) {
                var obj = objPool[i];
                if(obj.IsDestroyed) {
                    //Remove Entity, and Destory ComponentList
                    obj.SendMessage("_Cleanup");
                    objPool.Remove(obj);
                    compAlloc.RemoveComponents(obj as GameObject);
                    continue;
                }

                obj.SendMessage("_Update");
                compAlloc.Update();
            }
        }

        /// <summary>
        /// Runs every physics tick. Runs FixedUpate all on Entity
        /// </summary>
        public void FixedUpdate() {
            var ents = objPool.FindAll(e => e is GameObject);
            for(int i = 0; i < ents.Count; i++) {
                var e1 = ents[i] as GameObject;

                //If Entity IsActive, then run _FixedUpdate
                if(e1.IsActive) {
                    e1.SendMessage("_FixedUpdate()");

                    //Update Position based on Velocity
                    e1.Transform.Position = new Vector2(e1.Transform.Position.X + e1.Velocity.X * Time.deltaTime,
                                             e1.Transform.Position.Y - e1.Velocity.Y * Time.deltaTime);

                    //Check all Entity for collision
                    for(int j = 0; j < ents.Count; j++) {
                        var e2 = ents[j] as GameObject;
                        if(e1 != e2 && e2.IsActive) {
                            if(e1.Intersects(e2)) {
                                e1.OnIntersection(e2);
                                e2.OnIntersection(e1);
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
            }
        }

        /// <summary>
        ///  Instantiates new Entity and returns reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="position"></param>
        /// <returns></returns>
        public T Instantiate<T>(Vector2 position) where T : GameObject {
            var type = typeof(T);
            GameObject newObject = null;
            if (typeof(GameObject).IsAssignableFrom(typeof(T))){
                newObject = GameObject.Factory<T>.Instantiate(this);
                newObject.Transform.Position = position;

                if (newObject is Entity){
                    var e = newObject as Entity;
                    e.AddComponent<Sprite>();
                }

                newObject.SendMessage("_Awake");
                objQueue.Enqueue(newObject);
                return (T)newObject;
            }
            throw new Exception("No Object of type " + typeof(T));
        }

        public GameObject AddInstance(GameObject gameObject){
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
        public T AddComponent<T>(GameObject entity) where T : Component {
            return compAlloc.AddComponent<T>(entity);
        }

        /// <summary>
        /// Gets Component from Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetComponent<T>(GameObject id) where T : Component {
            return compAlloc.GetComponent<T>(id);
        }

        public List<T> GetComponents<T>(GameObject id) where T : Component {
            return compAlloc.GetComponents<T>(id);
        }

        public void SendMessage(string name, object[] args = null) {
            Messager.SendMessage(this, name, args);
        }
    }
}
