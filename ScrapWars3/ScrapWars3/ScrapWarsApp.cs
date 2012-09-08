using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameTools.Events;
using ScrapWars3.Screens;
using ScrapWars3.Resources;
using ScrapWars3.Data;

namespace ScrapWars3
{
    class ScrapWarsApp : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Screen currentScreen;
        Screen previousScreen;

        public ScrapWarsApp()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            GameSettings.Resolution = new Vector2(800, 600);
            Vector2 currentResolution = GameSettings.Resolution;

            graphics.PreferredBackBufferWidth = (int)currentResolution.X;
            graphics.PreferredBackBufferHeight = (int)currentResolution.Y;            
            graphics.ApplyChanges();

            ScrapWarsEventManager.SetManager(new BasicEventManager());

            LoadGameData( );
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ScreenTextureRepo.mainMenu = Content.Load<Texture2D>(@"art\progart_main_menu");
            ScreenTextureRepo.battleGUIFrame = Content.Load<Texture2D>(@"art\progart_battle_hud");
            ScreenTextureRepo.teamSelect = Content.Load<Texture2D>(@"art\progart_team_select");
            ScreenTextureRepo.unitSelect = Content.Load<Texture2D>(@"art\progart_unit_select");
            ScreenTextureRepo.customizeUnit = Content.Load<Texture2D>(@"art\progart_unit_customize");
            ScreenTextureRepo.optionScreen = Content.Load<Texture2D>(@"art\progart_options_screen");

            GameTextureRepo.teamLogos[0] = Content.Load<Texture2D>(@"art\progart_team_logo0");
            GameTextureRepo.teamLogos[1] = Content.Load<Texture2D>(@"art\progart_team_logo1");
            GameTextureRepo.teamLogos[2] = Content.Load<Texture2D>(@"art\progart_team_logo2");

            FontRepo.mainMenuFont = Content.Load<SpriteFont>(@"font\main_menu_font");
            FontRepo.teamSelectFont = FontRepo.mainMenuFont;

            currentScreen = new MainMenu(this, GraphicsDevice, Window);
            previousScreen = currentScreen;
        }

        private void LoadGameData( )
        {
            // This is faked at the moment
            TeamDatabase.teams.Add(new Team("Frags", 0, Color.Blue));
            TeamDatabase.teams.Add(new Team("Scrapyard", 1, Color.Red));
            TeamDatabase.teams.Add(new Team("Boomers", 2, Color.Yellow));
        }

        protected override void UnloadContent()
        {
        }

        public void ChangeScreen(Screen newScreen)
        {
            previousScreen = currentScreen;
            currentScreen = newScreen;
        }

        protected override void Update(GameTime gameTime)
        {
            ExtendedKeyboard.Update();

            currentScreen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            currentScreen.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
