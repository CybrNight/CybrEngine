using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal static class Autoload{

        public static TickHandler tickHandler;
        public static ObjectHandler objHandler;
        public static InputHandler inputHandler;
        public static ParticleHandler particleHandler;
        public static SceneManager sceneManager;

        static Autoload(){
            tickHandler = TickHandler.Instance;
            objHandler = ObjectHandler.Instance;
            inputHandler = InputHandler.Instance;
            particleHandler = ParticleHandler.Instance; 
            sceneManager = SceneManager.Instance;
        }
    }
}
