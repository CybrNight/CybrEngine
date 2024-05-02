using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Messager messager;

        protected abstract bool LoadGameContent();
        protected abstract bool GameInit();
        protected abstract bool GameStart();

        protected abstract void GameUpdate();
        protected abstract void GameDraw();
        protected abstract void GameStop();

        private bool GameRunning { get; set; }

        public CybrGame() : base() {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            Assets.Content = Content;


            //Initialize singleton handlers
           

            IsMouseVisible = true;
        }

        public Object Instantiate<T>() where T : Entity {
            return ObjectHandler.Instance.Instantiate<T>();
        }

        protected sealed override void Initialize() {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = Config.RES_X;
            graphics.PreferredBackBufferHeight = Config.RES_Y;
            graphics.ApplyChanges();

            handler = TickHandler.Instance;
            messager = Messager.Instance;
            inputHandler = InputHandler.Instance;
            objHandler = ObjectHandler.Instance;

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //After core content loaded, tell game to load unique assets
            if(LoadGameContent()) {

                if(GameInit()) { //If content loaded, tell game to init
                    GameRunning = GameStart();
                }
            }

            if(!GameRunning) {
                Exit();
            }
        }

        protected override void Update(GameTime gameTime) {
            if(GameRunning) {
                handler.Update(gameTime);                
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            if (GameRunning){
                GraphicsDevice.Clear(Config.BACKGROUND_COLOR);
                handler.Draw(spriteBatch);
                GameDraw();
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
