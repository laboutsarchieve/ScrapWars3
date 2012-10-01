using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ScrapWars3.Data;

namespace ScrapWars3.Resources
{
    static class GameTextureRepo
    {
        private static int numLogos = 3;

        public static Texture2D[] teamLogos = new Texture2D[numLogos];

        public static Texture2D errorTexture;

        public static Texture2D debugMechA;
        public static Texture2D debugMechB;
        public static Texture2D debugMechC;

        public static Texture2D tileDirt;
        public static Texture2D tileGrass;
        public static Texture2D tileSand;
        public static Texture2D tileWater;

        public static Texture2D basicBullet;
        public static Texture2D pixel;

        public static Texture2D GetMechTexture(MechType mechType)
        {
            // Big Fat
            switch(mechType)
            {
                case MechType.DebugMechA:
                    return debugMechA;
                case MechType.DebugMechB:
                    return debugMechB;
                case MechType.DebugMechC:
                    return debugMechC;
                default:
                    return errorTexture;
            }
        }

        internal static Texture2D GetBulletTexture(BulletType bulletType)
        {
            switch(bulletType)
            {
                case BulletType.Basic:
                    return basicBullet;
                default:
                    return basicBullet;
            }
        }
    }
}
