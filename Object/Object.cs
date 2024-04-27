using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Object {

        internal static EntityHandler handler;

        public string Name {  get; protected set; }

        private bool Active { get; set; } = true;
        protected bool Destroyed { get; set; }
        protected bool BeingDestroyed { get; set; }
        public int ID { get; private set; }
        private static int GLOBAL_ID { get; set; } = 0;



        public bool IsActive {
            get { return Active || IsDestroyed; }
        }

        public bool IsDestroyed {
            get {
                return Destroyed;
            }
        }

        public void SetActive(){ Active = true; }
        public virtual void Destroy() { Destroyed = true; }

        public override bool Equals(object obj) {
            return obj is Object @object &&
                   ID == @object.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ID);
        }

        public static Object Instantiate<T>() where T : Entity {
            return handler.Instantiate<T>();
        }

        public static T Instantiate<T>(Vector2 position) where T : Entity {
            return handler.Instantiate<T>(position);
        }

        protected Object(){
            handler = EngineHandler.Instance.GetHandler<EntityHandler>();
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
