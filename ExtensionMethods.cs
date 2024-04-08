using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public static class ExtensionMethods {

        public static Component GetComponent<T>(this Entity entity){
            return entity.GetComponent<T>();
        }

        public static void AddComponent(this Entity entity, Type cType){
            entity.AddComponent(cType);
        }
    }
}
