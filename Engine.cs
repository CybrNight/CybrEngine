﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Security.AccessControl;
using System.Text.Json.Serialization;

namespace CybrEngine {
    /// <summary>
    /// Class defining custom Game type
    /// Allows for different Game to be swapped out
    /// </summary>
    public class Engine : Game {
        
        //Defines the lowest level of game to engine access
        public static class EntryPoint {
            private static Engine engine;
            static EntryPoint() {
                engine = new Engine();
            }

            public static void Load(CybrGame game) {
                engine.LoadGame(game);
            }

            public static void Run(){
                engine.Run();
            }

            public static void Update() {
                var kstate = Keyboard.GetState();
                if(kstate.IsKeyDown(Keys.Escape)) {
                    engine.StartGame();
                }
            }
        }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private ObjectAllocator objAlloc;
        private InputHandler inputHandler;

        private ParticleHandler particleHandler;

        private bool GameRunning { get; set; } = false;
        private bool ContentLoaded { get; set; } = false;
        private bool GameStopping  {  get; set; } = false;  

        private CybrGame game;
        private readonly int DEFAULT_FIXED_UPDATE_RATE = Config.FIXED_UPDATE_FPS;

        private Engine() {
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

            objAlloc = Autoload.objAllocator;
            particleHandler = Autoload.particleHandler;
            inputHandler = Autoload.inputHandler;

            //Initialize singleton handlers
        }

        public void StartGame(){
            game.GameInit();
            GameRunning = game.GameStart();
        }

        public void LoadGame(CybrGame game) {
            this.game = game;
            game.graphics = graphics;
            game.spriteBatch = spriteBatch;

            ContentLoaded = game.LoadContent();
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

            IsMouseVisible = true;
        }

        public void Stop() {
            GameRunning = false;
            GameStopping = true;
            Autoload.Reset();
            ContentLoaded = false;
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
            var kstate = Keyboard.GetState();
            Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Time.gameTime = gameTime;

            if(!GameRunning) {
                EntryPoint.Update();
                return;
            }
            
            if(ContentLoaded) {
                if(kstate.IsKeyDown(Keys.Enter)) {
                    Config.PAUSED = !Config.PAUSED;
                }
                if(kstate.IsKeyDown(Keys.Back)) {
                    Stop();
                }
                if(GameRunning) {
                    Time.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                    accumulator += Time.deltaTime;

                    if (!Config.PAUSED){
                        objAlloc.Update();
                        inputHandler.Update();
                        particleHandler.Update();
                    }
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

        private void FixedUpdate() {
            objAlloc.FixedUpdate();
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
            if(ContentLoaded){
                GraphicsDevice.Clear(Config.BACKGROUND_COLOR);

                if (GameRunning){
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
                    particleHandler.Draw(spriteBatch);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
                    objAlloc.Draw(spriteBatch);

                    game.DebugDraw(spriteBatch);
                    spriteBatch.End();
                }
            }

            base.Draw(gameTime);
        }
    }
}
