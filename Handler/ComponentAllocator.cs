﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CybrEngine {
    internal class ComponentAllocator : IResettable {

        private Dictionary<Entity, List<Component>> cMap;

        /// <summary>
        /// Gets all Components from Entity
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Component> GetAllComponents(Entity key){
            return cMap[key];
        }

        public ComponentAllocator() {
            cMap = new Dictionary<Entity, List<Component>>();
        }

        /// <summary>
        /// Update all 
        /// </summary>
        public void Update(){
            
        }

        /// <summary>
        /// Draw all Components
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch){
            foreach(var cList in cMap.Values) {
                foreach(var c in cList) {
                    c.Draw(spriteBatch);
                }
            }
        }

        public void RemoveComponents(Entity key) {
            if(cMap.ContainsKey(key)) {
                for(int i = 0; i < cMap[key].Count; i++) {
                    cMap[key][i].Destroy();
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

            if(!cMap.ContainsKey(entity)) {
                cMap[entity] = new List<Component>();
            }

            cMap[entity].Add(component);
            return component;
        }

        /// <summary>
        /// Retrieves Component of type T from Entity at CINDEX
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetComponent<T>(Entity key) where T : Component {
            if(!cMap.ContainsKey(key)) {
                return null;
            }

            var cList = cMap[key];
            return (T)cList.Find(e => e is T);
        }

        /// <summary>
        /// Returns a List<Component> of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> GetComponents<T>(Entity key) where T : Component {
            if(!cMap.ContainsKey(key)) {
                return null;
            }

            var cList = cMap[key];
            List<T> result = new List<T>();

            for(int i = 0; i < cList.Count; i++) {
                var component = cList[i];
                if(component is T) {
                    result.Add((T)component);
                }
            }
            return result;
        }

        public void Reset() {
            cMap.Clear();
        }
    }
}
