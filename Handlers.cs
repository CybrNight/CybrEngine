using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public static class Handlers {
        private static Dictionary<Type, Handler> handlers = new Dictionary<Type, Handler>();

        private static Queue<Handler> creationQueue = new Queue<Handler>();

        private static void ConstructHandlers(){
            while (creationQueue.Count > 0){
                var handler = creationQueue.Dequeue();
                handlers[handler.GetType()] = handler;
                handler.Awake();
            }
        }

        public static void Update() {
            if (creationQueue.Count > 0){
                ConstructHandlers();
                return;
            }

            int startSize = handlers.Count;
            foreach(Handler handler in handlers.Values) {
                if (!handler.IsActive && !handler.IsCreated){
                    handler.Start();    
                }

                handler._Update();

                if(startSize != handlers.Count) {
                    return;
                }
            }
        }

        public static T ObjectInstance<T>(object[] deps) where T : Object{
            T obj = (T)Activator.CreateInstance(typeof(T), true, deps);
            return obj;
        }

        public static void FixedUpdate(){
            int startSize = handlers.Count;
            foreach(Handler handler in handlers.Values) {
                if(!handler.IsActive) continue;

                handler.FixedUpdate();

                if(startSize != handlers.Count) {
                    return;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch) {
            int startSize = handlers.Count;
            foreach(Handler handler in handlers.Values) {
                if(!handler.IsActive) continue;

                handler.Draw(spriteBatch);

                if(startSize != handlers.Count) {
                    return;
                }
            }
        }

        /// <summary>
        /// Create new instance of Handler type T and return reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T AddHandler<T>() where T : Handler {
            T handler = (T)Activator.CreateInstance(typeof(T));
            creationQueue.Enqueue(handler);
            return handler;
        }

        /// <summary>
        /// Gets reference to Handler of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetHandler<T>() where T : Handler {
            if (!handlers.ContainsKey(typeof(T))){
                return default(T);
            }

            return (T)handlers[typeof(T)];
        }

        /// <summary>
        /// Enable or disable Handler of Type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public static void SetActive<T>(bool value = true) where T : Handler {
            try {
                handlers[typeof(T)].SetActive(value);
            } catch(KeyNotFoundException e) {
                Debug.WriteLine(e.Message + "No handler of type" + typeof(T));
            }
        }
    }
}
