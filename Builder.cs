using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal static class Builder {
        public static T Component<T>(int entity) where T : Component {
            T newComponent = (T)Activator.CreateInstance(typeof(T), true);
            return newComponent;
        }

        public static T Entity<T>() where T : Entity {
            T newComponent = (T)Activator.CreateInstance(typeof(T), true);
            return newComponent;
        }

        public static T Object<T>(object[] deps = null) where T : Object {
            T newComponent = (T)Activator.CreateInstance(typeof(T), true);
            return newComponent;
        }
    }
}
