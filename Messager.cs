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

        /// <summary>
        /// Invokes private method with name on instance
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        public static void SendMessage(object instance, string name, object[] args = null) {
            //If first time messaging this instance, add new cache for it, and return
            if(!msgCache.ContainsKey(instance)) {
                msgCache[instance] = new Dictionary<string, MethodInfo>();
                InvokeMethod(instance, name);
            }else{
                var cache = msgCache[instance];
                if(cache.ContainsKey(name)) {
                    var method = cache[name];
                    try {
                        method.Invoke(instance, args);
                    } catch(TargetException){
                        Debug.WriteLine("Type mismatch between " + instance + " and " + method);
                        throw new MessageException();
                    }
                } else {
                    var method = Builder.MethodCall(instance, name);
                    if(method != null) {
                        msgCache[instance].Add(name, method);
                        method.Invoke(instance, args);
                    }
                }
            }
        }

        private static void InvokeMethod(object instance, string name, object[] args = null){
            var method = Builder.MethodCall(instance, name);
            if(method != null) {
                //msgCache[instance].Add(name, method);
                method.Invoke(instance, args);
            }
        }

    }


}
