using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CybrEngine {
    public abstract class Entity {

        //Private references to engine systems

        private Vector2 _positiion;
        private Texture2D _sprite;
        private Handler handler;
        private ComponentList cList;

        public Vector2 position{ 
            get{ return _positiion; }
            set{ _positiion = value; }
        }

        public Texture2D sprite {
            get { return _sprite; }
        }

        protected Entity(){
            handler = Handler.Instance;
        }

        public virtual void SetSprite(string name){
            _sprite = Assets.GetSprite(name);
        }


        public virtual void Update(){

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

        protected void AddComponenent(Type cType){
            cList.AddComponent(this, cType);
        }

        protected void GetComponent<T>(Entity entity){
            cList.GetComponent<T>(entity);
        }
    }
}
