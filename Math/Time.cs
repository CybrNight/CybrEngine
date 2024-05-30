using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CybrEngine {
    public static class Time {

        public static GameTime gameTime;

        public static float fixedUpdateAlpha;
        public static float fixedUpdateMult = 0;
        public static float elapsedTime;
        public static float deltaTime;
        public static float fixedDeltaTime;
        public static float fixedTime;
        public static float frameTime;
        public static TimeSpan fixedUpdateRate;

        public static List<Timer> alarms = new List<Timer>();

        public static void Update(ref GameTime gameTime) {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            for(int i = 0; i < alarms.Count; i++) {
                Timer alarm = alarms[i];
                if (alarm.IsActive){
                    alarm.SendMessage("Tick");
                }
            }
        }

        public static void FixedUpdate(ref GameTime gameTime) {
            fixedDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
