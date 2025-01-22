using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.Threading;

namespace CybrEngine {
    /// <summary>
    /// Class defining custom Game type
    /// Allows for different Game to be swapped out
    /// </summary>
    public class Engine {

        private bool GameInitialized { get; set; } = false;
        private bool GameRunning { get; set; } = false;
        private bool ContentLoaded { get; set; } = false;
        private bool GameStopping { get; set; } = false;

        private CybrGame _game;

        private readonly int DEFAULT_FIXED_UPDATE_RATE = Config.FIXED_UPDATE_FPS;

        private Thread thread;

        public Engine(CybrGame game) {
            Autoload.Construct();
            game.Run();
        }

        public void StartGame() {
            ContentLoaded = _game.LoadGameContent();

            if (ContentLoaded){
                SignalBus.Emit("content-loaded");
            }
        }

        public void Stop() {
            GameRunning = false;
            GameStopping = true;
            Autoload.Reset();
            ContentLoaded = false;
        }

        
    }
}
