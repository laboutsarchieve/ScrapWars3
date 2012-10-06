using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Data;

namespace ScrapWars3.Logic.Cards
{
    class OverDrive : Card
    {
        public override void ApplyToMechs(Mech[] mechs, int lastTurnUsed)
        {
            foreach(Mech mech in mechs)
            {
                mech.SaveAsCurrentState( );
                mech.MainGun.Damage = (int)(mech.MainGun.Damage*1.5f);
                mech.MainGun.BulletSpeed *= 1.5f;
                mech.MaxSpeed = (int)(mech.MaxSpeed*1.5f);
                mech.Damage(mech.MainGun.Damage);                
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
