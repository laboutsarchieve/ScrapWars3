using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScrapWars3
{
    static class GameSettings
    {
        static GameSettings()
        {
            Resolution = maxResolution;
        }

        private static Vector2 maxResolution = new Vector2(1600,900);
        private static Vector2 artScale;
        private static Vector2 resolution;

        public static Vector2 Resolution
        {
            get { return GameSettings.resolution; }
            set { GameSettings.resolution = value; artScale = value / maxResolution; }
        }
        public static Vector2 ArtScale
        {
            get
            {
                return artScale;
            }
        }
    }
}
