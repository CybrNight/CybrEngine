using System;
using System.Collections.Generic;
using CybrEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace CybrEngine {
    internal class ComponentList {

        //Reference to world entity that component is attached to
        private Entity owner;

        private Dictionary<Type, List<Component>> cList = new Dictionary<Type, List<Component>>();

        public ComponentList(Entity owner) {
            this.owner = owner;
        }

        public void Destroy(){
            owner = null;
            cList.Clear();
        }

        public T AddComponent<T>(T component) where T : Component{
            Type cType = component.ComponentGroup;

            if (!cList.ContainsKey(cType)) {
                cList.Add(cType, new List<Component>());
            }

            cList[cType].Add(component);
            component.Owner = owner;

            return component;
        }

        public T GetComponent<T>() where T : Component{
            //Check components list for entity
            Type cType = typeof(T);
        
            if (!cList.ContainsKey(typeof(T))) {
                return null;
            }
            return (T)cList[cType].First();
        }

        public List<T> GetComponents<T>() where T : Component {
            //Check components list for entity
            Type cType = typeof(T);

            List<T> result = new List<T>(cList[cType].Count);
            foreach (IComponent c in cList[cType]){
                result.Add((T)c);
            }
            return result;
        }

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

            public void DrawAll(SpriteBatch batch) {
                int startSize = cList.Count;
                foreach(List<Component> list in cList.Values) {
                    int innerSize = list.Count;
                    foreach(Component component in list) {
                        if(component is IDrawComponent) {
                            (component as IDrawComponent).Draw(batch);
                            if(innerSize != list.Count) {
                                return;
                            }
                        }
                    }
                    if(startSize != cList.Count) {
                        return;
                    }
                }
        }
    }
    }
