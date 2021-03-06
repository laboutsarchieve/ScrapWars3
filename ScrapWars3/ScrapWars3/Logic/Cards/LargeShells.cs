﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Data;

namespace ScrapWars3.Logic.Cards
{
    class LargeShells : Card
    {
        public LargeShells()
            : base("Large Shells")
        {
        }
        public override void ApplyToMechs(Mech[] mechs, int lastTurnUsed)
        {
            foreach(Mech mech in mechs)
            {
                mech.SaveAsCurrentState( );
                mech.MainGun.Damage *= 2;
                mech.MainGun.BulletScale *= 2;
                mech.MainGun.BulletSpeed *= 0.5f;
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
