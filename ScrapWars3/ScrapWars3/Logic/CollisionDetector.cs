using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScrapWars3.Data;
using ScrapWars3.Resources;

namespace ScrapWars3.Logic
{
    // TODO: Unit Testing for Collision Detector
    class CollisionDetector
    {
        public static bool IsMechOnTile(Mech mech, Map map, Tile tileType)
        {
            Rectangle mechBox = mech.BoundingRect;
            Rectangle scaledBox = new Rectangle(mechBox.X / GameSettings.TileSize,
                                                mechBox.Y / GameSettings.TileSize,
                                                mechBox.Width / GameSettings.TileSize,
                                                mechBox.Height / GameSettings.TileSize);
            if(map.ContainsTileType(scaledBox, tileType))
            {
                return true;
            }

            return false;
        }
        internal static CollisionReport DetectCollision(Bullet bullet, Mech mech)
        {
            CollisionObject bulletObject = new CollisionObject(GameTextureRepo.GetBulletTexture(bullet.BulletType), bullet.Location, 0.0f);
            CollisionObject mechObject = new CollisionObject(GameTextureRepo.GetMechTexture(mech.MechType), mech.Location - mech.Size/2.0f, mech.FacingAngle + mech.ImageFacingOffset);

            return DetectCollision(bulletObject, mechObject);
        }
        public static CollisionReport DetectCollision(CollisionObject objectOne, CollisionObject objectTwo)
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

            for(int x = 0; x < mainObject.ColorArray.GetLength(0); x++)
            {
                for(int y = 0; y < mainObject.ColorArray.GetLength(0); y++)
                {
                    Vector2 colorLocation = mainObject.GetTransformedScreenLocation(x, y);
                    Color colorOne = mainObject.ColorArray[x, y];
                    Color colorTwo = secondObject.GetColorAtScreenLocation((int)colorLocation.X, (int)colorLocation.Y);

                    if(colorOne.A > 0 && colorTwo.A > 0)
                    {
                        report.RecordCollision(colorLocation);
                        break;
                    }
                }

                if(report.CollisionOccured)
                    break;
            }

            return report;
        }        
    }
}
