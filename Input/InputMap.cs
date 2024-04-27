using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {


    struct InputAction {
        string Name { get; set; }
        public List<Keys> Bindings { get; private set; }

        public float heldTime;
        

        public InputAction(string name, Keys key) {
            Name = name;
            heldTime = 0;
            Bindings = new List<Keys>() { key };
        }

        public void AddBinding(Keys key){
            Bindings.Add(key);
        }

        public void Perform(){
            heldTime += Time.deltaTime;
        }

        public void Reset(){
            heldTime = 0;
        }
     
        public bool IsPressed {
            get {
                return (heldTime > 0);
            }
        }
    }

    public class InputMap {

        private Dictionary<string, InputAction> _inputMap;
    
        public InputMap() {
            _inputMap = new Dictionary<string, InputAction>();

            //Add default movement keys to InputMap
            AddInput("ui_left", Keys.Left);
            AddInput("ui_right", Keys.Right);
            AddInput("ui_up", Keys.Up);
            AddInput("ui_down", Keys.Down);
        }

        public bool IsActionPerformed(string action){
            return _inputMap[action].IsPressed;
        }

        public void Update(){
            var kstate = Keyboard.GetState();
            int startSize = kstate.GetPressedKeyCount();
            foreach(InputAction action in _inputMap.Values ){
                int innerSize = action.Bindings.Count;
                foreach(Keys key in action.Bindings){
                    if (kstate.IsKeyDown(key)){
                        action.Perform();
                        continue;
                    }

                    if(kstate.IsKeyUp(key)){
                        action.Reset();
                        continue;
                    }
                }
            }
        }

        public void AddInput(string name, Keys key){
            _inputMap[name] = new InputAction(name, key);
        }
    }
}
