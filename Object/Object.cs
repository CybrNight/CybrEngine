using System;
using System.Collections.Generic;

namespace CybrEngine {
    public abstract class Object {

        internal Messager messager;

        public string Name { get; protected set; }

        private bool Active { get; set; } = false;

        public bool IsCreated { get; private set; } = false;
        protected bool Destroyed { get; set; }
        protected bool BeingDestroyed { get; set; }
        public int ID { get; private set; }
        private static int GLOBAL_ID { get; set; } = 0;

        public bool IsActive {
            get { return Active || IsDestroyed; }
        }

        public bool IsDestroyed {
            get {
                return BeingDestroyed || Destroyed;
            }
        }

        public void Start() {
            if(IsCreated) return;
            IsCreated = true;
        }

        public virtual void SendMessage(string name) {
            messager.SendMessage(this, name);
        }

        public void SetActive(bool value = true) { Active = value; }
        public void Destroy() { BeingDestroyed = true; _Cleanup(); Destroyed = true; }

        /// <summary>
        /// Function that handles cleaning up un-managed data
        /// </summary>
        protected virtual void _Cleanup() { }

        public override bool Equals(object obj) {
            return obj is Object @object &&
                   ID == @object.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ID);
        }

        protected Object() {
            ID = GLOBAL_ID++;
            Active = true;

            messager = Messager.Instance;
        }

        public static bool operator ==(Object left, Object right) {
            return EqualityComparer<Object>.Default.Equals(left, right);
        }

        public static bool operator !=(Object left, Object right) {
            return !(left == right);
        }
    }
}
