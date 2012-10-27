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
        protected string name;

        protected Card(string name)
        {
            this.name = name;
        }

        public virtual void ApplyToMechs(Mech[] mechs, int lastTurnUsed)
        {
            this.lastTurnUsed = lastTurnUsed;
        }

        public abstract void UnapplyToMechs(Mech[] mechs);

        public string GetName()
        {
            return name;
        }
        public int LastTurnUsed
        {
            get { return lastTurnUsed; }            
        }
    }
}
