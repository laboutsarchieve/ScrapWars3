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
    class UnitSelect : Screen
    {
        Team team;

        public UnitSelect(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window, Team team)
            : base(scrapWarsApp, graphics, window)
        {
            this.team = team;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Escape))
                scrapWarsApp.ChangeScreen(new TeamSelect(scrapWarsApp, graphics, window));

            // This will be more complicated when team card customization is added
            if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Enter))
                SelectCurrentUnit();


            // TODO: Process controls
        }
        private void SelectCurrentUnit()
        {
            scrapWarsApp.ChangeScreen(new UnitCustomize(scrapWarsApp, graphics, window, team, new Unit( )));
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ScreenTextureRepo.unitSelect, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
            DrawTeamInfo( );
            DrawUnits( );
            spriteBatch.End();
        }
        public void DrawTeamInfo( )
        {
            spriteBatch.Draw(team.Logo,
                             Vector2.Zero,
                             null,
                             team.LogoColor,
                             0.0f,
                             Vector2.Zero,
                             GameSettings.ArtScale,
                             SpriteEffects.None,
                             0);

            Vector2 nameLocation = new Vector2(team.Logo.Width * GameSettings.ArtScale.X /2 - FontRepo.teamSelectFont.MeasureString(team.Name).X/2,
                                               team.Logo.Height * GameSettings.ArtScale.Y);

            spriteBatch.DrawString(FontRepo.teamSelectFont, team.Name, nameLocation, Color.Black);

        }
        public void DrawUnits( )
        {

        }
    }
}
