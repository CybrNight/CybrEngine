using System;
using System.Collections.Generic;
using CybrEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal class ComponentList {

        public ComponentList() { 
            
        }

        private Dictionary<Type, List<Component>> cList = new Dictionary<Type, List<Component>>();

        public T AddComponent<T>(T component) where T : Component{
            Type cType = component.CType;

            if (!cList.ContainsKey(cType)) {
                cList.Add(typeof(T), new List<Component>());
            }

            cList[cType].Add(component);

            return component;
        }

        public T GetComponent<T>() where T : Component{
            //Check components list for entity
            Type cType = typeof(T);
        
            if (!cList.ContainsKey(cType)) {
                return null;
            }
            return (T)cList[cType].First();
        }

        public List<T> GetComponents<T>() where T : Component {
            //Check components list for entity
            Type cType = typeof(T);

            List<T> result = new List<T>(cList[cType].Count);
            foreach (Component c in cList[cType]){
                result.Add((T)c);
            }
            return result;
        }
    }
}
