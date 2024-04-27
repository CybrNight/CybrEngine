using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class ObjectHandler : Handler {

        public bool Active { get; set; }

        public override void FixedUpdate() {
            /*int startSize = handlers.Count;
            foreach(Handler handler in handlers.Values) {
                if(!handler.Enabled) continue;

                handler.FixedUpdate();

                if(startSize != handlers.Count) {
                    return;
                }
            }*/
        }

        public override void Update() {
            /*int startSize = handlers.Count;
            foreach(Handler handler in handlers.Values) {
                if(!handler.Enabled) continue;

                handler.Update();

                if(startSize != handlers.Count) {
                    return;
                }
            }*/
        }

        public override void Draw(SpriteBatch spriteBatch) {
           /* int startSize = handlers.Count;
            foreach(Handler handler in handlers.Values) {
                if(!handler.Enabled) continue;

                handler.Draw(spriteBatch);

                if(startSize != handlers.Count) {
                    return;
                }
            }*/
        }
    }
}
