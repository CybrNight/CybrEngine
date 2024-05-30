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
    public class Engine : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private ObjectAllocator objHandler;
        private InputHandler inputHandler;

        private ParticleHandler particleHandler;

        private bool GameRunning { get; set; } = false;
        private bool ContentLoaded { get; set; } = false;

        private CybrGame game;
        private readonly int DEFAULT_FIXED_UPDATE_RATE = Config.FIXED_UPDATE_FPS;

        public Engine(CybrGame game) {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            IsFixedTimeStep = false;

            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            Window.IsBorderless = false;
            graphics.IsFullScreen = true;
            graphics.HardwareModeSwitch = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            Assets.Content = Content;
            Assets.GraphicsDevice = GraphicsDevice;

            objHandler = Autoload.objAllocator;
            particleHandler = Autoload.particleHandler;
            inputHandler = Autoload.inputHandler;

            //Initialize singleton handlers

            this.game = game;
            game.graphics = graphics;
            game.spriteBatch = spriteBatch;
            IsMouseVisible = true;
        }

        public Entity Instantiate(Entity instance) {
            return objHandler.AddInstance(instance);
        }

        public T Instantiate<T>(float x, float y) where T : Entity {
            return objHandler.Instantiate<T>(new Vector2(x, y));
        }

        public T Instantiate<T>(Vector2 position) where T : Entity {
            return objHandler.Instantiate<T>(position);
        }


        public T Instantiate<T>() where T : Entity {
            return objHandler.Instantiate<T>(new Vector2());
        }

        protected sealed override void Initialize() {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = Config.WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = Config.WINDOW_HEIGHT;
            graphics.ApplyChanges();

            fixedUpdateRate = (int)(Config.FIXED_UPDATE_FPS == 0 ? 0 : (1000 / (float)Config.FIXED_UPDATE_FPS));
            Time.fixedUpdateRate = TimeSpan.FromTicks((long)TimeSpan.TicksPerSecond / Config.FIXED_UPDATE_FPS);
            Time.fixedUpdateMult = (float)DEFAULT_FIXED_UPDATE_RATE / Config.FIXED_UPDATE_FPS;

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var _blankTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _blankTexture.SetData(new[] { Color.White });

            Assets.AddTexture("missing_tex", new Texture2D(GraphicsDevice, 32, 32));
            Assets.AddTexture("blank", _blankTexture);

            //After core content loaded, tell game to load unique assets
            if(game.LoadGameContent()) {
                ContentLoaded = game.GameInit();
            }



        }

        float timer = 0.0f;
        private int fixedUpdateRate;
        private float fixedUpdateElapsedTime = 0;
        private const float fixedUpdateDelta = 1.0f / 60.0f;
        private float fixedDeltaTime = fixedUpdateDelta;
        private float previousT = 0;
        private float accumulator = 0.0f;
        private float maxFrameTime = 10;
        protected override void Update(GameTime gameTime) {
            Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Time.gameTime = gameTime;
            if(ContentLoaded) {
                timer += Time.deltaTime;
                if(timer > 1f)
                    GameRunning = game.GameStart();
                base.LoadContent();
                if(GameRunning) {
                    Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                    accumulator += Time.deltaTime;

                    objHandler.Update();
                    inputHandler.Update();
                    Time.Update(ref gameTime);


                    while(accumulator >= fixedUpdateDelta) {
                        FixedUpdate();
                        Time.FixedUpdate(ref gameTime);
                        accumulator -= fixedDeltaTime;
                    }

                    Time.fixedUpdateAlpha = (float)(accumulator / fixedUpdateDelta);
                }
            }

            base.Update(gameTime);
        }

        private void FixedUpdate(){
            objHandler.FixedUpdate();
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
            if(!ContentLoaded) return;
            GraphicsDevice.Clear(Config.BACKGROUND_COLOR);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            particleHandler.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
            objHandler.Draw(spriteBatch);

            game.DebugDraw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}
