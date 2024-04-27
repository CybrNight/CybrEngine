using System;
using System.Collections.Generic;
using CybrEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace CybrEngine {
    public class ComponentList {

        //Reference to world entity that component is attached to
        private Entity entity;

        /// <summary>
        /// Stores all components in ComponentList
        /// </summary>
        private readonly Dictionary<Type, List<Component>> cList;
        public ComponentList(Entity entity) {
            this.entity = entity;
            cList = new Dictionary<Type, List<Component>>();
        }

        /// <summary>
        /// Destroy ComponentList
        /// </summary>
        public void Destroy() {
            entity = null;
            foreach(List<Component> list in cList.Values) {
                int innerSize = list.Count;
                foreach(Component component in list) {
                    component.Destroy();
                }
            }
            cList.Clear();    
        }

        /// <summary>
        /// Adds new Component of type T to ComponentList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public T AddComponent<T>(T component) where T : Component {
            Type cType = component.ComponentType; //Get ComponentType

            //Create new List<Component> for T if not exist
            if(!cList.ContainsKey(cType)) {
                cList.Add(cType, new List<Component>());
            }

            //Append new Component to cList at key cType
            cList[cType].Add(component);

            //Set Component Entity reference and Init
            component.Init(entity);

            return component;
        }

        /// <summary>
        /// Gets Component of type T from ComponentList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : Component {
            Type cType = typeof(T);

            //Check if there is Component of type T
            if(!cList.ContainsKey(typeof(T))) {
                return null;
            }

            //Return the first Component of type T
            return (T)cList[cType].First();
        }

        /// <summary>
        /// Gets all Components of type T from ComponentList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetComponents<T>() where T : Component {
            //Check components list for entity
            Type cType = typeof(T);

            List<T> result = new List<T>(cList[cType].Count);
            foreach(Component c in cList[cType]) {
                result.Add((T)c);
            }
            return result;
        }

        /// <summary>
        /// Updates all Components in ComponentList
        /// </summary>
        public void UpdateAll() {
            int startSize = cList.Count;
            foreach(List<Component> list in cList.Values) {
                int innerSize = list.Count;
                foreach(Component component in list) {
                    (component).Update();
                    if(innerSize != list.Count) {
                        return;
                    }
                }
            }
            if(startSize != cList.Count) {
                return;
            }
        }

        /// <summary>
        /// Draws all Components in ComponentList of type IDrawComponent
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawAll(SpriteBatch spriteBatch) {
            int startSize = cList.Count;
            //Iterate over every List<Component> in ComponentList
            foreach(List<Component> list in cList.Values) {
                int innerSize = list.Count;
                //Iterate over every IDrawComponent
                foreach(Component component in list) {
                    
                    //If component is IDrawComponent then Draw()
                    if(component is IDrawComponent) {
                        (component as IDrawComponent).Draw(spriteBatch);

                        //If List size changed during iteration reset
                        if(innerSize != list.Count) {
                            return;
                        }
                    }
                }

                //If Dictionary size change during iteration reset
                if(startSize != cList.Count) {
                    return;
                }
            }
        }
    }
}
