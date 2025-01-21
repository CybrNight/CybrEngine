using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal static class Autoload{

        public static SignalBus SignalBus;
        public static EntityAllocator EntityAllocator;
        public static ComponentAllocator ComponentAllocator;
        public static InputHandler InputHandler;
        public static ParticleStore ParticleHandler;
        public static SceneManager SceneManager;
        public static PhysicsHandler PhysicsHandler;

        public static void Reset(){
            EntityAllocator.Reset();
            InputHandler.Reset();
            ParticleHandler.Reset();
            SceneManager.Reset();
            PhysicsHandler.Reset();
        }

        public static void Construct(){
            SignalBus = SignalBus.Instance; 
            EntityAllocator = EntityAllocator.Instance;
            ComponentAllocator = ComponentAllocator.Instance;
            InputHandler = InputHandler.Instance;
            ParticleHandler = ParticleStore.Instance; 
            SceneManager = SceneManager.Instance;
        }
    }
}
