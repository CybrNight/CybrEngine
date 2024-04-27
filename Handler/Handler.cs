using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Handler {
        public bool Enabled { get; set; } = true;

        protected Handler() { }

        /// <summary>
        /// Called every game tick
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Called every physics tick
        /// </summary>
        public virtual void FixedUpdate() { }

        /// <summary>
        /// Called every draw tick
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }

    public class EngineHandler : Handler {

        private static EngineHandler _instance;
        public static EngineHandler Instance {
            get {
                if(_instance == null) {
                    _instance = new EngineHandler();
                }
                return _instance;
            }
        }

        private Dictionary<Type, Handler> handlers;

        protected EngineHandler() {
            handlers = new Dictionary<Type, Handler>();
        }

        public override void FixedUpdate() {
            int startSize = handlers.Count;
            foreach(Handler handler in handlers.Values) {
                if(!handler.Enabled) continue;

                handler.FixedUpdate();

                if(startSize != handlers.Count) {
                    return;
                }
            }
        }

        public override void Update() {
            int startSize = handlers.Count;
            foreach(Handler handler in handlers.Values) {
                if(!handler.Enabled) continue;

                handler.Update();

                if(startSize != handlers.Count) {
                    return;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            int startSize = handlers.Count;
            foreach(Handler handler in handlers.Values) {
                if(!handler.Enabled) continue;

                handler.Draw(spriteBatch);

                if(startSize != handlers.Count) {
                    return;
                }
            }
        }

        /// <summary>
        /// Enable or disable Handler of Type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="active"></param>
        public void SetActive<T>(bool active) where T : Handler {
            try {
                handlers[typeof(T)].Enabled = active;
            } catch(KeyNotFoundException e) {
                Debug.WriteLine(e.Message + "No handler of type" + typeof(T));
            }
        }

        /// <summary>
        /// Gets reference to Handler of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetHandler<T>() where T : Handler {
            try {
                return (T)handlers[typeof(T)];
            } catch(KeyNotFoundException e) {
                Debug.WriteLine(e.Message + "No handler of type" + typeof(T));
                return default;
            }
        }

        /// <summary>
        /// Create new instance of Handler type T and return reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddHandler<T>() where T : Handler {
            if(handlers.ContainsKey(typeof(T))) {
                throw new Exception("Handler " + typeof(T) + " already exists");
            }

            T handler = Activator.CreateInstance<T>();
            handlers[typeof(T)] = handler;
            return handler;
        }
    }
}
