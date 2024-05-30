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

        public static float Abs(float value){
            return Math.Abs(value);
        }

        public static float Sqrt(float value){
            return (float)Math.Sqrt((double)value);
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
}
