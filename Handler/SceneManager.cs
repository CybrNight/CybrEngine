using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class SceneManager {
        private static SceneManager _instance;
        public static Scene currentScene;

        public static SceneManager Instance {
            get {
                    
                if (_instance == null ) {
                    _instance = new SceneManager();
                }
                return _instance;
            }
        }

        public static void LoadScene(Scene scene){
            currentScene = scene;
            scene.LoadObjects();
        }
    }
}
