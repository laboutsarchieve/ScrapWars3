using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars3.Resources
{
    static class GameTextureRepo
    {
        private static int numLogos = 10;
        public static Texture2D[] teamLogos = new Texture2D[numLogos];
    }
}
