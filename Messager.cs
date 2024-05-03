using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace CybrEngine {
    public static class MesssageExtensions {
        public static void SendMessage(this IMessageable obj, string message) {
            Messager.SendMessage(obj, message);
        }
    }

    internal static class Messager {

        private static Dictionary<object, Dictionary<string, MethodInfo>> msgCache;

        static Messager() {
            msgCache = new Dictionary<object, Dictionary<string, MethodInfo>>();
        }

        public static void SendMessage(object instance, string name, object[] args = null) {
            if(!msgCache.ContainsKey(instance)) {
               // msgCache[instance] = new Dictionary<string, MethodInfo>();
            }
            var method = Builder.MethodCall(instance, name);
            if(method != null) {
               // msgCache[instance].Add(name, method);
                method.Invoke(instance, args);
            }
        }
    }
}
