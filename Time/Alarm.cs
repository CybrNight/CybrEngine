using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine{
    public class Alarm : IMessageable, IDisposable{

        public bool IsActive { get; private set; }
        public float Counter { get; private set; }
        public float Max { get; set; }
        public bool Loop { get; set; }

        public event Action Callback;

        public Alarm(float max, bool loop = false){
            Max = max;
            Loop = loop;
            Time.alarms.Add(this);
        }

        public void Reset(){
            Callback.Invoke();
            Counter = 0;
            if(!Loop) {
                Dispose();
            }
        }

        public void Start(){
            IsActive = true;
        }

        private void Tick(){
            if(!IsActive) return;
        
            Counter += Time.deltaTime;

            if (Counter >= Max){
                Reset();
            }
        }

        public void SendMessage(string name, object[] args = null) {
            Messager.Instance.SendMessage(this, name, args);
        }

        public void Dispose() {
            Callback = null;   
        }
    }
}
