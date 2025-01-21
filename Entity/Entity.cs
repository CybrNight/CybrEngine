using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CybrEngine {
    public abstract partial class Entity : Object {
        public Transform Transform { get; private set; }
        public int sortingLayer = 0;
        internal ComponentAllocator ComponentAllocator { get; set; }

        public Entity(){ }

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
        public Rectangle Bounds => Transform.Bounds;

        /// <summary>
        /// Returns Transform.Velocity
        /// </summary>
        public Vector2 Velocity {
            get { return Transform.Velocity; }
            set { Transform.Velocity = value; }
        }

        /// <summary>
        /// Chekcs if two GameObjects are intersecting
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Intersects(Entity other) {
            return Transform.Bounds.Intersects(other.Transform.Bounds);
        }

        //Adds new Component to Entity
        public T AddComponent<T>() where T : Component {
            return ComponentAllocator.AddComponent<T>(this);
        }

        //Handles retrieving Componenet from Entity
        public Component GetComponent<T>() where T : Component {
            return ComponentAllocator.GetComponent<T>(this);
        }

        public List<Component> GetComponents<T>() where T : Component {
            return ComponentAllocator.GetComponents<T>(this);
        }

        public override bool Equals(object obj) {
            return obj is Entity entity &&
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

        public new Entity Instantiate<T>() where T : Entity {
            return Instantiate<T>(Position);
        }

        public Entity Instantiate<T>(Vector2 position) where T : Entity {
            return ObjectAllocator.Instantiate<T>(position);
        }

        public T FindEntityOfType<T>() where T : Entity {
            return ObjectAllocator.GetObjectOfType<T>();
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
