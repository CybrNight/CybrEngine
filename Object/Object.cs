using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace CybrEngine {
    public abstract class Object : IMessageable {

        public string Name { get; protected set; }

        private bool Active { get; set; }

        public bool IsCreated { get; private set; }
        protected bool Destroyed { get; set; }
        protected bool BeingDestroyed { get; set; }
        public int ID { get; private set; }
        private static int GLOBAL_ID { get; set; } = 0;

        public bool IsActive => Active || IsDestroyed;
        public bool IsDestroyed => BeingDestroyed || Destroyed;

        public void SetActive(bool value = true) { Active = value; }

        /// <summary>
        /// Marks Object for destruction
        /// </summary>
        public void Destroy() { BeingDestroyed = true; }

        public override bool Equals(object obj) {
            return obj is Object @object &&
                   ID == @object.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ID);
        }

        public void Print(string value){
            Debug.WriteLine(value);
        }

        public Object() {
            ID = GLOBAL_ID++;
            Active = true;
        }

        public static bool operator ==(Object left, Object right) {
            return EqualityComparer<Object>.Default.Equals(left, right);
        }

        public static bool operator !=(Object left, Object right) {
            return !(left == right);
        }
    }
}
