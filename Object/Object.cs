using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Object {

        protected static ObjectHandler handler;

        public string Name {  get; protected set; }

        protected bool Destroyed { get; set; }
        protected bool BeingDestroyed { get; set; }
        public int ID { get; private set; }
        private static int GLOBAL_ID { get; set; } = 0;

        public virtual void Destroy() { Destroyed = true; }

        public override bool Equals(object obj) {
            return obj is Object @object &&
                   ID == @object.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ID);
        }

        public static Object Instantiate<T>() where T : Object {
            return handler.Instantiate<T>();
        }

        public bool Active { get; set; } = true;

        public bool IsDestroyed {
            get {
                return Destroyed || BeingDestroyed;
            }
        }

        protected Object(){
            handler = ObjectHandler.Instance;
            ID = GLOBAL_ID++;
        }

        public static bool operator ==(Object left, Object right) {
            return EqualityComparer<Object>.Default.Equals(left, right);
        }

        public static bool operator !=(Object left, Object right) {
            return !(left == right);
        }
    }
}
