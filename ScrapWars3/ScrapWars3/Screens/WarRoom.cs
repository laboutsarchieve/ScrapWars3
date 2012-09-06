using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScrapWars3.Resources;

namespace ScrapWars3.Screens
{
    enum WarRoomState
    {
        TeamSelect,
        UnitSelect,
        CustomizeUnit
    }
    class WarRoom : Screen
    {
        WarRoomState warRoomState;

        public WarRoom(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window)
            : base(scrapWarsApp,graphics, window)
        {
            warRoomState = WarRoomState.TeamSelect;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // TODO: Process controls
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            spriteBatch.Begin();
            switch(warRoomState)
            {
                case WarRoomState.TeamSelect:
                    DrawTeamSelect( );
                    break;
                case WarRoomState.UnitSelect:
                    DrawUnitSelect( );
                    break;
                case WarRoomState.CustomizeUnit:
                    DrawCustomizeUnit( );
                    break;

            }
            spriteBatch.End();
        }

        private void DrawTeamSelect()
        {
            spriteBatch.Draw(ScreenTextureRepo.warRoomTeamSelect, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
        }
        private void DrawUnitSelect()
        {
            spriteBatch.Draw(ScreenTextureRepo.warRoomTeamUnitSelect, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
        }
        private void DrawCustomizeUnit()
        {
            spriteBatch.Draw(ScreenTextureRepo.warRoomTeamCustomizeUnit, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
        }        
    }
}
