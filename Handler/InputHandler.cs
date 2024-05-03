using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace CybrEngine {

    /// <summary>
    /// Static wrapper class to expose InputHandler
    /// </summary>
    public static class Input {

        private static InputHandler _inputHandler;

        static Input() {
            _inputHandler = InputHandler.Instance;
        }

        public static bool GetKey(Keys key) {
            return _inputHandler.GetKey(key);
        }

        public static bool GetKeyDown(Keys key){
            return _inputHandler.GetKeyDown(key);
        }

        public static int GetAxisRaw(string axis) {
            return _inputHandler.GetAxisRaw(axis);
        }
        public static float GetAxis(string axis) {
            return _inputHandler.GetAxis(axis);
        }
    }

    internal class InputHandler : IMessageable {
        private static InputHandler _instance;
        public static InputHandler Instance {
            get {
                if(_instance == null) {
                    _instance = new InputHandler();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Stores all input axes used by InputHandler
        /// </summary>
        private static Dictionary<string, float> axes;

        /// <summary>
        /// Stores all Keys currently pressed, and # of frames pressed
        /// </summary>
        private static Dictionary<Keys, float> pressedKeys = new Dictionary<Keys, float>();

        //Initialize base Input axes
        private InputHandler() {
            axes = new Dictionary<string, float>();

            axes["Horizontal"] = 0;
            axes["Vertical"] = 0;

        }

        private void ResetKey(Keys key) {
            pressedKeys[key] = 0;
        }

        //Update all input Dictionary
        public void Update() {
            var kstate = Keyboard.GetState();

            foreach(Keys key in pressedKeys.Keys) {
                if (!kstate.IsKeyDown(key)){
                    ResetKey(key);
                }
            }

            //Update Horizontal, and Vertical axes with directional int value
            axes["Horizontal"] = (-GetKeyVal(Keys.Left) + GetKeyVal(Keys.Right));
            axes["Vertical"] = (GetKeyVal(Keys.Up) - GetKeyVal(Keys.Down));

            //Check state of all pressedKeys
            foreach(Keys key in kstate.GetPressedKeys()) {
                //If key no longer held, reset pressed time
                if(!pressedKeys.ContainsKey(key)) {
                    ResetKey(key);
                }

                pressedKeys[key]++;
            }
        }

        public int GetAxisRaw(string axis) {
            return Mathf.CeilToInt(axes[axis]);
        }

        public float GetAxis(string axis) {
            return axes[axis];
        }

        //Check if Key is currently pressed
        public bool GetKey(Keys key) {
            var kstate = Keyboard.GetState();
            if(!pressedKeys.ContainsKey(key)) {
                return false;
            }

            return pressedKeys[key] > 0;
        }

        //Gets current state of a Key as Int
        public int GetKeyVal(Keys key) {
            return GetKey(key).ToInt();
        }

        //Check if Key pressed this frame
        public bool GetKeyDown(Keys key) {
            if(!pressedKeys.ContainsKey(key)) {
                pressedKeys[key] = 0;
            }
            Debug.WriteLine("Press:"+pressedKeys[key]);
            return (pressedKeys[key] >= 1 && pressedKeys[key] <= 2);
        }
    }
}
