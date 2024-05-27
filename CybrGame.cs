using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CybrEngine {
    /// <summary>
    /// Class defining custom Game type
    /// Allows for different Game to be swapped out
    /// </summary>
    public abstract class CybrGame : Game {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private TickHandler handler;
        private ObjectHandler objHandler;
        private InputHandler inputHandler;

        protected abstract bool LoadGameContent();
        protected abstract bool GameInit();
        protected abstract bool GameStart();

        protected abstract void GameUpdate();
        protected abstract void GameDraw();
        protected abstract void GameStop();

        private bool GameRunning { get; set; } = false;
        private bool ContentLoaded { get; set; } = false;

        public CybrGame() {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            Assets.Content = Content;
            Assets.GraphicsDevice = GraphicsDevice;


            //Initialize singleton handlers


            IsMouseVisible = true;
        }

        public GameObject Instantiate(GameObject instance){
            return objHandler.AddInstance(instance);
        }

        public T Instantiate<T>(float x, float y) where T : GameObject {
            return objHandler.Instantiate<T>(new Vector2(x, y));
        }

        public T Instantiate<T>(Vector2 position) where T : GameObject {
            return objHandler.Instantiate<T>(position);
        }


        public T Instantiate<T>() where T : GameObject {
            return objHandler.Instantiate<T>(new Vector2());
        }

        protected sealed override void Initialize() {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = Config.WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = Config.WINDOW_HEIGHT;
            graphics.ApplyChanges();

            handler = TickHandler.Instance;
            inputHandler = InputHandler.Instance;
            objHandler = ObjectHandler.Instance;

            base.Initialize();
        }

        protected override void LoadContent() {
            Assets.AddTexture("missing_tex", new Texture2D(GraphicsDevice, 32, 32));
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //After core content loaded, tell game to load unique assets
            if(LoadGameContent()) {
                if (GameInit()){
                    ContentLoaded = GameStart();
                    base.LoadContent();
                }
            }


           
        }

        protected override void Update(GameTime gameTime) {
            if(GameRunning) {
                handler.Update(gameTime);
            }else if (ContentLoaded){
                GameRunning = true;
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
            if(!GameRunning) return;

            spriteBatch.Begin();
            GraphicsDevice.Clear(Config.BACKGROUND_COLOR);
            handler.Draw(spriteBatch);
            GameDraw();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
