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
    // TODO: Make team and mech select inherate from a base selection screen class
    class MechSelect : Screen
    {
        Team team;
        Mech[] mechs;
        int currentMech;

        public MechSelect(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window, Team team)
            : base(scrapWarsApp, graphics, window)
        {
            this.team = team;
            mechs = team.Mechs;
            currentMech = 0;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Escape))
                scrapWarsApp.ChangeScreen(new TeamSelect(scrapWarsApp, graphics, window));

            // This will be more complicated when team card customization is added
            if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Enter))
                SelectCurrentMech();

            if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Left) || ExtendedKeyboard.IsKeyDownAfterUp(Keys.A))
                SelectMechLeft();

            if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Right) || ExtendedKeyboard.IsKeyDownAfterUp(Keys.D))
                SelectMechRight();
        }

        private void SelectMechRight()
        {
            if (currentMech == mechs.Length - 1)
                currentMech = 0;

            else currentMech++;
        }
        private void SelectMechLeft()
        {
            if (currentMech == 0)
                currentMech = mechs.Length - 1;

            else currentMech--;
        }
        private void SelectCurrentMech()
        {
            scrapWarsApp.ChangeScreen(new MechCustomize(scrapWarsApp, graphics, window, team, mechs[currentMech]));
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ScreenTextureRepo.mechSelect, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
            DrawTeamInfo( );
            DrawMechs( );
            spriteBatch.End();
        }
        public void DrawTeamInfo( )
        {
            spriteBatch.Draw(team.Logo,
                             Vector2.Zero,
                             null,
                             team.TeamColor,
                             0.0f,
                             Vector2.Zero,
                             GameSettings.ArtScale,
                             SpriteEffects.None,
                             0);

            Vector2 nameLocation = new Vector2(team.Logo.Width * GameSettings.ArtScale.X /2 - FontRepo.SelectScreenFont.MeasureString(team.Name).X/2,
                                               team.Logo.Height * GameSettings.ArtScale.Y);

            spriteBatch.DrawString(FontRepo.SelectScreenFont, team.Name, nameLocation, Color.Black);

        }
        
        public void DrawMechs()
        {
            Mech mech = mechs[currentMech];
            DrawSingleMech(mech, GameSettings.CenterOfScreen, team.TeamColor);

            if (currentMech != 0)
            {
                mech = mechs[currentMech - 1];
                DrawSingleMech(mech, new Vector2(GameSettings.Resolution.X / 4, GameSettings.CenterOfScreen.Y), team.TeamColor);
            }

            if (currentMech != mechs.Length - 1)
            {
                mech = mechs[currentMech + 1];
                DrawSingleMech(mech, new Vector2(3 * GameSettings.Resolution.X / 4, GameSettings.CenterOfScreen.Y), team.TeamColor);
            }
        }
        public void DrawSingleMech(Mech mech, Vector2 center, Color mechColor)
        {
            Texture2D mechTexture = GameTextureRepo.GetMechTexture(mech.MechType);

            spriteBatch.Draw(mechTexture,
                             center,
                             null,
                             mechColor,
                             0.0f,
                             new Vector2(mechTexture.Width / 2, mechTexture.Height / 2),
                             GameSettings.ArtScale,
                             SpriteEffects.None,
                             0);

            Vector2 nameLocation = new Vector2(center.X - FontRepo.SelectScreenFont.MeasureString(mech.Name).X / 2,
                                               center.Y - mechTexture.Height * GameSettings.ArtScale.Y - FontRepo.SelectScreenFont.MeasureString(mech.Name).Y);

            spriteBatch.DrawString(FontRepo.SelectScreenFont, mech.Name, nameLocation, team.TeamColor);
        }
    }
}