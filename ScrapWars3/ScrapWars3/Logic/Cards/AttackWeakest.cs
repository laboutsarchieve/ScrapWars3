using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Data;
using ScrapWars3.Logic.Behaviors;

namespace ScrapWars3.Logic.Cards
{
    class AttackWeakest : Card
    {
        public override void ApplyToMechs(Mech[] mechs, int lastTurnUsed)
        {
            foreach(Mech mech in mechs)
            {
                mech.Brain.AttackBehavior = (BehaviorState)new AttackWeakest( );
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
