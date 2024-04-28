using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Object {

        protected EntityHandler eHandler;
        protected ComponentHandler cHandler;

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
        public void Destroy() { BeingDestroyed = true; Cleanup(); Destroyed = true; }
        protected virtual void Cleanup(){ }

        public override bool Equals(object obj) {
            return obj is Object @object &&
                   ID == @object.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ID);
        }

        protected Object Instantiate<T>() where T : Entity {
            return eHandler.Instantiate<T>();
        }

        protected T Instantiate<T>(Vector2 position) where T : Entity {
            return eHandler.Instantiate<T>(position);
        }

        protected T Instantiate<T>(float x, float y) where T : Entity{
            return eHandler.Instantiate<T>(new Vector2(x, y));
        }

        protected Object(){
            eHandler = Handlers.GetHandler<EntityHandler>();
            cHandler = Handlers.GetHandler<ComponentHandler>();

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
