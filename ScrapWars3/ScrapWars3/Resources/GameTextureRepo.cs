using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ScrapWars3.Data;
using Microsoft.Xna.Framework;

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

        public static GraphicsDevice graphics; // TODO: move this and the scale texture function to another class
        public static Dictionary<float, Texture2D> scaledBulletCache = new Dictionary<float,Texture2D>( );

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
        public static Texture2D GetScaledTexture(Texture2D texture, float scale)
        {
            if(!scaledBulletCache.ContainsKey(scale))
            {
                RenderTarget2D target = new RenderTarget2D(graphics, (int)(texture.Width * scale), (int)(texture.Height * scale));

                graphics.SetRenderTarget(target);
                SpriteBatch spriteBatch = new SpriteBatch(graphics);
                spriteBatch.Begin();
                spriteBatch.Draw(texture, target.Bounds, Color.White);
                spriteBatch.End();
                graphics.SetRenderTarget(null);

                scaledBulletCache[scale] = (Texture2D)target;
            }

            return scaledBulletCache[scale];
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
