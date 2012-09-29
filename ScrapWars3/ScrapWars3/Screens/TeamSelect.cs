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
    class TeamSelect : Screen
    {
        private int currentTeam;

        public TeamSelect(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window)
            : base(scrapWarsApp, graphics, window)
        {
            currentTeam = 0;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Escape))
                scrapWarsApp.ChangeScreen(new MainMenu(scrapWarsApp, graphics, window));

            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Enter))
                SelectCurrentTeam();

            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Left) || ExtendedKeyboard.IsKeyDownAfterUp(Keys.A))
                SelectTeamLeft();

            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Right) || ExtendedKeyboard.IsKeyDownAfterUp(Keys.D))
                SelectTeamRight();
        }

        private void SelectTeamRight()
        {
            //TODO: Add cool Transition Effect
            if(currentTeam == TeamDatabase.teams.Count - 1)
                currentTeam = 0;

            else currentTeam++;
        }

        private void SelectTeamLeft()
        {
            if(currentTeam == 0)
                currentTeam = TeamDatabase.teams.Count - 1;

            else currentTeam--;
        }
        private void SelectCurrentTeam()
        {
            scrapWarsApp.ChangeScreen(new MechSelect(scrapWarsApp, graphics, window, TeamDatabase.teams[currentTeam]));
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ScreenTextureRepo.teamSelect, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
            DrawTeams();
            // Display Team Statistics
            spriteBatch.End();
        }
        public void DrawTeams()
        {
            DrawSingleTeam(currentTeam, GameSettings.CenterOfScreen, TeamDatabase.teams[currentTeam].TeamColor);

            if(currentTeam != 0)
                DrawSingleTeam(currentTeam - 1, new Vector2(GameSettings.Resolution.X / 4, GameSettings.CenterOfScreen.Y), Color.White);

            if(currentTeam != TeamDatabase.teams.Count - 1)
                DrawSingleTeam(currentTeam + 1, new Vector2(3 * GameSettings.Resolution.X / 4, GameSettings.CenterOfScreen.Y), Color.White);
        }
        public void DrawSingleTeam(int teamNum, Vector2 center, Color nameColor)
        {
            Team team = TeamDatabase.teams[teamNum];

            spriteBatch.Draw(team.Logo,
                             center,
                             null,
                             team.TeamColor,
                             0.0f,
                             new Vector2(team.Logo.Width / 2, team.Logo.Height / 2),
                             GameSettings.ArtScale,
                             SpriteEffects.None,
                             0);

            Vector2 nameLocation = new Vector2(center.X - FontRepo.selectScreenFont.MeasureString(team.Name).X / 2,
                                               center.Y - team.Logo.Height / 2);

            spriteBatch.DrawString(FontRepo.selectScreenFont, team.Name, nameLocation, nameColor);
        }
    }
}
