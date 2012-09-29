using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapWars3.Data
{
    static class MechFactory
    {
        static int id = 0;

        public static Mech GetBaseMechFromType(MechType type)
        {
            // TODO: Set the each mech's stats based on its type
            switch(type)
            {
                case MechType.DebugMechA:
                    return new Mech("DebugMechA", GetNextId(), type);
                case MechType.DebugMechB:
                    return new Mech("DebugMechB", GetNextId(), type);
                case MechType.DebugMechC:
                    return new Mech("DebugMechC", GetNextId(), type);
                default:
                    return new Mech("Error", -1, MechType.Error);
            }
        }
        private static int GetNextId()
        {
            id++;
            return id;
        }
    }
}
