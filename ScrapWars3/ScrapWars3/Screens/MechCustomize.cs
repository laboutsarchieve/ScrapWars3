using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScrapWars3.Resources;
using Microsoft.Xna.Framework.Input;
using ScrapWars3.Data;

namespace ScrapWars3.Screens
{
    class MechCustomize : Screen
    {
        private Team team;
        private Mech mech;

        public MechCustomize(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window, Team team, Mech mech)
            : base(scrapWarsApp, graphics, window)
        {
            this.team = team;
            this.mech = mech;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Escape))
                scrapWarsApp.RevertScreen();

            // TODO: Process controls
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ScreenTextureRepo.customizeMech, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
            DrawUnit();
            spriteBatch.End();
        }
        public void DrawUnit()
        {
            Texture2D mechTexture = GameTextureRepo.GetMechTexture(mech.MechType);

            Vector2 spriteCenter = GameSettings.Resolution * 0.05f;

            spriteBatch.Draw(mechTexture,
                             spriteCenter,
                             null,
                             mech.MechColor,
                             0.0f,
                             Vector2.Zero,
                             GameSettings.ArtScale,
                             SpriteEffects.None,
                             0);

            Vector2 nameLocation = new Vector2(spriteCenter.X + mechTexture.Width * GameSettings.ArtScale.X / 2 - FontRepo.selectScreenFont.MeasureString(mech.Name).X / 2,
                                               spriteCenter.Y + mechTexture.Height * GameSettings.ArtScale.Y);

            spriteBatch.DrawString(FontRepo.selectScreenFont, mech.Name, nameLocation, team.TeamColor);
        }
    }
}
