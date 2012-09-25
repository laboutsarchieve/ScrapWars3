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

            LoadProtoGameData( );
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ScreenTextureRepo.mainMenu = Content.Load<Texture2D>(@"art\progart_main_menu");
            ScreenTextureRepo.battleGUIFrame = Content.Load<Texture2D>(@"art\progart_battle_hud");
            ScreenTextureRepo.teamSelect = Content.Load<Texture2D>(@"art\progart_team_select");
            ScreenTextureRepo.mechSelect = Content.Load<Texture2D>(@"art\progart_unit_select");
            ScreenTextureRepo.customizeMech = Content.Load<Texture2D>(@"art\progart_unit_customize");
            ScreenTextureRepo.optionScreen = Content.Load<Texture2D>(@"art\progart_options_screen");

            GameTextureRepo.teamLogos[0] = Content.Load<Texture2D>(@"art\progart_team_logo0");
            GameTextureRepo.teamLogos[1] = Content.Load<Texture2D>(@"art\progart_team_logo1");
            GameTextureRepo.teamLogos[2] = Content.Load<Texture2D>(@"art\progart_team_logo2");

            GameTextureRepo.tileDirt = Content.Load<Texture2D>(@"art\progart_dirt");
            GameTextureRepo.tileGrass = Content.Load<Texture2D>(@"art\progart_grass");
            GameTextureRepo.tileSand = Content.Load<Texture2D>(@"art\progart_sand");
            GameTextureRepo.tileWater = Content.Load<Texture2D>(@"art\progart_water");

            GameTextureRepo.errorTexture = Content.Load<Texture2D>(@"art\error");

            FontRepo.mainMenuFont = Content.Load<SpriteFont>(@"font\main_menu_font");
            FontRepo.SelectScreenFont = FontRepo.mainMenuFont;

            LoadDebugContent();

            currentScreen = new MainMenu(this, GraphicsDevice, Window);
            previousScreen = currentScreen;
        }
        protected void LoadDebugContent()
        {
            GameTextureRepo.debugMechA = Content.Load<Texture2D>(@"art\progart_MechA");
            GameTextureRepo.debugMechB = Content.Load<Texture2D>(@"art\progart_MechB");
            GameTextureRepo.debugMechC = Content.Load<Texture2D>(@"art\progart_MechC");

        }
        private void LoadProtoGameData( )
        {
            // This is faked at the moment
            Team Frag = new Team("Frags", 0, Color.Blue);
            Frag.AddMech(new Mech("DebugMechA",0, MechType.DebugMechA));
            Frag.AddMech(new Mech("DebugMechA", 1, MechType.DebugMechA));
            Frag.AddMech(new Mech("DebugMechB", 2, MechType.DebugMechB));

            Team Scrapyard = new Team("Scrapyard", 1, Color.Red);
            Scrapyard.AddMech(new Mech("DebugMechA", 3, MechType.DebugMechA));
            Scrapyard.AddMech(new Mech("DebugMechC", 4, MechType.DebugMechC));
            Scrapyard.AddMech(new Mech("DebugMechC", 5, MechType.DebugMechC));
            Scrapyard.AddMech(new Mech("DebugMechB", 6, MechType.DebugMechB));

            Team Boomer = new Team("Boomers", 2, Color.Yellow);
            Boomer.AddMech(new Mech("DebugMechC", 7, MechType.DebugMechC));
            Boomer.AddMech(new Mech("DebugMechC", 8, MechType.DebugMechC));
            Boomer.AddMech(new Mech("DebugMechC", 9, MechType.DebugMechC));
            Boomer.AddMech(new Mech("DebugMechB", 10, MechType.DebugMechB));
            Boomer.AddMech(new Mech("DebugMechB", 11, MechType.DebugMechB));

            TeamDatabase.teams.Add(Frag);
            TeamDatabase.teams.Add(Scrapyard);
            TeamDatabase.teams.Add(Boomer);
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
