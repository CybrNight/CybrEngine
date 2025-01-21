using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CybrEngine;

internal class PhysicsHandler : IResettable {

        public void Reset() {

        }

        /// <summary>
        /// Runs every physics tick. Runs FixedUpate all on Entity
        /// </summary>
        public void FixedUpdate() {
            var ents = Autoload.EntityAllocator.GetAll();
            for(int i = 0; i < ents.Count; i++) {
                var e1 = ents[i];

                //If Entity IsActive, then run _FixedUpdate
                if(e1.IsActive) {
                    e1.SendMessage("_FixedUpdate");

                    //Update Position based on Velocity
                    e1.Transform.Position = new Vector2(e1.Transform.Position.X + e1.Velocity.X,
                                             e1.Transform.Position.Y - e1.Velocity.Y);

                    /*Check all Entity for collision
                    for(int j = 0; j < ents.Count; j++) {
                        var e2 = ents[j];
                        if(e1 != e2 && e2.IsActive) {
                            if(e1.Intersects(e2)) {
                                e1.SendMessage("_OnIntersection", e2);
                                e2.SendMessage("_OnIntersection", e1);
                            }
                        }
                    }*/
                }
            }
        }
    }
