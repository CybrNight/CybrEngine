using Microsoft.Xna.Framework;
using System;

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

        public static void Update(ref GameTime gameTime) {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public static void FixedUpdate(ref GameTime gameTime) {
            fixedDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
