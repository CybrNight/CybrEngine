using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public interface IMessageable {

        public abstract void SendMessage(string name, object[] args = null);
        
    }
}
