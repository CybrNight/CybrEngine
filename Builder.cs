using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal static class Builder {
        public static T Component<T>() where T : Component {
            T component = (T)Activator.CreateInstance(typeof(T), true);
            return component;
        }

        public static T Entity<T>() where T : Entity {
            T entity = (T)Activator.CreateInstance(typeof(T), true);
            return entity;
        }

        public static T Object<T>(object[] deps = null) where T : Object {
            T obj = (T)Activator.CreateInstance(typeof(T), true);
            return obj;
        }
    }
}
