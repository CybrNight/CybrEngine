using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CybrEngine {
    public abstract class Entity : Object {

        //Private references to engine syst
        public int ComponentIndex { get; private set; }
        private static int GLOBAL_COMPONENT_INDEX { get; set; } = 0;

        public Transform Transform { get; private set; }

        /// <summary>
        /// Exposes Transform position value
        /// </summary>
        public Vector2 Position { 
            get { return Transform.Position; } 
            set { Transform.Position = value; } 
        }

        public Vector2 Velocity{
            get { return Transform.Velocity; }
            set { Transform.Velocity = value; }
        }

        public bool Intersects(Entity other) {
            return Transform.Intersects(other.Transform);
        }

        protected Entity() {
            handler = ObjectHandler.Instance;
            ComponentIndex = GLOBAL_COMPONENT_INDEX++;
            Transform = new Transform();
            Name = nameof(Entity);
        }

        public abstract void Awake();
        public abstract void Start();
        public abstract void Update();

        public virtual void OnIntersection(Entity other) { }
        


        public virtual void Draw(SpriteBatch batch) {
            // cList.DrawAll(batch);
        }

        public override void Destroy() {
            BeingDestroyed = true;

            Destroyed = true;
        }

        //Adds new Component to Entity
        public T AddComponent<T>() where T : Component {
            return handler.AddComponent<T>(ComponentIndex);
        }

        //Handles retrieving Componenet from Entity
        public T GetComponent<T>() where T : Component {
            Debug.WriteLine(typeof(T));
            return handler.GetComponent<T>(ComponentIndex);
        }

        public List<T> GetComponents<T>() where T : Component {
            return handler.GetComponents<T>(ComponentIndex);
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

        public static bool operator ==(Entity left, Entity right) {
            return EqualityComparer<Entity>.Default.Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right) {
            return !(left == right);
        }
    }
}
