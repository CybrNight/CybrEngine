using System;
using System.Reflection;

namespace CybrEngine {
    internal static class Builder {
        public static MethodInfo MethodCall(object instace, string name) {
            // Get the type of MyClass
            Type type = instace.GetType();

            // Get the method information using reflection
            MethodInfo method = type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);

            return method;
        }

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
