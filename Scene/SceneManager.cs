using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class SceneManager : IResettable {
        private static SceneManager _instance;
        public static Scene currentScene;

        private Queue<Entity> buildQueue;

        public static SceneManager Instance {
            get {
                    
                if (_instance == null ) {
                    _instance = new SceneManager();
                }
                return _instance;
            }
        }
        private SceneManager(){ }

        public T Instantiate<T>(float x, float y) where T : Entity {
            return Instantiate<T>(new Vector2(x, y));
        }

        public T Instantiate<T>(Vector2 position) where T : Entity {
            var entity = Entity.Construct<T>();
            entity.Transform.Position = position;

            entity.SendMessage("_Awake");
            return (T)entity;
        }

        public static void LoadScene(Scene scene){
            currentScene = scene;
            scene.LoadObjects();
        }

        public void Reset() {
            currentScene = null;
        }
    }
}
