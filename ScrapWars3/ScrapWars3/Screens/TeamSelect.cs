using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScrapWars3.Resources;
using Microsoft.Xna.Framework.Input;

namespace ScrapWars3.Screens
{
    class TeamSelect : Screen
    {
        public TeamSelect(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window)
            : base(scrapWarsApp,graphics, window)
        {   
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Escape))
                scrapWarsApp.ChangeScreen(new MainMenu(scrapWarsApp, graphics, window));

            if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Enter))
                SelectCurrentTeam();
            // TODO: Process input
        }
        private void SelectCurrentTeam( )
        {
            scrapWarsApp.ChangeScreen(new UnitSelect(scrapWarsApp, graphics, window));
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ScreenTextureRepo.teamSelect, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
            // Display Teams
            // Display Team Statistics
            spriteBatch.End();
        }        
    }
}
