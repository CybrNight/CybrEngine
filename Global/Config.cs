using Microsoft.Xna.Framework;
using System;

namespace CybrEngine {
    public class Config {
        public static int RES_X { get; set; } = 640;
        public static int RES_Y { get; set; } = 480;

        public static Color BACKGROUND_COLOR { get; set; } = Color.Black;

        public static float CHARACTER_SPEED { get; set; } = 80f;
        public static int GRID { get; set; } = 16;
        public static float HORIZONTAL_FRICTION { get; set; } = 0.4f;
        public static float VERTICAL_FRICTION { get; set; } = 0.8f;
        public static float BUMP_FRICTION { get; set; } = 0.7f;
        public static bool GRAVITY_ON { get; set; } = true;
        public static float GRAVITY_FORCE { get; set; } = 8f;
        public static float JUMP_FORCE { get; set; } = 3f;
        public static float GRAVITY_T_MULTIPLIER { get; set; } = 0.4f;

        public static float DYNAMIC_COLLISION_CHECK_FREQUENCY { get; set; } = 1f;

        public static bool INCREASING_GRAVITY { get; set; } = true;

        public static float SPRITE_DRAW_OFFSET { get; set; } = 0f;
        public static float SPRITE_COLLISION_OFFSET { get; set; } = 0f;

        public static float TIME_OFFSE{ get; set; } = 0.1f;
        public static int FIXED_UPDATE_FPS { get; set; } = 60;
        public static int PIVOT_RADIUS { get; set; } = 10;

        public static int CAMERA_TIME_MULTIPLIER { get; set; } = 1; // set this and CAMERA_FOLLOW_DELAY to a higher value to create a "wabbly" camera
        public static float CAMERA_DEADZONE { get; set; } = 100;
        public static float CAMERA_FRICTION { get; set; } = 0.89f;
        public static float CAMERA_FOLLOW_DELAY { get; set; } = 0.0005f;
        public static float CAMERA_ZOOM { get; set; } = 1;

        internal static float SCALE = 1;

        public static Action ExitAction;
    }
}
