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
    class UnitCustomize : Screen
    {
        Team team;
        Unit unit;

        public UnitCustomize(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window, Team team, Unit unit)
            : base(scrapWarsApp, graphics, window)
        {
            this.team = team;
            this.unit = unit;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Escape))
                scrapWarsApp.ChangeScreen(new UnitSelect(scrapWarsApp, graphics, window, team));

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
