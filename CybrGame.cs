using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace CybrEngine {
    /// <summary>
    /// Class defining custom Game type
    /// Allows for different Game to be swapped out
    /// </summary>
    public abstract class CybrGame : Game, IDisposable {
        public SpriteBatch spriteBatch;

        private EntityAllocator entityAllocator;

        public abstract bool LoadGameContent();
        public abstract bool GameInit();
        public abstract bool GameStart();
        public abstract void GameUpdate();
        public abstract void GameStop();

        public abstract void DebugDraw(SpriteBatch spriteBatch);

        private bool GameRunning { get; set; } = false;
        private bool ContentLoaded { get; set; } = false;
        private bool GameInitialized { get; set; } = false;
        private bool GameStopping { get; set; } = false;

        private GraphicsDeviceManager Graphics;
        private GameWindow GameWindow;


        private readonly int DEFAULT_FIXED_UPDATE_RATE = Config.FIXED_UPDATE_FPS;

        public CybrGame() {
            //Initialize graphics device, setup content directory
            Graphics = new GraphicsDeviceManager(this);

            IsMouseVisible = true;
            IsFixedTimeStep = false;

            //Setup Window
            Graphics.PreferredBackBufferWidth = Config.WINDOW_WIDTH;
            Graphics.PreferredBackBufferHeight = Config.WINDOW_HEIGHT;
            Window.IsBorderless = false;
            Graphics.IsFullScreen = false;
            Graphics.HardwareModeSwitch = true;
            Graphics.SynchronizeWithVerticalRetrace = true;
            Graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            Assets.Content = Content;
            Assets.GraphicsDevice = GraphicsDevice;

            GameWindow = Window;

            fixedUpdateRate = (int)(Config.FIXED_UPDATE_FPS == 0 ? 0 : (1000 / (float)Config.FIXED_UPDATE_FPS));
            Time.fixedUpdateRate = TimeSpan.FromTicks((long)TimeSpan.TicksPerSecond / Config.FIXED_UPDATE_FPS);
            Time.fixedUpdateMult = (float) DEFAULT_FIXED_UPDATE_RATE / Config.FIXED_UPDATE_FPS;

        }


        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var _blankTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _blankTexture.SetData(new[] { Color.White });

            Assets.AddTexture("missing_tex", new Texture2D(GraphicsDevice, 32, 32));
            Assets.AddTexture("blank", _blankTexture);

            IsMouseVisible = true;

            SignalBus.Emit("content-loaded");
        }

        protected override void BeginRun() {
            base.BeginRun();
        }

        protected override void EndDraw() {
            base.EndDraw();
        }

        protected override bool BeginDraw() {
            return base.BeginDraw();
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Gray);

            if(GameInitialized) {
                GraphicsDevice.Clear(Config.BACKGROUND_COLOR);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                entityAllocator.Draw(spriteBatch);
                ComponentAllocator.Instance.Draw(spriteBatch);

                DebugDraw(spriteBatch);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }


        float timer = 0.0f;
        private int fixedUpdateRate;
        private float fixedUpdateElapsedTime = 0;
        private const float fixedUpdateDelta = 1.0f / 60.0f;
        private float fixedDeltaTime = fixedUpdateDelta;
        private float previousT = 0;
        private float accumulator = 0.0f;
        private float maxFrameTime = 10;
        private const float FixedTimeStep = 1f / 60f; // 60 updates per second
        private float _accumulatedTime;
        private TimeSpan _previousGameTime;

        protected override void Update(GameTime gameTime) {
            Time.gameTime = gameTime;
            if(GameInitialized) {
                Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                GameUpdate();
                Autoload.InputHandler.Update();

                GameRunning = true;
                // Calculate elapsed time since last frame
                TimeSpan currentGameTime = gameTime.TotalGameTime;
                float elapsedTime = (float)(currentGameTime - _previousGameTime).TotalSeconds * (Time.timeScale);
                _previousGameTime = currentGameTime;

                // Accumulate elapsed time
                _accumulatedTime += elapsedTime;

                if(elapsedTime > 0) {
                    Autoload.ComponentAllocator.Update();
                    Autoload.SceneManager.Update();
                }

                // Fixed update loop
                while(_accumulatedTime >= FixedTimeStep) {
                    FixedUpdate();
                    _accumulatedTime -= FixedTimeStep;
                }

            }
            base.Update(gameTime);
        }


        private void FixedUpdate() {
            Autoload.SceneManager.FixedUpdate();
        }

        private void OnContentLoaded(object[] args) {
            ContentLoaded = LoadGameContent();
            if(ContentLoaded) {
                SignalBus.Emit("game-content-loaded");
            }
        }

        private void OnGameContentLoaded(object[] args){
            GameInitialized = GameInit();
            if (GameInitialized){
                SignalBus.Emit("game-initialized");
            }
        }

        private void OnGameInitialized(object[] args){
            GameStart();    
        }


        protected sealed override void Initialize(){
            entityAllocator = Autoload.EntityAllocator;

            // Connect game start signals
            SignalBus.Connect("content-loaded", OnContentLoaded);
            SignalBus.Connect("game-content-loaded", OnGameContentLoaded);
            SignalBus.Connect("game-initialized", OnGameInitialized);

            base.Initialize();
        }

        public Entity Instantiate(Entity instance) {
            return entityAllocator.AddInstance(instance);
        }

        public Entity Instantiate<T>(float x, float y) where T : Entity {
            return entityAllocator.Instantiate<T>(new Vector2(x, y));
        }

        public Entity Instantiate<T>(Vector2 position) where T : Entity {
            return entityAllocator.Instantiate<T>(position);
        }


        public Entity Instantiate<T>() where T : Entity {
            return entityAllocator.Instantiate<T>(new Vector2());
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

    }
}
