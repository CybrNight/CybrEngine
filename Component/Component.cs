﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Component {
        private string name;
        public string Name { get { return name; } set { name = value; } }
    }
}
