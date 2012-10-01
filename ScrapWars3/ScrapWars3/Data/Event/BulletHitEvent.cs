using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTools.Events;
using ScrapWars3.Logic;

namespace ScrapWars3.Data.Event
{
    class BulletHitEvent : BaseGameEvent
    {
        private CollisionReport collisionReport;        

        public BulletHitEvent(CollisionReport collisionReport) : base("BulletHit", 0)
        {
            this.collisionReport = collisionReport;
        }

        internal CollisionReport CollisionReport
        {
            get { return collisionReport; }            
        }
    }
}
