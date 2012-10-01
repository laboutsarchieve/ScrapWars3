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
            TeamOne,
            TeamTwo,
            Battle,
            Return
        };

        private Point mapSize;

        private string[] menuOptions = {"Map Size:",
                                        "Team 1:",
                                        "Team 2:",
                                        "Start",
                                        "Return to Main Menu"};

        private Options currSelection;
        private Color[] menuColors = new Color[5];

        private Color mainMenuColor = Color.Black;
        private Color selectionColor = Color.Yellow;

        private float lineHeight;
        private float menuMiddle;
        private float menuTop;

        public MapSelection(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window)
            : base(scrapWarsApp, graphics, window)
        {
            SetSelection(0);
            mapSize = new Point(100, 100);
        }
        public override void Refresh(GraphicsDevice graphics, GameWindow window)
        {
            UpdateMenuVaribles();
            base.Refresh(graphics, window);
        }
        private void UpdateMenuVaribles()
        {
            lineHeight = FontRepo.generalFont.LineSpacing;

            menuTop = GameSettings.CenterOfScreen.Y - (menuOptions.Length * 0.5f * lineHeight);
            menuMiddle = GameSettings.CenterOfScreen.X;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ProcessInput();
        }
        private void ProcessInput()
        {
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Escape))
                scrapWarsApp.ChangeScreen(new MainMenu(scrapWarsApp, graphics, window));

            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Enter) || ExtendedKeyboard.IsKeyDownAfterUp(Keys.Space))
            {
                if(currSelection == Options.Battle)
                    StartBattle();
                else if(currSelection == Options.Return)
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
            Map map = mapGen.GenerateMap(new Vector2(mapSize.X, mapSize.Y));
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
                option = menuOptions.Length - 1;

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
                Vector2 location = new Vector2(menuMiddle - FontRepo.generalFont.MeasureString(option).X / 2, menuTop + lineHeight * index);

                spriteBatch.DrawString(FontRepo.generalFont, option, location, menuColors[index]);
                DrawOptionValue((Options)index, location + FontRepo.generalFont.MeasureString(option).X * Vector2.UnitX);
            }
        }
        private void DrawOptionValue(Options option, Vector2 location)
        {
            string valueString;
            Color color = Color.Black;
            switch(option)
            {
                case Options.MapSize:
                    valueString = " " + mapSize.X + " X " + mapSize.Y;
                    break;
                case Options.TeamOne:
                    valueString = TeamDatabase.teams[0].Name;
                    color = TeamDatabase.teams[0].TeamColor;
                    break;
                case Options.TeamTwo:
                    valueString = TeamDatabase.teams[1].Name;
                    color = TeamDatabase.teams[1].TeamColor;
                    break;
                default:
                    valueString = "";
                    break;
            }

            spriteBatch.DrawString(FontRepo.generalFont, valueString, location, color);
        }
    }
}
