using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CybrEngine {
    internal class TickHandler {

        private ObjectHandler _objHandler;
        private InputHandler _inputHandler;
        private readonly int DEFAULT_FIXED_UPDATE_RATE = Config.FIXED_UPDATE_FPS;

        private static TickHandler _instance;
        public static TickHandler Instance {
            get {
                if(_instance == null) {
                    _instance = new TickHandler(ObjectHandler.Instance, InputHandler.Instance);
                }
                return _instance;
            }
        }

        private TickHandler(ObjectHandler objHandler, InputHandler inputHandler) {
            _objHandler = objHandler;
            _inputHandler = inputHandler;

            // TODO: Add your initialization logic here
            fixedUpdateRate = (int)(Config.FIXED_UPDATE_FPS == 0 ? 0 : (1000 / (float)Config.FIXED_UPDATE_FPS));
            Time.fixedUpdateRate = TimeSpan.FromTicks((long)TimeSpan.TicksPerSecond / Config.FIXED_UPDATE_FPS);
            Time.fixedUpdateMult = (float)DEFAULT_FIXED_UPDATE_RATE / Config.FIXED_UPDATE_FPS;
        }

        private int fixedUpdateRate;
        private float fixedUpdateElapsedTime = 0;
        private float fixedUpdateDelta = 0.33f;
        private float previousT = 0;
        private float accumulator = 0.0f;
        private float maxFrameTime = 250;
        public void Update(GameTime gameTime) {
            if(gameTime.ElapsedGameTime.TotalSeconds > 0.1) {
                accumulator = 0;
                previousT = 0;
                return;
            }

            if(previousT == 0) {
                fixedUpdateDelta = fixedUpdateRate;
                previousT = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            _objHandler.Update();
            _inputHandler.Update();
            Time.Update(ref gameTime);

            float now = (float)gameTime.TotalGameTime.TotalMilliseconds;
            Time.frameTime = now - previousT;

            if(Time.frameTime > maxFrameTime) {
                Time.frameTime = maxFrameTime;
            }
            previousT = now;

            accumulator += Time.frameTime;

            while(accumulator >= fixedUpdateDelta) {
                FixedUpdate();
                Time.FixedUpdate(ref gameTime);
                fixedUpdateElapsedTime += fixedUpdateDelta;
                accumulator -= fixedUpdateDelta;
            }

            Time.fixedUpdateAlpha = (float)(accumulator / fixedUpdateDelta);
        }

        public void Draw(SpriteBatch spriteBatch) {
            _objHandler.Draw(spriteBatch);
            // TODO: Add your drawing code here
        }

        private void FixedUpdate() {
            _objHandler.FixedUpdate();
        }
    }
}
