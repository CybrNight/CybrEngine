using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public static class ExtensionMethods {

        public static int ToInt(this bool value){
            return value ? 1 : 0;
        }

        public static int CeilToInt(this float value){
            return (int)Math.Ceiling(value);
        }
    }
}
