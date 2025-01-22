using System;
using System.Collections.Generic;

namespace CybrEngine {
    
    public static class SignalExtensions{
        public static int Emit(string channel, object[] args) {
            return SignalBus.Emit(channel, args);
        }

    }

    public class SignalBus {

        /// <summary>
        /// Defines singleton reference to SignalBus
        /// </summary>
        private static Dictionary<string, List<EventHandler>> channels;
        public delegate void EventHandler(object[] args);

        private static SignalBus _instance;
        public static SignalBus Instance {
            get {
                if(_instance == null) {
                    _instance = new SignalBus();
                }
                return _instance;
            }
        }

        private SignalBus(){
            channels = new Dictionary<string, List<EventHandler>>();
        }

        /// <summary>
        /// Defines Signal which contains reference to 
        /// </summary>
        public class Signal {
            private EventHandler _handler;
            private string _channel;
            public bool IsDisposed { get; private set; } = false;
            public Signal(string channel, EventHandler handler) {
                this._handler = handler;
                this._channel = channel;
            }
            public void Dispose() {
                if(IsDisposed) return;
                IsDisposed = true;
                if(channels.TryGetValue(_channel, out var handlers))
                    handlers.Remove(_handler);
            }
        }

        public static void Connect(string channel, EventHandler handler) {
            List<EventHandler> listeners;
            if(channels.ContainsKey(channel))
                listeners = channels[channel];
            else
                channels[channel] = listeners = new();
            listeners.Add(handler);
        }

        public static int Emit(string channel){
            return Emit(channel, new object[] { });
        }

        public static int Emit(string channel, params object[] args) {
            if(!channels.TryGetValue(channel, out var handlers)) return 0;
            handlers.ForEach(handler => handler(args));
            return handlers.Count;
        }
    }
}


