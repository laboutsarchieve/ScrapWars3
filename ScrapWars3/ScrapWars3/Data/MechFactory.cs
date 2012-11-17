using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Data
{
    static class MechFactory
    {
        static int id = 0;

        public static Mech GetBaseMechFromType(MechType type)
        {   
            Mech mech;
            switch(type)
            {
                case MechType.DebugMechA:
                    {
                        mech = new Mech("DebugMechA", GetNextId(), 20, 4*GameSettings.TileSize, type);
                        mech.MainGun = new Gun(5, 6*GameSettings.TileSize, 20*GameSettings.TileSize, 1, 1.25f, BulletType.Basic);
                        break;
                    }
                case MechType.DebugMechB:
                    {
                        mech = new Mech("DebugMechB", GetNextId(), 40, 3*GameSettings.TileSize, type);
                        mech.MainGun = new Gun(10, 4*GameSettings.TileSize, 10*GameSettings.TileSize, 0.5f, 2, BulletType.Basic);
                        break;
                    }
                case MechType.DebugMechC:
                    {
                        mech = new Mech("DebugMechC", GetNextId(), 10, 5*GameSettings.TileSize, type);
                        mech.MainGun = new Gun(1, 8*GameSettings.TileSize,30*GameSettings.TileSize, 1.5f, 0.75f, BulletType.Basic);
                        break;
                    }
                default:
                    { 
                        mech = new Mech("Error", -1, 0, 0, MechType.Error);
                        break;
                    }
            }

            mech.SaveAsDefaultState( );
            return mech;
        }        
        private static int GetNextId()
        {
            id++;
            return id;
        }
    }
}
