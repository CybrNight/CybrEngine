using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class SceneManager : IResettable {
        private static SceneManager _instance;
        public static Scene CurrentScene { private set; get; }

        private Queue<Entity> buildQueue;

        public static SceneManager Instance {
            get {
                    
                if (_instance == null ) {
                    _instance = new SceneManager();
                }
                return _instance;
            }
        }
        private SceneManager(){ 
            buildQueue = new Queue<Entity>();
        }

        public T Instantiate<T>(float x, float y) where T : Entity {
            return Instantiate<T>(new Vector2(x, y));
        }

        internal void Update(){
            AddInstantiatedObjects();
            CurrentScene.Update();
        }

        /// <summary>
        /// Runs every physics tick. Runs FixedUpate all on Entity
        /// </summary>
        public void FixedUpdate() {
            CurrentScene.FixedUpdate();
        }

        private void AddInstantiatedObjects() {
            while(buildQueue.Count > 0) {
                var obj = buildQueue.Dequeue();
                CurrentScene.Root.AddChild(obj);
                obj.SendMessage("_Start");
                obj.SetActive(true);
            }
        }

        public Entity FindEntityOfType<T>() where T : Entity{
            return CurrentScene.Root.Find<T>();
        }

        public T Instantiate<T>(Vector2 position) where T : Entity {
            var entity = Entity.Construct<T>();

            entity.Transform.Position = position;

            entity.SendMessage("_Awake");
            buildQueue.Enqueue(entity);   
           
            return (T)entity;
        }

        public static void LoadScene(Scene scene){
            CurrentScene = scene;
            scene.LoadObjects();
        }

        public void Reset() {
            CurrentScene = null;
        }
    }
}
