using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace CybrEngine {
    public static class Assets {

        public static class Primitives {
            private static Texture2D _square;
            private static Texture2D _circle;
            private static Texture2D _triangle;
            private static Texture2D _ring;

            public static Texture2D Square{
                get {
                    if (_square == null) {
                        _square = MakeSquare(32);
                    }
                    return _square;
                }
            }

            public static Texture2D Circle {
                get {
                    if(_circle == null) {
                        _circle = MakeCircle(32);
                    }
                    return _circle;
                }
            }

            public static Texture2D Triangle {
                get {
                    if(_triangle == null) {
                        _triangle = MakeTriangle(32, 32);
                    }
                    return _triangle;
                }
            }

            public static Texture2D Ring {
                get {
                    if(_ring == null) {
                        _ring = MakeRing(16, 8);
                    }
                    return _ring;
                }
            }

            public static Texture2D MakeStarWithCutout(int size, Color color) {
                int width = size * 2;
                int height = size * 2;

                Texture2D texture = new Texture2D(GraphicsDevice, width, height);
                Color[] data = new Color[width * height];

                // Calculate the center of the star
                Vector2 center = new Vector2(width / 2, height / 2);

                // Define the points of the outer star
                Vector2[] starOuterPoints = GenerateStarPoints(center, size, 5, 0.5f);

                // Define the points of the inner star
                Vector2[] starInnerPoints = GenerateStarPoints(center, size / 2, 5, 0.5f);

                // Draw the star with cutout
                for(int y = 0; y < height; y++) {
                    for(int x = 0; x < width; x++) {
                        Vector2 point = new Vector2(x, y);

                        // Check if the pixel is inside the outer star but outside the inner star
                        if(IsPointInPolygon(point, starOuterPoints) && !IsPointInPolygon(point, starInnerPoints)) {
                            data[y * width + x] = color;
                        } else {
                            data[y * width + x] = Color.Transparent;
                        }
                    }
                }

                texture.SetData(data);
                return texture;
            }

            // Function to generate star points
            private static Vector2[] GenerateStarPoints(Vector2 center, float size, int numPoints, float innerRadiusRatio) {
                Vector2[] points = new Vector2[numPoints * 2];

                float angleIncrement = MathHelper.TwoPi / numPoints;
                float currentAngle = -MathHelper.PiOver2; // Starting angle at the top
                float outerRadius = size;
                float innerRadius = size * innerRadiusRatio;

                for(int i = 0; i < numPoints * 2; i++) {
                    if(i % 2 == 0) // Outer point
                    {
                        points[i] = new Vector2((float)Math.Cos(currentAngle) * outerRadius + center.X, (float)Math.Sin(currentAngle) * outerRadius + center.Y);
                    } else // Inner point
                      {
                        points[i] = new Vector2((float)Math.Cos(currentAngle) * innerRadius + center.X, (float)Math.Sin(currentAngle) * innerRadius + center.Y);
                    }
                    currentAngle += angleIncrement;
                }

                return points;
            }

            public static Texture2D MakeHexagon(int size, Color color) {
                int width = size * 2;
                int height = (int)(size * Math.Sqrt(3));

                Texture2D texture = new Texture2D(GraphicsDevice, width, height);
                Color[] data = new Color[width * height];

                // Calculate the center of the hexagon
                Vector2 center = new Vector2(width / 2, height / 2);

                // Define the points of the hexagon
                Vector2[] points = new Vector2[6];
                for(int i = 0; i < 6; i++) {
                    float angle = MathHelper.ToRadians(60 * i);
                    points[i] = new Vector2((float)Math.Cos(angle) * size + center.X, (float)Math.Sin(angle) * size + center.Y);
                }

                // Draw the hexagon
                for(int y = 0; y < height; y++) {
                    for(int x = 0; x < width; x++) {
                        // Check if the pixel is within the hexagon boundaries
                        if(IsPointInPolygon(new Vector2(x, y), points)) {
                            // Set color for this pixel
                            data[y * width + x] = color;
                        } else {
                            // Set transparent for pixels outside the hexagon
                            data[y * width + x] = Color.Transparent;
                        }
                    }
                }

                texture.SetData(data);
                return texture;
            }

            // Function to check if a point is inside a polygon
            private static bool IsPointInPolygon(Vector2 point, Vector2[] polygon) {
                bool inside = false;
                int count = polygon.Length;
                int j = count - 1;
                for(int i = 0; i < count; i++) {
                    if(((polygon[i].Y < point.Y && polygon[j].Y >= point.Y) || (polygon[j].Y < point.Y && polygon[i].Y >= point.Y)) &&
                        (polygon[i].X <= point.X || polygon[j].X <= point.X)) {
                        inside ^= (polygon[i].X + (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < point.X);
                    }
                    j = i;
                }
                return inside;
            }


            public static Texture2D MakeRing(int outerRadius, int innerRadius) {
                int diameter = outerRadius * 2;
                Texture2D texture = new Texture2D(GraphicsDevice, diameter, diameter);
                Color[] colorData = new Color[diameter * diameter];

                for(int y = 0; y < diameter; y++) {
                    for(int x = 0; x < diameter; x++) {
                        int dx = x - outerRadius;
                        int dy = y - outerRadius;
                        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                        if(distance >= innerRadius && distance <= outerRadius) {
                            colorData[y * diameter + x] = Color.White;
                        } else {
                            colorData[y * diameter + x] = Color.Transparent;
                        }
                    }
                }

                texture.SetData(colorData);
                return texture;
            }


            public static Texture2D MakeSquare(int size = 32) {
                Texture2D texture = new Texture2D(GraphicsDevice, size, size);
                Color[] colorData = new Color[size * size];

                // Set all pixels to transparent initially
                for(int i = 0; i < colorData.Length; i++) {
                    colorData[i] = Color.Transparent;
                }

                // Draw the square
                for(int y = 0; y < size; y++) {
                    for(int x = 0; x < size; x++) {
                        colorData[y * size + x] = Color.White;
                    }
                }

                texture.SetData(colorData);
                return texture;
            }
            public static Texture2D MakeCircle(int diameter) {
                Texture2D texture = new Texture2D(GraphicsDevice, diameter, diameter);
                Color[] colorData = new Color[diameter * diameter];

                // Set all pixels to transparent initially
                for(int i = 0; i < colorData.Length; i++) {
                    colorData[i] = Color.Transparent;
                }

                // Draw the circle
                int radius = diameter / 2;
                int centerX = radius;
                int centerY = radius;
                for(int y = 0; y < diameter; y++) {
                    for(int x = 0; x < diameter; x++) {
                        int dx = centerX - x;
                        int dy = centerY - y;
                        if(dx * dx + dy * dy <= radius * radius) {
                            colorData[y * diameter + x] = Color.White;
                        }
                    }
                }
                texture.SetData(colorData);
                return texture;
            }

            public static Texture2D MakeTriangle(int width, int height) {
                Texture2D texture = new Texture2D(GraphicsDevice, width, height);
                Color[] colorData = new Color[width * height];


                // Set all pixels to transparent initially
                for(int i = 0; i < colorData.Length; i++) {
                    colorData[i] = Color.Transparent;
                }

                // Draw the isosceles triangle
                int centerX = width / 2;
                for(int y = 0; y < height; y++) {
                    int startX = centerX - (y * width / height / 2);
                    int endX = centerX + (y * width / height / 2);
                    for(int x = startX; x <= endX; x++) {
                        colorData[y * width + x] = Color.White;
                    }
                }

                texture.SetData(colorData);
                return texture;
            }
        }

        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, Entity> objects = new Dictionary<string, Entity>();
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static ContentManager Content;
        public static GraphicsDevice GraphicsDevice;

        public static Entity LoadObject(string path) {
            //If object already loaded, return copy instance
            if(objects.ContainsKey(path)) {
                return Object.Factory<Entity>.Instance(objects[path]);
            }

            var entity = Content.Load<Entity>("object/" + path);

            objects[path] = entity;
            return entity;
        }

        public static void AddTexture(string name, Texture2D texture) {
            textures[name] = texture;
        }

        public static SpriteFont LoadSpriteFont(string name, string path) {
            SpriteFont font = Content.Load<SpriteFont>(path);
            fonts.Add(name, font);
            return font;
        }

        public static SpriteFont GetSpriteFont(string name) {
            if(textures.ContainsKey(name)) {
                return fonts[name];
            }
            return null;
        }

        public static Texture2D LoadTexture(string name, string path) {
            Texture2D sprite = Content.Load<Texture2D>(path);
            textures.Add(name, sprite);
            return sprite;
        }

        public static void DisposeTexture(string name) {
            var sprite = textures[name];
            if(sprite != null) {
                textures.Remove(name);
                sprite.Dispose();
            }
        }

        public static Texture2D GetTexture(string name) {
            if(textures.ContainsKey(name)) {
                return textures[name];
            }
            return GetTexture("missing_tex");
        }
    }
}
