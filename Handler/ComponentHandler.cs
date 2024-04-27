using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal class ComponentHandler : Handler {

        private Dictionary<Type, Dictionary<int, Component>> componentList;

        public ComponentHandler() {
            componentList = new Dictionary<Type, Dictionary<int, Component>>();
        }

        public override void Update() {
            int startSize = componentList.Count;
            foreach(var dict in componentList.Values){
                int innerSize = dict.Count;
                foreach(var component in dict.Values){
                    component.Update();    

                    if (innerSize != dict.Count){
                        return;
                    }
                }

                if (startSize != componentList.Count){
                    return;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            int startSize = componentList.Count;
            foreach(var dict in componentList.Values) {
                int innerSize = dict.Count;
                foreach(var component in dict.Values) {  
                    if (component is IDrawComponent){
                        (component as IDrawComponent).Draw(spriteBatch);
                    }

                    if(innerSize != dict.Count) {
                        return;
                    }
                }

                if(startSize != componentList.Count) {
                    return;
                }
            }
        }

        public Component BuildComponent<T>() where T : Component {
            T newComponent = (T)Activator.CreateInstance(typeof(T), true);
            return newComponent;
        }

        /// <summary>
        /// Creates Component ot type T and binds to Entity at index CINDEX
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CINDEX"></param>
        /// <returns></returns>
        public T AddComponent<T>(int CINDEX) where T : Component {
            var ctype = typeof(T);
            var newComponent = BuildComponent<T>();
            if(!componentList.ContainsKey(ctype)) {
                componentList[ctype] = new Dictionary<int, Component>();
            }
            componentList[ctype][CINDEX] = newComponent;
            return (T)newComponent;
        }

        /// <summary>
        /// Retrieves Component of type T from Entity at CINDEX
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CINDEX"></param>
        /// <returns></returns>
        public T GetComponent<T>(int CINDEX) where T : Component {
            var ctype = typeof(T);
            if(!componentList.ContainsKey(ctype)) {
                return null;
            }

            var dict = componentList[ctype];
            if(dict.ContainsKey(CINDEX)) {
                return (T)dict[CINDEX];
            }
            return null;
        }
    }
}
