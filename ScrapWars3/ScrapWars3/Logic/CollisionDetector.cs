using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Logic
{
    class CollisionDetector
    {
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
                mainObject = objectOne;
                secondObject = objectTwo;
            }

            for(int x = 0; x < mainObject.ColorArray.GetLength(0); x++)
            {
                for(int y = 0; y < mainObject.ColorArray.GetLength(0); y++)
                {                    
                    Vector2 colorLocation = mainObject.GetTransformedScreenLocation(x,y);
                    Color colorOne = mainObject.ColorArray[x,y];
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
