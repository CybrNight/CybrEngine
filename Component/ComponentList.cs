using System;
using System.Collections.Generic;
using CybrEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal class ComponentList {

        private static ComponentList _instance;
        public static ComponentList Instance { 
            get {
                if (_instance == null) {
                    _instance = new ComponentList();
                }
                return _instance;
            }   
        } 
        private ComponentList() { }

        private static Dictionary<Entity, List<Component>> components = new Dictionary<Entity, List<Component>>();

        public Component AddComponent(Entity entity, Type cType){
            Component newComp = null;
            if (!components.ContainsKey(entity)) {
                components.Add(entity, new List<Component>());
            }
        
            if (cType == typeof(Transform)) {
                newComp = new Transform();
            }
            
            if (newComp == null) {
                throw new Exception("Passed component invalid!");
            }
            components[entity].Add(newComp);
            return newComp;
        }

        public Component GetComponent<T>(Entity entity){
            //Check components list for entity
            if (components.ContainsKey(entity)){
                var compList = components[entity];
                //Search components list for component
                for (int i = 0;i < compList.Count; i++){
                    var comp = compList[i];
                    //If match return component;
                    if (comp.GetType() == typeof(T)){
                        return comp;
                    }
                }
            }
            return null;
        }

        public List<Component> GetComponents<T>(Entity entity) {
            //Check components list for entity
            if(components.ContainsKey(entity)) {
                var compList = components[entity];
                return compList;
            }
            return null;
        }
    }
}
