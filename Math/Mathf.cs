using Microsoft.Xna.Framework;
using System;

namespace CybrEngine {

    public static class Mathf {

        /// <summary>
        /// Returns Cos of angle in degrees
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float Cos(float degrees) {
            return (float)Math.Cos(degrees.Radians());
        }

        /// <summary>
        /// Returns Sin of angle in degrees
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float Sin(float degrees) {
            return (float)Math.Sin(degrees.Radians());
        }

        /// <summary>
        /// Returns Sign of value in degrees
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Sign(float value) {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns Floor of float value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int FloorToInt(float value) {
            return (int)Math.Floor(value);
        }

        /// <summary>
        /// Returns Ceil of float value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int CeilToInt(float value) {
            return (int)Math.Ceiling(value);
        }

        /// <summary>
        /// Converts boolcean value to int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this bool value) {
            return value ? 1 : 0;
        }

        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float Radians(this float degrees) {
            return MathHelper.ToRadians(degrees);
        }

        /// <summary>
        /// Convert radians to degrees
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static float Degrees(this float radians) {
            return MathHelper.ToDegrees(radians);

        }

        /// <summary>
        /// Converts int value to clamp degree value
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float Angle(this int angle){
            return (angle % 360);
        }

        public static Vector2 ToVector2(this Point point) {
            return new Vector2(point.X, point.Y);
        }
    }
}
