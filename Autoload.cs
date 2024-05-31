using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal static class Autoload{

        public static ObjectAllocator objAllocator;
        public static InputHandler inputHandler;
        public static ParticleHandler particleHandler;
        public static SceneManager sceneManager;

        public static void Reset(){
            objAllocator.Reset();
            inputHandler.Reset();
            particleHandler.Reset();
            sceneManager.Reset();
        }

        static Autoload(){
            objAllocator = ObjectAllocator.Instance;
            inputHandler = InputHandler.Instance;
            particleHandler = ParticleHandler.Instance; 
            sceneManager = SceneManager.Instance;
        }
    }
}
