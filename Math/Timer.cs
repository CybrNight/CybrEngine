using System;

namespace CybrEngine {
    public class Timer : IMessageable {

        public bool IsActive { get; private set; }
        public float Counter { get; private set; }
        public float Max { get; set; }
        public bool Loop { get; set; }
        public bool Fixed { get; set; }

        public event Action Callback;

        public Timer(float max, bool loop = false, bool fix = false ) {
            Max = max;
            Loop = loop;
            Time.alarms.Add(this);
        }

        public void Reset() {
            Callback.Invoke();
            Counter = 0;
        }

        public void Start() {
            IsActive = true;
        }

        private void Tick() {
            if(Counter >= Max) {
                Reset();
            }

            Counter += Time.deltaTime;
        }
    }
}
