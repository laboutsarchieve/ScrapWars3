using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameTools.Events;

namespace ScrapWars3.Data.Event
{
    class MechDiedEvent : BaseGameEvent
    {
        private Mech mech;
        
        public MechDiedEvent(Mech mech)
            : base("MechDied", 0)
        {
            this.mech = mech;
        }

        internal Mech Mech
        {
            get { return mech; }            
        }
    }
}
