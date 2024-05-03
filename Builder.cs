using System;
using System.Reflection;

namespace CybrEngine {
    internal static class Builder {

        public static T Construct<T>(){
            return Construct<T>(new Type[] { }, new object[]{});
        }
        public static T Construct<T>(Type[] paramTypes, object[] paramValues) {
            Type t = typeof(T);

            ConstructorInfo ci = t.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null, paramTypes, null);

            return (T)ci.Invoke(paramValues);
        }

        public static MethodInfo MethodCall(object instace, string name) {
            // Get the type of MyClass
            Type type = instace.GetType();

            // Get the method information using reflection
            MethodInfo method = type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);

            return method;
        }

        public static T Component<T>() where T : Component {
            T instance = (T)Activator.CreateInstance(typeof(T), true);
            return instance;
        }

        public static T GameObject<T>() where T : GameObject {
            T entity = (T)Activator.CreateInstance(typeof(T), true);
            return entity;
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
