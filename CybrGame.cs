using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CybrEngine
{
    public abstract class CybrGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private ObjectHandler handler;

        private int fixedUpdateRate;


        protected abstract bool LoadGameContent();
        protected abstract bool GameInit();
        protected abstract bool GameStart();

        protected abstract void GameUpdate();
        protected abstract void GameDraw();
        protected abstract void GameStop();

        private bool GameRunning {  get; set; }

        public CybrGame() : base(){ 
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            Assets.Content = Content;
            handler = ObjectHandler.Instance;

            IsMouseVisible = true;
        }

        public Object Instantiate<T>() where T : Object {
            return handler.Instantiate<T>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            fixedUpdateRate = (int)(60 == 0 ? 0 : (1000 / (float)60));
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
            // TODO: use this.Content to load your game content here
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
                previousT = (float)gameTime.TotalGameTime.TotalMilliseconds;
            }

            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Globals.ElapsedTime = elapsedTime;
            Globals.GameTime = gameTime;
            float now = (float)gameTime.TotalGameTime.TotalMilliseconds;
            float frameTime = now - previousT;
            if(frameTime > maxFrameTime)
                frameTime = maxFrameTime;
            previousT = now;

            accumulator += frameTime;

            if(GameRunning)
                GameUpdate();
                handler.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime){

            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            if (GameRunning){
                GameDraw();
                handler.Draw(spriteBatch);
            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
