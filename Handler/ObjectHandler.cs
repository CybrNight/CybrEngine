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
    public class ObjectHandler : Handler {


        private static List<Object> objects = new List<Object>();
        private static Queue<Object> creationQueue = new Queue<Object>();

        public ObjectHandler() {

        }

        public override void _Awake() {

        }

        public override void _Start() {

        }

        public override void Draw(SpriteBatch spriteBatch) {
            var cList = objects.FindAll(e => e is Entity);
            for(int i = 0; i < cList.Count; i++) {
                var component = cList[i] as Entity;
                component.Sprite.Draw(spriteBatch);
            }
        }

        public override void _Update() {
            //Instantiate all Entites queued from last update
            InstantiateQueuedObjects();
            Debug.WriteLine(objects.Count);

            int startSize = objects.Count;
            for(int i = 0; i < objects.Count; i++) {
                var e = objects[i];

                if(!e.IsCreated) {
                    e.Start();
                }

                if(e.IsDestroyed) {
                    //Remove Entity, and Destory ComponentList
                    int index = e.ID;
                    objects.Remove(e);
                } else if(e.IsActive) {
                    int index = e.ID;
                    e._Update();
                }
            }
        }

        public override void FixedUpdate() {
            var ents = objects.FindAll(e => e is Entity);
            for(int i = 0; i < ents.Count; i++) {
                var e1 = ents[i] as Entity;

                if(e1.IsActive) {
                    e1.FixedUpdate();
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
                if(obj is Entity) {
                    (obj as Entity).Construct();
                }
                objects.Add(obj);
                obj.Awake();
            }
        }

        public void AddObjectInstance(Object obj){
            creationQueue.Enqueue(obj);
        }

        public T Instantiate<T>() where T : Object {
            T entity = Builder.Object<T>();
            creationQueue.Enqueue(entity);
            return entity;
        }

        public T Instantiate<T>(Vector2 position) where T : Entity{
            T entity = Instantiate<T>();
            entity.Position = position;
            return entity;
        }
    }
}
