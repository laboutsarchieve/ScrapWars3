using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Data
{
    class Mech
    {
        string name;
        MechType mechType;
        int mechId;

        Vector2 location;

        public Mech(string name, int mechId, MechType mechType)
        {
            this.name = name;
            this.mechType = mechType;
            this.mechId = mechId;
        }
        public string Name
        {
            get { return name; }
        }
        internal MechType MechType
        {
            get { return mechType; }
        }
        public int MechId
        {
            get { return mechId; }
        }

        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
