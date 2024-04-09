using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CybrEngine {
    public abstract class Entity {

        //Private references to engine systems
        private Texture2D _sprite;
        private Handler handler;
        private ComponentList cList;

        public Texture2D sprite {
            get { return _sprite; }
        }

        protected Entity(){
            handler = Handler.Instance;
            cList = new ComponentList(this);
        }

        public virtual void Update(){
            cList.UpdateAll();
        }

        public virtual void Draw(SpriteBatch batch){
            cList.DrawAll(batch);
        }


        protected bool Destroyed = false;
        protected bool BeingDestroyed = false;
        
        public bool IsDestroyed {
            get {
                return Destroyed || BeingDestroyed;
            }
        }

        protected void Destory(Entity entity){
            entity.Destroyed = true;
        }

        protected void Instantiate(Entity entity){
            handler.Instantiate(entity);
        }

        //Adds new Component to Entity
        public T AddComponenent<T>(T component) where T : Component{
            return cList.AddComponent(component);
        }

        //Handles retrieving Componenet from Entity
        public T GetComponent<T>() where T: Component{
            Debug.WriteLine(typeof(T));
            return cList.GetComponent<T>();
        }
    }
}
