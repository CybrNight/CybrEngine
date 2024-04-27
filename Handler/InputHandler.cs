using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CybrEngine {

    /// <summary>
    /// Static wrapper class to expose InputHandler
    /// </summary>
    public static class Input {

        private static InputHandler _inputHandler;

        static Input() {
            _inputHandler = EngineHandler.Instance.GetHandler<InputHandler>();
        }

        public static bool GetKey(Keys key) {
            return _inputHandler.GetKey(key);
        }

        public static int GetAxisRaw(string axis) {
            return _inputHandler.GetAxisRaw(axis);
        }
        public static float GetAxis(string axis) {
            return _inputHandler.GetAxis(axis);
        }

        public static bool IsAction(string action) {
            return _inputHandler.IsAction(action);
        }

    }

    internal class InputHandler : Handler {

        /// <summary>
        /// Stores all input axes used by InputHandler
        /// </summary>
        private static Dictionary<string, float> axes;

        /// <summary>
        /// Stores all Keys currently pressed, and # of frames pressed
        /// </summary>
        private static Dictionary<Keys, float> pressedKeys = new Dictionary<Keys, float>();

        /// <summary>
        /// Stores reference to InputMap
        /// </summary>
        private static InputMap _inputMap;

        //Initialize base Input axes
        public InputHandler() {
            axes = new Dictionary<string, float>();
            _inputMap = new InputMap();

            axes["Horizontal"] = 0;
            axes["Vertical"] = 0;
            
        }

        private void ResetKey(Keys key) {
            pressedKeys[key] = 0;
        }

        //Update all input Dictionary
        public override void Update() {
            _inputMap.Update();

            var kstate = Keyboard.GetState();

            //Update Horizontal, and Vertical axes with directional int value
            axes["Horizontal"] = (-GetKeyVal(Keys.Left) + GetKeyVal(Keys.Right));
            axes["Vertical"] = (GetKeyVal(Keys.Up) - GetKeyVal(Keys.Down));

            //Check state of all pressedKeys
            foreach(Keys key in kstate.GetPressedKeys()) {
                //If key no longer held, reset pressed time
                if(!pressedKeys.ContainsKey(key)) {
                    ResetKey(key);
                }

                //Incrament pressed frames count;
                if(pressedKeys[key] < 1) {
                    pressedKeys[key] += Time.deltaTime;
                }
            }

            //Check if any Keys released this frame
            foreach(Keys key in pressedKeys.Keys) {
                //If key released reset value in pressedKeys
                if(kstate.IsKeyUp(key)) {
                    ResetKey(key);
                }
            }


        }

        public bool IsAction(string action){
            return _inputMap.IsActionPerformed(action);
        }

        public int GetAxisRaw(string axis) {
            return axes[axis].CeilToInt();
        }

        public float GetAxis(string axis) {
            return axes[axis];
        }

        //Check if Key is currently pressed
        public bool GetKey(Keys key) {
            if(!pressedKeys.ContainsKey(key)) {
                return false;
            }

            return (pressedKeys[key] > 0);
        }

        //Gets current state of a Key as Int
        public int GetKeyVal(Keys key) {
            return GetKey(key).ToInt();
        }

        //Check if Key pressed this frame
        public bool GetKeyDown(Keys key) {
            if(!pressedKeys.ContainsKey(key)) {
                return false;
            }
            return (pressedKeys[key] > 1);
        }
    }
}
