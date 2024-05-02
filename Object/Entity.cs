using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CybrEngine {
    public abstract class Entity : Object {

        internal ObjectHandler objHandler;

        public Transform Transform { get; private set; }

        //Define built-in reference to Component SpriteRenderer
        public Sprite Sprite { get; private set; }

        /// <summary>
        /// Get Position of Entity Transform
        /// </summary>
        public Vector2 Position {
            get { return Transform.Position; }
            set { Transform.Position = value; }
        }

        /// <summary>
        /// Get Bounds of Entity Transform
        /// </summary>
        public Rectangle Bounds {
            get { return Transform.Bounds; }
        }

        public Vector2 Velocity {
            get { return Transform.Velocity; }
            set { Transform.Velocity = value; }
        }

        public bool Intersects(Entity other) {
            return Transform.Bounds.Intersects(other.Transform.Bounds);
        }

        public virtual void OnIntersection(Entity other) { }

        protected Entity() {
            objHandler = ObjectHandler.Instance;
            messager = Messager.Instance;

            Transform = new Transform();
            Sprite = new Sprite();
            Sprite.Transform = Transform;
        }

        public virtual void FixedUpdate() { }

        /// <summary>
        /// Calls private method on Entity
        /// </summary>
        /// <param name="name"></param>
        public override void SendMessage(string name) {
            messager.SendMessage(this, name);
        }

        //Adds new Component to Entity
        public T AddComponent<T>() where T : Component {
            return objHandler.AddComponent<T>(ID);
        }

        //Handles retrieving Componenet from Entity
        public T GetComponent<T>() where T : Component {
            return objHandler.GetComponent<T>(ID);
        }

        public List<T> GetComponents<T>() where T : Component {
            return objHandler.GetComponents<T>(ID);
        }

        public override bool Equals(object obj) {
            return obj is Entity entity &&
                   base.Equals(obj) &&
                   Name == entity.Name &&
                   ID == entity.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(base.GetHashCode(), Name, ID);
        }

        protected T Instantiate<T>() where T : Entity {
            return objHandler.Instantiate<T>();
        }

        protected T Instantiate<T>(Vector2 position) where T : Entity {
            return objHandler.Instantiate<T>(position);
        }

        protected T Instantiate<T>(float x, float y) where T : Entity {
            return objHandler.Instantiate<T>(new Vector2(x, y));
        }

        public static implicit operator bool(Entity e) {
            return (e != null);
        }

        public static bool operator ==(Entity left, Entity right) {
            return EqualityComparer<Entity>.Default.Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right) {
            return !(left == right);
        }
    }
}
