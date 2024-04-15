using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CybrEngine {
    public abstract class Entity : Object {

        //Private references to engine systems
        private Texture2D _sprite;
        public int ComponentIndex { get; protected set; }
        private static int GLOBAL_COMPONENT_INDEX { get; set; } = 0;

        public Transform Transform { get; protected set; }
        public Texture2D sprite {
            get { return _sprite; }
        }

        protected Entity(){
            handler = ObjectHandler.Instance;
            ComponentIndex = GLOBAL_COMPONENT_INDEX++;
        }

        public abstract void Awake();
        public abstract void Start();
        public abstract void Update();
     

        public virtual void Draw(SpriteBatch batch){
           // cList.DrawAll(batch);
        }

        public override void Destroy(){
            BeingDestroyed = true;

            Destroyed = true;
        }

        public static void Instantiate(Type entity){
            handler.Instantiate(entity);
        }

        //Adds new Component to Entity
        public T AddComponenent<T>(T component) where T : Component{
            return handler.AddComponent(ComponentIndex, component);
        }

        //Handles retrieving Componenet from Entity
        public T GetComponent<T>() where T: Component{
            Debug.WriteLine(typeof(T));
            return handler.GetComponent<T>(ComponentIndex);
        }

    }
}
