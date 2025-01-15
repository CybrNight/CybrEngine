using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace CybrEngine {
    public abstract class Object : IMessageable {
        internal static class Factory<T> where T : Object {
            /// <summary>
            /// Constructs new instance of Object
            /// </summary>
            /// <returns></returns>
            public static T Construct(Type[] paramTypes, object[] paramVals) {
                var obj = Builder.Construct<T>(paramTypes, paramVals);

                obj.ID = GLOBAL_ID++;
                obj.ObjectAllocator = Autoload.ObjectAllocator;
                obj.ParticleHandler = Autoload.ParticleHandler;
                obj.Name = obj.GetType().ToString();

                return obj;
            }

            public static T Construct(){
                return Construct(new Type[] { }, null);
            }

            /// <summary>
            /// Creates clone of Object instance
            /// </summary>
            /// <returns></returns>
            public static T Instance(Object obj) {
                if (obj){
                    var clone = obj.MemberwiseClone() as T;
                    return clone;
                }
                return default(T);
            }
        }

        public string Name { get; set; }

        protected bool Active { get; set; } = false;

        public bool IsCreated { get; private set; } = false;
        protected bool Destroyed { get; set; } = false;
        protected bool BeingDestroyed { get; set; } = false;
        public int ID { get; internal set; }
        internal static int GLOBAL_ID { get; set; } = 0;

        public bool IsActive => Active || IsDestroyed;
        public bool IsDestroyed => BeingDestroyed || Destroyed;

        public void SetActive(bool value = true) { Active = value; }

        internal ObjectAllocator ObjectAllocator { get; set;  }
        protected ParticleStore ParticleHandler { get; set; }

        /// <summary>
        /// Marks Object for destruction
        /// </summary>
        public void Destroy() { BeingDestroyed = true; }
        public Object Clone() {
            var clone = (Object)MemberwiseClone();
            clone.Name = Name;
            clone.ID = GLOBAL_ID++;
            clone.Active = Active;
            return clone;
        }


        public override bool Equals(object obj) {
            return obj is Object @object &&
                   ID == @object.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ID);
        }

        public void Print(string value) {
            Debug.WriteLine(value);
        }

        public T Instantiate<T>() where T : Object {
            return Builder.Construct<T>();
        }

        public static implicit operator bool(Object e) {
            return (e != default);
        }

        public static bool operator ==(Object left, Object right) {
            return EqualityComparer<Object>.Default.Equals(left, right);
        }

        public static bool operator !=(Object left, Object right) {
            return !(left == right);
        }
    }
}
