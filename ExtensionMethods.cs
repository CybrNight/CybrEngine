using System;

namespace CybrEngine {
    public static class ExtensionMethods {

        public static int ToInt(this bool value) {
            return value ? 1 : 0;
        }

        public static int CeilToInt(this float value) {
            return (int)Math.Ceiling(value);
        }
    }
}
