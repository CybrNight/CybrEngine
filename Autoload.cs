using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal static class Autoload{

        public static EntityAllocator ObjectAllocator;
        public static ComponentAllocator ComponentAllocator;
        public static InputHandler InputHandler;
        public static ParticleStore ParticleHandler;
        public static SceneManager SceneManager;
        public static PhysicsHandler PhysicsHandler;

        public static void Reset(){
            ObjectAllocator.Reset();
            InputHandler.Reset();
            ParticleHandler.Reset();
            SceneManager.Reset();
            PhysicsHandler.Reset();
        }

        static Autoload(){
            ObjectAllocator = EntityAllocator.Instance;
            ComponentAllocator = ComponentAllocator.Instance;
            InputHandler = InputHandler.Instance;
            ParticleHandler = ParticleStore.Instance; 
            SceneManager = SceneManager.Instance;
        }
    }
}
