using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public partial class Entity{
        internal static class GameObjectFactory<T> where T : Entity {
            internal static T Construct(ObjectAllocator objHandler) {
                var gameObject = Builder.GameObject<T>();

                //Set member variables of gameObject
                gameObject.Name = gameObject.GetType().ToString();
                gameObject.Name = gameObject.GetType().Name;
                gameObject.objAlloc = Autoload.objAllocator;
                gameObject.particleHandler = Autoload.particleHandler; 
                return gameObject;
            }
        }

    }
}
