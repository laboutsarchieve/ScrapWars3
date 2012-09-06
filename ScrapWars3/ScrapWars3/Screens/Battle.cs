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
    class Battle : Screen
    {
        public Battle(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window)
            : base(scrapWarsApp, graphics, window)
        {
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (ExtendedKeyboard.IsKeyDown(Keys.Escape))
                scrapWarsApp.ChangeScreen(new MainMenu(scrapWarsApp, graphics, window));
            // Get Player Input
            // Battle Logic
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            graphics.Clear(Color.White);
            spriteBatch.Begin();
            DrawBattleField();
            DrawHud();
            spriteBatch.End();
        }
        public void DrawHud()
        {
            spriteBatch.Draw(ScreenTextureRepo.battleGUIFrame, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
            // Draw Avalible cards at top 1/4
            // Draw Stats at bottom 1/6th
        }
        public void DrawBattleField()
        {
            //Draw Tiles
            //Draw Obstacles
            //Draw Units
        }
    }
}
