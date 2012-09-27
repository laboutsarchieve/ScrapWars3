﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScrapWars3.Resources;
using Microsoft.Xna.Framework.Input;

namespace ScrapWars3.Screens
{
    class MainMenu : Screen
    {
        enum Options
        {
            Battle = 0,
            CustomizeSquad,
            Options,
            Exit
        };

        string[] menuOptions = {"Start a Battle",
                                "Customize Squad",
                                "Options",
                                "Exit" };

        Options currSelection;
        Color[] menuColors = new Color[4];

        Color mainMenuColor = Color.Black;
        Color selectionColor = Color.Yellow;

        float lineHeight;

        Rectangle menuBounds;


        float imageDisplacement;


        public MainMenu(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window)
            : base(scrapWarsApp, graphics, window)
        {
            SetSelection((int)Options.Battle);
        }
        public override void Refresh(GraphicsDevice graphics, GameWindow window)
        {
            UpdateMenuVaribles();
            base.Refresh(graphics, window);
        }

        private void UpdateMenuVaribles()
        {
            lineHeight = FontRepo.mainMenuFont.LineSpacing;

            int longestStringLength = 0;

            foreach(string option in menuOptions)
            {
                if(FontRepo.mainMenuFont.MeasureString(option).X > longestStringLength)
                    longestStringLength = (int)FontRepo.mainMenuFont.MeasureString(option).X;
            }

            int menuTop = (int)(GameSettings.CenterOfScreen.Y - (menuOptions.Length * 0.5f * lineHeight));
            int menuLeft = (int)(GameSettings.CenterOfScreen.X - longestStringLength/2);

            menuBounds = new Rectangle(menuLeft, menuTop, longestStringLength, (int)lineHeight * menuOptions.Length);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            imageDisplacement = (imageDisplacement + 0.25f) % (ScreenTextureRepo.mainMenu.Width * GameSettings.ArtScale.X);
            ProcessInput();
        }
        private void ProcessInput()
        {
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Enter) || ExtendedKeyboard.IsKeyDownAfterUp(Keys.Space))
            {
                if(currSelection == Options.Exit)
                    scrapWarsApp.Exit();
                else
                    scrapWarsApp.ChangeScreen(GetScreenFromCurrentSelection());
            }

            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Up) || ExtendedKeyboard.IsKeyDownAfterUp(Keys.W))
                SetSelection((int)currSelection - 1); // Move the selection up by one
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Down) || ExtendedKeyboard.IsKeyDownAfterUp(Keys.S))
                SetSelection((int)currSelection + 1); // Move the selection down by one            
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
        private Screen GetScreenFromCurrentSelection()
        {
            switch(currSelection)
            {
                case Options.Battle:
                    return new MapSelection(scrapWarsApp, graphics, window);
                case Options.CustomizeSquad:
                    return new TeamSelect(scrapWarsApp, graphics, window);
                case Options.Options:
                    return new OptionsScreen(scrapWarsApp, graphics, window);
                default:
                    throw new ArgumentOutOfRangeException(); // TODO: Replace with custom exception
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
            spriteBatch.Draw(ScreenTextureRepo.mainMenu, new Vector2(-imageDisplacement, 0), null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(ScreenTextureRepo.mainMenu, new Vector2(-imageDisplacement + ScreenTextureRepo.mainMenu.Width * GameSettings.ArtScale.X, 0), null, Color.White, 0.0f, Vector2.Zero, GameSettings.ArtScale, SpriteEffects.None, 0.0f);
        }
        private void DrawMenuText()
        {
            spriteBatch.Draw(GameTextureRepo.pixel, menuBounds, new Color(100, 100, 100, 180));

            for(int index = 0; index < menuOptions.Length; index++)
            {
                string option = menuOptions[index];
                spriteBatch.DrawString(FontRepo.mainMenuFont,
                                       option,
                                       new Vector2(menuBounds.Center.X - FontRepo.mainMenuFont.MeasureString(option).X / 2,
                                                   menuBounds.Top + lineHeight * index),
                                       menuColors[index]);
            }
        }
    }
}
