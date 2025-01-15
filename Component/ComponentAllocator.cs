using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CybrEngine {
    internal class ComponentAllocator : IResettable {

        private Dictionary<Entity, List<Component>> cMap;
        private Dictionary<Type, List<Component>> cTypeMap;
        private Queue<Entity> dQueue;
        /// <summary>
        /// Gets all Components from Entity
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Component> GetAllComponents(Entity key){
            return cMap[key];
        }

        private static ComponentAllocator _instance;
        public static ComponentAllocator Instance {
            get {
                if(_instance == null) {
                    _instance = new ComponentAllocator();
                }
                return _instance;
            }
        }

        private ComponentAllocator() {
            cMap = new Dictionary<Entity, List<Component>>();
            cTypeMap = new Dictionary<Type, List<Component>>();
             dQueue = new Queue<Entity>();

        }

        /// <summary>
        /// Update all 
        /// </summary>
        public void Update(){
            if(dQueue.Count > 0) {
                while(dQueue.Count > 0) { 
                    var e = dQueue.Dequeue();
                    RemoveComponents(e);
                }
                return;
            }

            var startSize = cMap.Count;
            foreach(var cList in cMap.Values) {
                var innerSize = cList.Count;
                foreach(var c in cList) {
                    c.Update();

                    if(innerSize != cList.Count) {
                        return;
                    }
                }

                if(startSize != cMap.Count) {
                    return;
                }
            }
        }

        /// <summary>
        /// Draw all Components
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch){
            var startSize = cMap.Count;
            foreach(var cList in cMap.Values) {
                var innerSize = cList.Count;
                foreach(var c in cList) {
                    c.Draw(spriteBatch);

                    if(innerSize != cList.Count) {
                        return;
                    }
                }

                if(startSize != cMap.Count) {
                    return;
                }
            }
        }

        public void RemoveEntity(Entity entity) {
            dQueue.Enqueue(entity);
        }

        public void RemoveComponents(Entity key) {
            if(cMap.ContainsKey(key)) {
                for(int i = 0; i < cMap[key].Count; i++) {
                    var c = cMap[key][i];
                    var cType = cMap[key][i].GetType();

                    c.Destroy();
                }
                cMap.Remove(key);
            }
        }

        /// <summary>
        /// Creates Component ot type T and binds to Entity at index CINDEX
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddComponent<T>(Entity entity) where T : Component  {
            var component = Component.Create<T>(entity);

            //Initialize entries for both caches
            if(!cMap.ContainsKey(entity)) {
                cMap[entity] = new List<Component>();
            }

            cMap[entity].Add(component);
            component.SendMessage("_Awake");
            return component;
        }

        /// <summary>
        /// Retrieves Component of type T from Entity at CINDEX
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Component GetComponent<T>(Entity key) {
            if(!cMap.ContainsKey(key)) {
                return default(Component);
            }

            var cList = cMap[key];
            return cList.Find(e => e is T);
        }

        /// <summary>
        /// Returns a List<Component> of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Component> GetComponents<T>(Entity key) where T : Component {
            if(!cMap.ContainsKey(key)) {
                return default(List<Component>);
            }

            var cList = cMap[key];
            List<Component> result = new List<Component>();

            for(int i = 0; i < cList.Count; i++) {
                var component = cList[i];
                if(component is T) {
                    result.Add(component);
                }
            }
            return result;
        }

        public List<Component> GetComponentsOfType<T>(Type cType){
            if (!cTypeMap.ContainsKey(cType)){
                return default(List<Component>);
            }

            return cTypeMap[cType];
        }

        public void Reset() {
            cMap.Clear();
        }
    }
}
