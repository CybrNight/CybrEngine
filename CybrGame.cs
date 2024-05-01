using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace CybrEngine
{
    /// <summary>
    /// Class defining custom Game type
    /// Allows for different Game to be swapped out
    /// </summary>
    public abstract class CybrGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private ObjectHandler objHandler;
        private InputHandler inputHandler;
        private Messager messager;

        private int fixedUpdateRate;


        protected abstract bool LoadGameContent();
        protected abstract bool GameInit();
        protected abstract bool GameStart();

        protected abstract void GameUpdate();
        protected abstract void GameDraw();
        protected abstract void GameStop();

        private readonly int DEFAULT_FIXED_UPDATE_RATE = Config.FIXED_UPDATE_FPS;

        private bool GameRunning {  get; set; }

        public CybrGame() : base(){ 
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            Assets.Content = Content;


            //Initialize singleton handlers
            messager = Messager.Instance;
            inputHandler = InputHandler.Instance;
            objHandler = ObjectHandler.Instance;

            IsMouseVisible = true;
        }

        public Object Instantiate<T>() where T : Entity {
            return ObjectHandler.Instance.Instantiate<T>();
        }

        protected sealed override void Initialize()
        {
            // TODO: Add your initialization logic here
            fixedUpdateRate = (int)(Config.FIXED_UPDATE_FPS == 0 ? 0 : (1000 / (float)Config.FIXED_UPDATE_FPS));
            Time.fixedUpdateRate = TimeSpan.FromTicks((long)TimeSpan.TicksPerSecond / Config.FIXED_UPDATE_FPS);
            Time.fixedUpdateMult = (float)DEFAULT_FIXED_UPDATE_RATE / Config.FIXED_UPDATE_FPS;

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = Config.RES_X;
            graphics.PreferredBackBufferHeight = Config.RES_Y;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //After core content loaded, tell game to load unique assets
            if(LoadGameContent()){

                if(GameInit()) { //If content loaded, tell game to init
                    GameRunning = GameStart();
                }
            } 
            
            if (!GameRunning){
                Exit();
            }
        }


        private float fixedUpdateElapsedTime = 0;
        private float fixedUpdateDelta = 0.33f;
        private float previousT = 0;
        private float accumulator = 0.0f;
        private float maxFrameTime = 250; 

        protected override void Update(GameTime gameTime)
        {

            if(gameTime.ElapsedGameTime.TotalSeconds > 0.1) {
                accumulator = 0;
                previousT = 0;
                return;
            }

            if(previousT == 0) {
                fixedUpdateDelta = fixedUpdateRate;
                previousT = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            Time.Update(ref gameTime);

            float now = (float)gameTime.TotalGameTime.TotalMilliseconds;
            Time.frameTime = now - previousT;

            if(Time.frameTime > maxFrameTime) {
                Time.frameTime = maxFrameTime;
            }
            previousT = now;

            accumulator += Time.frameTime;

            if(GameRunning) {
                objHandler.Update();
                inputHandler.Update();
                GameUpdate();

                while (accumulator >= fixedUpdateDelta){
                    objHandler.FixedUpdate();
                    Time.FixedUpdate(ref gameTime);
                    fixedUpdateElapsedTime += fixedUpdateDelta;
                    accumulator -= fixedUpdateDelta;
                }
            }

            Time.fixedUpdateAlpha = (float)(accumulator / fixedUpdateDelta);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime){
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (GameRunning){
                objHandler.Draw(spriteBatch);
                GameDraw();
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
