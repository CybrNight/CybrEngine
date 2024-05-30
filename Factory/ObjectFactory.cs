using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public partial class Object {
        internal static class Factory<T> where T : Object {
            /// <summary>
            /// Constructs new instance of Object
            /// </summary>
            /// <returns></returns>
            public static T Construct(Type[] paramTypes, object[] paramVals) {
                var obj = Builder.Construct<T>(paramTypes, paramVals);
                obj.Name = obj.GetType().ToString();
                return obj;
            }

            /// <summary>
            /// Creates clone of Object instance
            /// </summary>
            /// <returns></returns>
            public static T Instance(Object obj) {
                var clone = obj.MemberwiseClone() as T;
                return clone;
            }
        }
    }

    public partial class GameObject{
        internal static class GameObjectFactory<T> where T : GameObject {
            internal static T Construct(ObjectHandler objHandler) {
                var gameObject = Builder.GameObject<T>();

                //Set member variables of gameObject
                gameObject.Active = true;
                gameObject.Name = gameObject.GetType().ToString();
                gameObject.Name = gameObject.GetType().Name;
                gameObject.objHandler = Autoload.objHandler;
                gameObject.particleHandler = Autoload.particleHandler; 
                return gameObject;
            }
        }

    }
}
