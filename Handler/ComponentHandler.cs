using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class ComponentHandler : Handler {

        private Dictionary<int, List<Component>> componentMap;

        public ComponentHandler() {
            componentMap = new Dictionary<int, List<Component>>();
        }

        public void DestroyMap(int id){
            if (componentMap.ContainsKey(id)){
                for (int i = 0; i < componentMap[id].Count; i++){
                    componentMap[id][i].Destroy();
                }
                componentMap.Remove(id);
            }
        }

        /// <summary>
        /// Updates all Components in componentMap
        /// </summary>
        public override void Update() {
            int startSize = componentMap.Count;
            foreach(var pair in componentMap){
                var cList = pair.Value;
                int innerSize = cList.Count;
                for (int i = 0; i < cList.Count ; i++){
                    var component = cList[i];
                    component.Update();

                    if(innerSize != cList.Count) {
                        return;
                    }
                }
              
                if (startSize != componentMap.Count){
                    return;
                }
            }
        }
        
        /// <summary>
        /// Draws all Components in componentMap of type IDrawComponent
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch) {
            int startSize = componentMap.Count;
            foreach(var cList in componentMap.Values) {
                int innerSize = cList.Count;
                for (int i = 0; i < cList.Count; i++) {
                    var component = cList[i]; 
                    if (component is IDrawComponent){
                        (component as IDrawComponent).Draw(spriteBatch);
                    }

                    if(innerSize != cList.Count) {
                        return;
                    }
                }

                if(startSize != componentMap.Count) {
                    return;
                }
            }
        }

        /// <summary>
        /// Creates Component ot type T and binds to Entity at index CINDEX
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T AddComponent<T>(int id) where T : Component {
            var newComponent = Builder.Component<T>(id);
            if(!componentMap.ContainsKey(id)) {
                componentMap[id] = new List<Component>();
            }
            componentMap[id].Add(newComponent);
            return newComponent;
        }

        /// <summary>
        /// Retrieves Component of type T from Entity at CINDEX
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetComponent<T>(int id) where T : Component {
            if(!componentMap.ContainsKey(id)) {
                return null;
            }

            var cList = componentMap[id];
            return (T)cList.Find(e => e is T);
        }

        public List<T> GetComponents<T>(int id) where T : Component{
            if (!componentMap.ContainsKey(id)) {
                return null;
            }

            var cList = componentMap[id];
            List<T> result = new List<T>();

            for(int i = 0; i < cList.Count; i++) {
                var component = cList[i];
                if(component is T) {
                    result.Add((T)component);
                }
            }
            return result;
        }
    }
}
