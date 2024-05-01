using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal class ComponentStore {

        private Dictionary<int, List<Component>> cMap;
        private ObjectHandler _objHandler;

        public ComponentStore() {
            cMap = new Dictionary<int, List<Component>>();
        }

        public void Remove(int id){
            if (cMap.ContainsKey(id)){
                for (int i = 0; i < cMap[id].Count; i++){
                    cMap[id][i].Destroy();
                }
                cMap.Remove(id);
            }
        }
       
        /// <summary>
        /// Creates Component ot type T and binds to Entity at index CINDEX
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public void AddComponent(int id, Component component) {
            if(!cMap.ContainsKey(id)) {
                cMap[id] = new List<Component>();
            }
            cMap[id].Add(component);
        }

        /// <summary>
        /// Retrieves Component of type T from Entity at CINDEX
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetComponent<T>(int id) where T : Component {
            if(!cMap.ContainsKey(id)) {
                return null;
            }

            var cList = cMap[id];
            return (T)cList.Find(e => e is T);
        }

        /// <summary>
        /// Returns a List<Component> of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<T> GetComponents<T>(int id) where T :Component {
            if (!cMap.ContainsKey(id)) {
                return null;
            }

            var cList = cMap[id];
            List<T> result = new List<T>();

            for(int i = 0; i < cList.Count; i++) {
                var component = cList[i];
                if (component is T){
                    result.Add((T)component);
                }
            }
            return result;
        }
    }
}
