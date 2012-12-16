using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScrapWars3.Data;
using ScrapWars3.Resources;

namespace ScrapWars3.Logic
{
    class CollisionDetector
    {
        public static bool IsMechOnTile(Mech mech, Map map, Tile tileType)
        {
            return ContainsTile(mech.Position,
                                mech.Size,
                                map,
                                tileType);
                                
        }
        public static bool ContainsTile(Vector2 topLeft, Vector2 bottomRight, Map map, Tile tileType)
        {   
            Rectangle scaledBox = new Rectangle((int)(topLeft.X / GameSettings.TileSize),
                                                (int)(topLeft.Y / GameSettings.TileSize),
                                                (int)Math.Round(bottomRight.X / GameSettings.TileSize),
                                                (int)Math.Round(bottomRight.Y / GameSettings.TileSize));

            if (map.ContainsTileType(scaledBox, tileType))
            {
                return true;
            }

            return false;
        }   
        // BUG: This doesn't work for large bullets
        internal static CollisionReport DetectBulletCollision(Bullet bullet, Mech mech, GameTime gameTime)
        {
            CollisionObject bulletObject = CollisionObject.FromBullet(bullet);
            CollisionObject mechObject = CollisionObject.FromMech(mech);

            return DetectCollision(bulletObject, mechObject, gameTime);
        }
        public static CollisionReport DetectCollision(CollisionObject objectOne, CollisionObject objectTwo, GameTime gameTime)
        {
            CollisionObject mainObject;
            CollisionObject secondObject;

            CollisionReport report = new CollisionReport(objectOne, objectTwo);

            if(objectOne.ColorArray.Length < objectTwo.ColorArray.Length) // This is to minimize the number of pixels examined
            {
                mainObject = objectOne;
                secondObject = objectTwo;
            }
            else
            {
                mainObject = objectTwo;
                secondObject = objectOne;
            }

            float stepSize = 0.001f;

            for(int stepNum = 0; stepNum < gameTime.ElapsedGameTime.Milliseconds / 1000.0f / stepSize; stepNum++)
            {
                for(int x = 0; x < mainObject.ColorArray.GetLength(0); x++)
                {
                    for(int y = 0; y < mainObject.ColorArray.GetLength(1); y++)
                    {
                        Vector2 colorScreenPosition = mainObject.GetTransformedScreenPosition(x, y);
                        Color colorOne = mainObject.ColorArray[x, y];
                        Color colorTwo = secondObject.GetColorAtScreenPosition((int)colorScreenPosition.X, (int)colorScreenPosition.Y);

                        if(colorOne.A > 0 && colorTwo.A > 0)
                        {
                            report.RecordCollision(colorScreenPosition);
                            return report;
                        }
                    }
                }

                mainObject.Step(stepSize);
            }

            return report;
        }
    }
}
