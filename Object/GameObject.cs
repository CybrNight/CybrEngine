using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CybrEngine {
    public static class GameObjectExtensions { 
        public static GameObject Instance(this GameObject obj){
            return GameObject.Factory<GameObject>.Instance(obj);
        }
    }

    public abstract class GameObject : Object {
        internal static class Factory<T> where T : GameObject {
            /// <summary>
            /// Creates a copy instance of GameObject
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            internal static T Instance(GameObject obj) {
                var gameObject = Builder.GameObject<T>();
                gameObject.objHandler = obj.objHandler;
                gameObject.Transform = obj.Transform;
                return gameObject;
            }

            internal static void Construct(ref GameObject obj){
                obj.objHandler = ObjectHandler.Instance;
            }

            internal static T Instantiate(ObjectHandler objHandler) {
                var gameObject = Builder.GameObject<T>();
                gameObject.objHandler = objHandler;
                return gameObject;
            }
        }

        internal ObjectHandler objHandler;

        public Transform Transform { get; private set; }
        protected GameObject(){ Transform = new Transform(); }

        /// <summary>
        /// Get Position of Entity Transform
        /// </summary>
        public Vector2 Position {
            get { return Transform.Position; }
        }

        /// <summary>
        /// Get Bounds of Entity Transform
        /// </summary>
        public Rectangle Bounds => Transform.Bounds;

        private void _Cleanup(){
            Transform = null;
        }


        public Vector2 Velocity {
            get { return Transform.Velocity; }
        }

        public bool Intersects(GameObject other) {
            return Transform.Bounds.Intersects(other.Transform.Bounds);
        }

        public virtual void OnIntersection(GameObject other) { }

        //Adds new Component to Entity
        public T AddComponent<T>() where T : Component {
            return ObjectHandler.Instance.AddComponent<T>(this);
        }

        //Handles retrieving Componenet from Entity
        public T GetComponent<T>() where T : Component {
            return ObjectHandler.Instance.GetComponent<T>(this);
        }

        public List<T> GetComponents<T>() where T : Component {
            return ObjectHandler.Instance.GetComponents<T>(this);
        }

        public override bool Equals(object obj) {
            return obj is GameObject entity &&
                   base.Equals(obj) &&
                   Name == entity.Name &&
                   ID == entity.ID;
        }

        protected void DestroyIfOutside() {
            if(Transform.Position.X < -32 || Transform.Position.X > Config.WINDOW_WIDTH + 32 || Transform.Position.Y < 0 || Transform.Position.Y > Config.WINDOW_HEIGHT + 32) {
                Destroy();
            }
        }

        public override int GetHashCode() {
            return HashCode.Combine(base.GetHashCode(), Name, ID);
        }

        public T Instantiate<T>() where T : GameObject {
            return Instantiate<T>(Position);
        }

        public T Instantiate<T>(Vector2 position) where T : GameObject {
            return ObjectHandler.Instance.Instantiate<T>(position);
        }

        public static implicit operator bool(GameObject e) {
            return (e != null);
        }

        public static bool operator ==(GameObject left, GameObject right) {
            return EqualityComparer<GameObject>.Default.Equals(left, right);
        }

        public static bool operator !=(GameObject left, GameObject right) {
            return !(left == right);
        }
    }
}
