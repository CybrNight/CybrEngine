using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Security.AccessControl;

namespace CybrEngine {
    /// <summary>
    /// Class defining custom Game type
    /// Allows for different Game to be swapped out
    /// </summary>
    public abstract class CybrGame : IDisposable {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        private ObjectAllocator objAlloc;

        public abstract bool LoadGameContent();
        public abstract bool GameInit();
        public abstract bool GameStart();

        public abstract void GameUpdate();
        public abstract void DebugDraw(SpriteBatch spriteBatch);
        public abstract void GameStop();

        private bool GameRunning { get; set; } = false;
        private bool ContentLoaded { get; set; } = false;

        public CybrGame() {
            objAlloc = Autoload.objAllocator;
        }

        public Entity Instantiate(Entity instance) {
            return objAlloc.AddInstance(instance);
        }

        public T Instantiate<T>(float x, float y) where T : Entity {
            return objAlloc.Instantiate<T>(new Vector2(x, y));
        }

        public T Instantiate<T>(Vector2 position) where T : Entity {
            return objAlloc.Instantiate<T>(position);
        }


        public T Instantiate<T>() where T : Entity {
            return objAlloc.Instantiate<T>(new Vector2());
        }


        protected void LoadContent(GraphicsDevice graphicsDevice) {

        }

        public void BeginRun() {

        }

        public void EndDraw() {

        }

        public void BeginDraw() {

        }

        //public void Draw(GameTime gameTime) {
        //    if(!ContentLoaded) return;
        //    graphics.GraphicsDevice.Clear(Config.BACKGROUND_COLOR);

        //    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
        //    particleHandler.Draw(spriteBatch);
        //    spriteBatch.End();

        //    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
        //    objHandler.Draw(spriteBatch);

        //    DebugDraw(spriteBatch);

        //    spriteBatch.End();
        //}


        //float timer = 0.0f;
        //public void Update(GameTime gameTime) {
        //    Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        //    Time.gameTime = gameTime;
        //    if(ContentLoaded) {
        //        timer += Time.deltaTime;
        //        if(timer > 1f)
        //            GameRunning = GameStart();
        //        if(GameRunning) {
        //            tickHandler.Update(gameTime);
        //        }
        //    }
        //}

        public void Dispose() {

        }
    }
}
