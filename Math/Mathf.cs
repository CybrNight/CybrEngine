using Microsoft.Xna.Framework;
using System;

namespace CybrEngine {
    /// <summary>
    /// Wrapper for System.Math, use floats instead of doubles
    /// </summary>
    public static class Mathf {

        /// <summary>
        /// Clamps value to range
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Clamp(float value, float min, float max) {
            return Math.Clamp(value, min, max);
        }

        public static int Clamp(int value, int min, int max){
            return Clamp(value, min, max);
        }

        /// <summary>
        /// Returns cos(anglee) in degrees
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float Cos(float degrees) {
            return (float)Math.Cos(degrees.Radians());
        }

        /// <summary>
        /// Returns sin(angle) in degrees
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float Sin(float degrees) {
            return (float)Math.Sin(degrees.Radians());
        }

        /// <summary>
        /// Returns sign(value)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Sign(float value) {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns returns Math.Floor as an int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int FloorToInt(float value) {
            return (int)Math.Floor(value);
        }

        /// <summary>
        /// Returns Math.Ceiling as an int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int CeilToInt(float value) {
            return (int)Math.Ceiling(value);
        }
    }

    public static class MathExtensions{
        public static float Clamp(this float value, float min, float max){
            return Math.Clamp(value, min, max);
        }

        public static int Clamp(this int value, int min, int max) {
            return Mathf.Clamp(value, min, max);
        }

        public static int Floor(this float value) {
            return Mathf.FloorToInt(value);
        }

        public static int Ceil(this float value) {
            return Mathf.CeilToInt(value);
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
        /// <param name="value"></param>
        /// <returns></returns>
        public static float ClampAngle(this float value) {
            return (value % 360);
        }

        public static int ClampAngle(this int value) {
            return (value % 360);
        }

        public static Vector2 ToVector2(this Point point) {
            return new Vector2(point.X, point.Y);
        }
    }
}
