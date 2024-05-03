using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CybrEngine {
    internal class ComponentAllocator {

        private Dictionary<GameObject, List<Component>> cMap;
        private ObjectHandler _objHandler;

        /// <summary>
        /// Gets all Components from Entity
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Component> GetAllComponents(GameObject key){
            return cMap[key];
        }

        public ComponentAllocator() {
            cMap = new Dictionary<GameObject, List<Component>>();
        }

        public void Update(){
            foreach(var cList in cMap.Values){
                foreach(var c in cList) {
                    c.SendMessage("_Update");
                }
            }   
        }

        public void Draw(SpriteBatch spriteBatch){
            foreach(var cList in cMap.Values) {
                foreach(var c in cList) {
                    if(c is Sprite) {
                        var sprite = (Sprite)c;
                        if(sprite == null || sprite.Texture == null) continue;

                        var transform = sprite.Entity.Transform;
                        spriteBatch.Draw(sprite.Texture, transform.Position, null, sprite.Color, 0f,
                        transform.Origin,
                        transform.Scale,
                        SpriteEffects.None,
                        0f);
                    }
                }
            }
        }

        public void RemoveComponents(GameObject key) {
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
        public T AddComponent<T>(GameObject entity) where T : Component  {
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
        public T GetComponent<T>(GameObject key) where T : Component {
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
        public List<T> GetComponents<T>(GameObject key) where T : Component {
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
    }
}
