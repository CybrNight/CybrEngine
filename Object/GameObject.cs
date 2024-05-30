using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CybrEngine {
    public abstract partial class GameObject : Object{
        private ObjectHandler objHandler;
        private ParticleHandler particleHandler;

        public Transform Transform { get; private set; }
        public int sortingLayer = 0;
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


        /// <summary>
        /// Returns Transform.Velocity
        /// </summary>
        public Vector2 Velocity {
            get { return Transform.Velocity; }
        }

        /// <summary>
        /// Chekcs if two GameObjects are intersecting
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Intersects(GameObject other) {
            return Transform.Bounds.Intersects(other.Transform.Bounds);
        }

        //Adds new Component to Entity
        public T AddComponent<T>() where T : Component {
            return objHandler.AddComponent<T>(this);
        }

        //Handles retrieving Componenet from Entity
        public T GetComponent<T>() where T : Component {
            return objHandler.GetComponent<T>(this);
        }

        public List<T> GetComponents<T>() where T : Component {
            return objHandler.GetComponents<T>(this);
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

        public Particle EmitParticle(Particle particle, Vector2 position){
            return particleHandler.Emit(particle.Instance(), position);
        }

        public T Instantiate<T>() where T : GameObject {
            return Instantiate<T>(Position);
        }

        public T Instantiate<T>(Vector2 position) where T : GameObject {
            return objHandler.Instantiate<T>(position);
        }

        public T FindObjectOfType<T>() where T : GameObject{
            return objHandler.GetObjectOfType<T>();
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
