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
    class UnitCustomize : Screen
    {
        public UnitCustomize(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window)
            : base(scrapWarsApp, graphics, window)
        {
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Escape))
                scrapWarsApp.ChangeScreen(new UnitSelect(scrapWarsApp, graphics, window));

            // TODO: Process controls
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ScreenTextureRepo.customizeUnit, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
            spriteBatch.End();
        }
    }
}
