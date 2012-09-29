using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScrapWars3.Data;
using ScrapWars3.Resources;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars3
{
    static class GameSettings
    {
        static GameSettings()
        {
            Resolution = maxResolution;
        }

        private static Vector2 maxResolution = new Vector2(1600, 1200);
        private static Vector2 artScale;
        private static Vector2 resolution;

        public static Vector2 GetMechSize(MechType mechType)
        {
            Texture2D mechImage = GameTextureRepo.GetMechTexture(mechType);
            return new Vector2(mechImage.Width, mechImage.Height) * ArtScale;
        }
        public static Vector2 CenterOfScreen
        {
            get { return new Vector2(resolution.X / 2, resolution.Y / 2); }
        }
        public static Rectangle ScreenRectangle
        {
            get { return new Rectangle(0, 0, (int)resolution.X, (int)resolution.Y); }
        }
        public static Vector2 Resolution
        {
            get { return GameSettings.resolution; }
            set { GameSettings.resolution = value; artScale = value / maxResolution; }
        }
        public static Vector2 ArtScale { get { return artScale; } }

        public static int TileSize { get; set; }
    }
}
