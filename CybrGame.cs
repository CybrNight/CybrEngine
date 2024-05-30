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
    public abstract class CybrGame : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private ObjectAllocator objHandler;
        private TickHandler tickHandler;
        private ParticleHandler particleHandler;

        protected abstract bool LoadGameContent();
        protected abstract bool GameInit();
        protected abstract bool GameStart();

        protected abstract void GameUpdate();
        protected abstract void GameDraw(SpriteBatch spriteBatch);
        protected abstract void GameStop();

        private bool GameRunning { get; set; } = false;
        private bool ContentLoaded { get; set; } = false;

        public CybrGame() {
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
            tickHandler = Autoload.tickHandler;
            particleHandler = Autoload.particleHandler;

            //Initialize singleton handlers


            IsMouseVisible = true;
        }

        public Entity Instantiate(Entity instance){
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

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var _blankTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            _blankTexture.SetData(new[] { Color.White });

            Assets.AddTexture("missing_tex", new Texture2D(GraphicsDevice, 32, 32));
            Assets.AddTexture("blank", _blankTexture);

            //After core content loaded, tell game to load unique assets
            if(LoadGameContent()) {
                ContentLoaded = GameInit();
                if (ContentLoaded){
                    GameRunning = GameStart();
                    base.LoadContent();
                }
            }


           
        }

        protected override void Update(GameTime gameTime) {
            Time.gameTime = gameTime;
            if(GameRunning) {
                tickHandler.Update(gameTime);
            }
            base.Update(gameTime);
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

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
            GraphicsDevice.Clear(Config.BACKGROUND_COLOR);

            particleHandler.Draw(spriteBatch);
            objHandler.Draw(spriteBatch);

            GameDraw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
