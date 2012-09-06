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
    class UnitSelect : Screen
    {
        public UnitSelect(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window)
            : base(scrapWarsApp, graphics, window)
        {
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
            scrapWarsApp.ChangeScreen(new UnitCustomize(scrapWarsApp, graphics, window));
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ScreenTextureRepo.unitSelect, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
            spriteBatch.End();
        }
    }
}
