using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTools.Events;
using GameTools;

namespace ScrapWars3.Data.Event
{
    class BulletFiredEvent : BaseGameEvent
    {
        private Bullet bullet;        
        public BulletFiredEvent(Bullet bullet) : base("BulletFired", 0 )
        {
            this.bullet = bullet;
        }
        internal Bullet Bullet
        {
            get { return bullet; }            
        }
    }
}
