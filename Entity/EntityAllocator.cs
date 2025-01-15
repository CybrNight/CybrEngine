using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace CybrEngine {
    internal class EntityAllocator : IResettable {
        private static EntityAllocator _instance;
        public static EntityAllocator Instance {
            get {
                if(_instance == null) {
                    _instance = new EntityAllocator();
                }
                return _instance;
            }
        }

        private List<Entity> entityPool;
        private Queue<Entity> objQueue;

        private ComponentAllocator compAlloc;


        private EntityAllocator() {
            entityPool = new List<Entity>();
            objQueue = new Queue<Entity>();
        }

        public void Reset(){
            entityPool.Clear();
            objQueue.Clear();
            compAlloc.Reset();
        }

        /// <summary>
        /// Iterates over all Entity in objPool and calls Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch) {
            #if DEBUG
                for (int i = 0; i < entityPool.Count; i++) {
                    Entity obj = entityPool[i];
                    if (obj is Entity){
                        obj.SendMessage("_DebugDraw", spriteBatch);
                    }
                }
            #endif
        }

        /// <summary>
        /// Called every frame tick by CybrGame
        /// </summary>
        public void Update() {
            for(int i = 0; i < entityPool.Count; i++) {
                var entity = entityPool[i];
                if(entity.IsDestroyed) {
                    //Remove Entity, and Destory ComponentList
                    Autoload.ComponentAllocator.RemoveEntity(entity);
                    entityPool.Remove(entity);
                    continue;
                }

                entity.SendMessage("_Update");
            }

            //Instantiate all Entites queued from last update
            AddInstantiatedObjects();
        }

        /// <summary>
        /// Runs every physics tick. Runs FixedUpate all on Entity
        /// </summary>
        public void FixedUpdate() {
            var ents = entityPool;
            for(int i = 0; i < ents.Count; i++) {
                var e1 = ents[i];

                //If Entity IsActive, then run _FixedUpdate
                if(e1.IsActive) {
                    e1.SendMessage("_FixedUpdate");

                    //Update Position based on Velocity
                    e1.Transform.Position = new Vector2(e1.Transform.Position.X + e1.Velocity.X,
                                             e1.Transform.Position.Y - e1.Velocity.Y);

                    //Check all Entity for collision
                    for(int j = 0; j < ents.Count; j++) {
                        var e2 = ents[j];
                        if(e1 != e2 && e2.IsActive) {
                            if(e1.Intersects(e2)) {
                                e1.SendMessage("_OnIntersection", e2);
                                e2.SendMessage("_OnIntersection", e1);
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
                entityPool.Add(obj);
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
            var entity = Entity.Construct<T>();
            entity.Transform.Position = position;

            entity.SendMessage("_Awake");
            objQueue.Enqueue(entity);
            return (T)entity;
        }

        public T GetObjectOfType<T>() where T : Entity{
            return (T)entityPool.Find(e => e is T);
        }

        public List<Entity> GetObjectsOfType<T>() where T : Entity{
            return entityPool.FindAll(e => e is T);
        }

        public List<Entity> GetAll(){
            return entityPool;
        }

        public Entity AddInstance(Entity entity){
            if (!entity){
                throw new ArgumentNullException("Entity reference null");
            }

            entity.SendMessage("_Awake");
            objQueue.Enqueue(entity);
            return entity;
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
        public Component GetComponent<T>(Entity id) {
            return compAlloc.GetComponent<T>(id);
        }

        public List<Component> GetComponents<T>(Entity id) where T : Component {
            return compAlloc.GetComponents<T>(id);
        }

        public void SendMessage(string name, object[] args = null) {
            Messager.SendMessage(this, name, args);
        }
    }
}
