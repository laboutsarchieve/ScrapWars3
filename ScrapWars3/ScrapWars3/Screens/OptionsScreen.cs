using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScrapWars3.Resources;
using Microsoft.Xna.Framework.Input;
using GameTools.Input;

namespace ScrapWars3.Screens
{
    class OptionsScreen : Screen
    {
        public OptionsScreen(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window)
            : base(scrapWarsApp, graphics, window)
        {
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if(ExtendedKeyboard.IsKeyDown(Keys.Escape))
                scrapWarsApp.ChangeScreen(new MainMenu(scrapWarsApp, graphics, window));
            // Process input
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawBackground();
            DrawOptions();
            spriteBatch.End();
        }
        private void DrawBackground()
        {
            spriteBatch.Draw(ScreenTextureRepo.optionScreen, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
        }
        private void DrawOptions()
        {
            // TODO
        }
    }
}
