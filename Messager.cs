using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    internal class Messager {

        private static Messager _instance;
        public static Messager Instance {
            get {
                if(_instance == null) {
                    _instance = new Messager();
                }
                return _instance;
            }
        }

        private static Dictionary<object, Dictionary<string, MethodInfo>> msgCache;

        private Messager() {
            msgCache = new Dictionary<object, Dictionary<string, MethodInfo>>();
        }

        public void SendMessage(object instance, string name, object[] args = null) {
            if(!msgCache.ContainsKey(instance)) {
                msgCache[instance] = new Dictionary<string, MethodInfo>();
            }

            var cache = msgCache[instance];
            if(cache.ContainsKey(name)) {
                cache[name].Invoke(instance, args);
            } else {
                var method = Builder.MethodCall(instance, name);
                if(method != null) {
                    msgCache[instance].Add(name, method);
                    method.Invoke(instance, args);
                }
            }
        }
    }
}
