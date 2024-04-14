using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace CybrEngine {
    public static class Input {

        public static bool GetKey(Keys key){
            var kstate = Keyboard.GetState();
            return (kstate.IsKeyDown(key));
        }

        public static bool GetKeyDown(Keys key) {
            var kstate = Keyboard.GetState();
            return (kstate.IsKeyDown(key));
        }
    }
}
