using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CybrEngine {
    internal class TickHandler {

        private ObjectAllocator _objHandler;
        private InputHandler _inputHandler;
        private readonly int DEFAULT_FIXED_UPDATE_RATE = Config.FIXED_UPDATE_FPS;

        private static TickHandler _instance;
        public static TickHandler Instance {
            get {
                if(_instance == null) {
                    _instance = new TickHandler(ObjectAllocator.Instance, InputHandler.Instance);
                }
                return _instance;
            }
        }

        private TickHandler(ObjectAllocator objHandler, InputHandler inputHandler) {
            _objHandler = objHandler;
            _inputHandler = inputHandler;

            // TODO: Add your initialization logic here
            fixedUpdateRate = (int)(Config.FIXED_UPDATE_FPS == 0 ? 0 : (1000 / (float)Config.FIXED_UPDATE_FPS));
            Time.fixedUpdateRate = TimeSpan.FromTicks((long)TimeSpan.TicksPerSecond / Config.FIXED_UPDATE_FPS);
            Time.fixedUpdateMult = (float)DEFAULT_FIXED_UPDATE_RATE / Config.FIXED_UPDATE_FPS;
        }

        private int fixedUpdateRate;
        private float fixedUpdateElapsedTime = 0;
        private const float fixedUpdateDelta = 1.0f/60.0f;
        private float fixedDeltaTime = fixedUpdateDelta;
        private float previousT = 0;
        private float accumulator = 0.0f;
        private float maxFrameTime = 10;
        public void Update(GameTime gameTime) {
            Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            accumulator += Time.deltaTime;

            _objHandler.Update();
            _inputHandler.Update();
            Time.Update(ref gameTime);


            while(accumulator >= fixedUpdateDelta) {
                FixedUpdate();
                Time.FixedUpdate(ref gameTime);
                accumulator -= fixedDeltaTime;
            }

            Time.fixedUpdateAlpha = (float)(accumulator / fixedUpdateDelta);
        }

        public void Draw(SpriteBatch spriteBatch) {
         
            // TODO: Add your drawing code here
        }

        private void FixedUpdate() {
            _objHandler.FixedUpdate();
        }
    }
}
