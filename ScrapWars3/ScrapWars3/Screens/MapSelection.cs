using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ScrapWars3.Resources;
using ScrapWars3.Logic;
using ScrapWars3.Data;

namespace ScrapWars3.Screens
{
    class MapSelection : Screen
    {
        enum Options
        {
            MapSize,
            OptionOne,
            OptionTwo,
            Battle,
            Return
        };

        string[] menuOptions = {"Map Size:",
                                "Option 1:",
                                "Option 2:",
                                "Start",
                                "Return to Main Menu"};

        Options currSelection;
        Color[] menuColors = new Color[5];

        Color mainMenuColor = Color.White;
        Color selectionColor = Color.Yellow;

        float lineHeight;
        float menuMiddle;
        float menuTop;

        public MapSelection(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window)
            : base(scrapWarsApp, graphics, window)
        {
            SetSelection(0);
        }
        public override void Refresh(GraphicsDevice graphics, GameWindow window)
        {
            UpdateMenuVaribles();
            base.Refresh(graphics, window);
        }
        private void UpdateMenuVaribles()
        {
            lineHeight = FontRepo.mainMenuFont.LineSpacing;

            menuTop = GameSettings.CenterOfScreen.Y - (menuOptions.Length * 0.5f * lineHeight);
            menuMiddle = GameSettings.CenterOfScreen.X;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ProcessInput();
        }
        private void ProcessInput()
        {
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Enter) || ExtendedKeyboard.IsKeyDownAfterUp(Keys.Space))
            {
                if(currSelection == Options.Battle)
                    StartBattle();
                else if( currSelection == Options.Return)
                    scrapWarsApp.ChangeScreen(new MainMenu(scrapWarsApp, graphics, window));
            }

            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Up) || ExtendedKeyboard.IsKeyDownAfterUp(Keys.W))
                SetSelection((int)currSelection - 1);
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Down) || ExtendedKeyboard.IsKeyDownAfterUp(Keys.S))
                SetSelection((int)currSelection + 1);
        }

        private void StartBattle()
        {
            MapGenerator mapGen = new MapGenerator();
            Map map = mapGen.GenerateMap(new Vector2(100, 100));
            scrapWarsApp.ChangeScreen(new Battle(scrapWarsApp,
                                      graphics,
                                      window,
                                      map,
                                      TeamDatabase.teams[0],
                                      TeamDatabase.teams[1]));
        }
        private void SetSelection(int option)
        {
            if(option < 0)
                option = menuOptions.Length-1;

            option %= menuOptions.Length;

            currSelection = (Options)option;

            for(int index = 0; index < menuOptions.Length; index++)
            {
                if(index == (int)option)
                    menuColors[index] = selectionColor;
                else
                    menuColors[index] = mainMenuColor;
            }
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            graphics.Clear(Color.Black);
            spriteBatch.Begin();
            DrawBackground();
            DrawMenuText();
            spriteBatch.End();
        }
        private void DrawBackground()
        {
            spriteBatch.Draw(ScreenTextureRepo.mapGen, Vector2.Zero, Color.White);
        }
        private void DrawMenuText()
        {
            for(int index = 0; index < menuOptions.Length; index++)
            {
                string option = menuOptions[index];
                spriteBatch.DrawString(FontRepo.mainMenuFont, option, new Vector2(menuMiddle - FontRepo.mainMenuFont.MeasureString(option).X / 2, menuTop + lineHeight * index), menuColors[index]);
            }
        }
    }
}
