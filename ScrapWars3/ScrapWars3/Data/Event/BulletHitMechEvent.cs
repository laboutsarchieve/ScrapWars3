using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTools.Events;
using ScrapWars3.Logic;

namespace ScrapWars3.Data.Event
{
    class BulletHitMechEvent : BaseGameEvent
    {
        private Mech mech;
        private Bullet bullet;
        
        public BulletHitMechEvent(Mech mech, Bullet bullet)
            : base("BulletHitMech", 0)
        {
            this.mech = mech;
            this.bullet = bullet;
        }

        internal Bullet Bullet
        {
            get { return bullet; }            
        }
        internal Mech Mech
        {
            get { return mech; }            
        }
    }
}
