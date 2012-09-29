using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Logic
{
    class CollisionReport
    {
        private bool collisionOccured;
        private Vector2 collisionLocation;

        public CollisionReport(CollisionObject objectOne, CollisionObject objectTwo)
        {
            collisionOccured = false;
            collisionLocation = -Vector2.One;
        }
        public void RecordCollision(Vector2 location)
        {
            collisionOccured = true;
            this.collisionLocation = location;
        }
        public bool CollisionOccured
        {
            get { return collisionOccured; }
        }
        public Vector2 CollisionLocation
        {
            get { return collisionLocation; }
        }
    }
}
