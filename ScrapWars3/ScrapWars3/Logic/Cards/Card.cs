using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Data;

namespace ScrapWars3.Logic.Cards
{
    abstract class Card
    {
        private int lastTurnUsed;        

        public virtual void ApplyToMechs(Mech[] mechs, int lastTurnUsed)
        {
            this.lastTurnUsed = lastTurnUsed;
        }

        public abstract void UnapplyToMechs(Mech[] mechs);

        public int LastTurnUsed
        {
            get { return lastTurnUsed; }            
        }
    }
}
