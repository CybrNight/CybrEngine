using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class DefaultScene : Scene {
        public override void LoadObjects() {
            Debug.WriteLine("Default Scene loaded");    
        }
    }
}
