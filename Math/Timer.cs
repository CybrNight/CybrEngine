using System;

namespace CybrEngine {
    public class Timer : IMessageable {

        public bool IsActive { get; private set; }
        public float Counter { get; private set; }
        public float Max { get; set; }
        public bool Loop { get; set; }
        public bool Fixed { get; set; }

        public event Action OnTimeout;

        public Timer(float max, bool loop = false, bool fix = false ) {
            Max = max;
            Loop = loop;
            Time.alarms.Add(this);
        }

        private void Timeout() {
            OnTimeout?.Invoke();
            Counter = 0;

            if(!Loop) {
                IsActive = false;
            }
        }

        public void Start() {
            Counter = 0;
            IsActive = true;
        }

        private void Tick() {
            if(Counter >= Max) {
                Timeout();    
            }

            Counter += Time.deltaTime;
        }
    }
}
