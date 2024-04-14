using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CybrEngine
{
    public abstract class CybrGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private ObjectHandler handler;

        private int fixedUpdateRate;


        protected abstract bool LoadGameContent();
        protected abstract void GameInit();

        public CybrGame() : base(){ 
            graphics = new GraphicsDeviceManager(this);
            handler = ObjectHandler.Instance;

            Content.RootDirectory = "Content";

            Assets.Content = Content;

            IsMouseVisible = true;
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
            
            if(LoadGameContent())
               GameInit();
            else 
               Exit();
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

            handler.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            handler.Draw(spriteBatch);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        protected void Instantiate(Entity entity){
            handler.Instantiate(entity);
        }
    }
}
