using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Data;

namespace ScrapWars3.Logic.Cards
{
    class StayStill : Card
    {
        public override void ApplyToMechs(Mech[] mechs, int lastTurnUsed)
        {
            foreach(Mech mech in mechs)
            {
                mech.SaveAsCurrentState( );
                mech.MaxSpeed = 0;                
            }

            base.ApplyToMechs(mechs, lastTurnUsed);
        }

        public override void UnapplyToMechs(Mech[] mechs)
        {
            foreach(Mech mech in mechs)
            {
                mech.RestorePreviousState(false);
            }
        }
    }
}
