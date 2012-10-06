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
        private Vector2 collisionPosition;

        public CollisionReport(CollisionObject objectOne, CollisionObject objectTwo)
        {
            collisionOccured = false;
            collisionPosition = -Vector2.One;
        }
        public void RecordCollision(Vector2 position)
        {
            collisionOccured = true;
            this.collisionPosition = position;
        }
        public bool CollisionOccured
        {
            get { return collisionOccured; }
        }
        public Vector2 CollisionPosition
        {
            get { return collisionPosition; }
        }
    }
}
