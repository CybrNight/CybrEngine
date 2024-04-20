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

        protected Entity(){
            handler = ObjectHandler.Instance;
            ComponentIndex = GLOBAL_COMPONENT_INDEX++;
            Transform = new Transform();
            Name = nameof(Entity);
        }

        public abstract void Awake();
        public abstract void Start();
        public abstract void Update();

        public virtual void OnIntersection(Transform other){ }
     

        public virtual void Draw(SpriteBatch batch){
           // cList.DrawAll(batch);
        }

        public override void Destroy(){
            BeingDestroyed = true;

            Destroyed = true;
        }

        //Adds new Component to Entity
        public T AddComponent<T>() where T : Component{
            return handler.AddComponent<T>(ComponentIndex);
        }

        //Handles retrieving Componenet from Entity
        public T GetComponent<T>() where T: Component{
            Debug.WriteLine(typeof(T));
            return handler.GetComponent<T>(ComponentIndex);
        }

        public List<T> GetComponents<T>() where T: Component{
            return handler.GetComponents<T>(ComponentIndex);
        }

    }
}
