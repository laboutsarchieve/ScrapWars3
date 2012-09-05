using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScrapWars3.Resources;

namespace ScrapWars3.Screens
{
    class MainMenu : Screen
    {
        SpriteBatch spriteBatch;
        float imageDisplacement;
        public MainMenu(GraphicsDevice graphics, GameWindow window) : base(graphics, window)
        {
            spriteBatch = new SpriteBatch(graphics);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {     
            imageDisplacement = imageDisplacement + 1 % ScreenTextureRepo.mainMenu.Width;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            graphics.Clear(Color.Black);   
            spriteBatch.Begin( );            
            DrawBackground( );
            DrawMenuText( );
            spriteBatch.End( );
        }
        private void DrawBackground( )
        {            
            spriteBatch.Draw(ScreenTextureRepo.mainMenu, new Vector2(-imageDisplacement,0),null,Color.White,0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);            
            spriteBatch.Draw(ScreenTextureRepo.mainMenu, new Vector2(-imageDisplacement+ScreenTextureRepo.mainMenu.Width,0),null,Color.White,0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);            
        }
        private void DrawMenuText( )
        {
            string play = "Start a Battle";
            string customizeTeams = "Customize Teams";
            string option = "Options";

            float stringHeight = FontRepo.mainMenuFont.MeasureString(play).Y;
            float MENU_MIDDLE = 400;
            float MENU_TOP = 300 - stringHeight*1.5f;
   
            spriteBatch.DrawString(FontRepo.mainMenuFont,
                                   play,
                                   new Vector2(MENU_MIDDLE - FontRepo.mainMenuFont.MeasureString(play).X/2,MENU_TOP),
                                   Color.Yellow);
            spriteBatch.DrawString(FontRepo.mainMenuFont,
                                   customizeTeams,
                                   new Vector2(MENU_MIDDLE - FontRepo.mainMenuFont.MeasureString(play).X/2, MENU_TOP + stringHeight),
                                   Color.Yellow);
            spriteBatch.DrawString(FontRepo.mainMenuFont,
                                   option,
                                   new Vector2(MENU_MIDDLE - FontRepo.mainMenuFont.MeasureString(play).X/2, MENU_TOP + 2*stringHeight),
                                   Color.Yellow);
        }
    }
}
