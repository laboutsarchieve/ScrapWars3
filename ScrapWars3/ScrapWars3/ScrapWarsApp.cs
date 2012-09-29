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
        private GraphicsDeviceManager graphics;
        private Screen currentScreen;
        private Screen previousScreen;

        public ScrapWarsApp()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            GameSettings.Resolution = new Vector2(1000, 750);
            Vector2 currentResolution = GameSettings.Resolution;

            graphics.PreferredBackBufferWidth = (int)currentResolution.X;
            graphics.PreferredBackBufferHeight = (int)currentResolution.Y;
            graphics.ApplyChanges();

            ScrapWarsEventManager.SetManager(new BasicEventManager());

            base.Initialize();
        }
        protected override void LoadContent()
        {
            ScreenTextureRepo.mainMenu = Content.Load<Texture2D>(@"art\progart_main_menu");
            ScreenTextureRepo.mapGen = Content.Load<Texture2D>(@"art\progart_map_gen_screen");
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

            GameTextureRepo.pixel = new Texture2D(GraphicsDevice, 1, 1);
            Color[] whitePixel = { Color.White };
            GameTextureRepo.pixel.SetData<Color>(whitePixel);

            FontRepo.generalFont = Content.Load<SpriteFont>(@"font\main_menu_font");
            FontRepo.selectScreenFont = FontRepo.generalFont; // These are the same for the moment
            FontRepo.titleFont = Content.Load<SpriteFont>(@"font\title_font");
                

            LoadDebugContent();
            LoadProtoGameData();

            currentScreen = new MainMenu(this, GraphicsDevice, Window);
            previousScreen = currentScreen;
        }
        protected void LoadDebugContent()
        {
            GameTextureRepo.debugMechA = Content.Load<Texture2D>(@"art\progart_MechA");
            GameTextureRepo.debugMechB = Content.Load<Texture2D>(@"art\progart_MechB");
            GameTextureRepo.debugMechC = Content.Load<Texture2D>(@"art\progart_MechC");

        }
        private void LoadProtoGameData()
        {
            // This is faked at the moment
            Team frag = new Team("Frags", 0, Color.Blue);
            List<Mech> mechs = new List<Mech>();
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechA));
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechA));
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechA));
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechB));

            foreach(Mech mech in mechs)
                mech.MechColor = frag.TeamColor;
            frag.AddMechs(mechs);

            mechs.Clear();

            Team scrapyard = new Team("Scrapyard", 1, Color.Red);
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechB));
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechB));
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechC));
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechC));

            foreach(Mech mech in mechs)
                mech.MechColor = scrapyard.TeamColor;
            scrapyard.AddMechs(mechs);

            mechs.Clear();

            Team boomer = new Team("Boomers", 2, Color.Yellow);
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechA));
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechA));
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechC));
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechC));
            mechs.Add(MechFactory.GetBaseMechFromType(MechType.DebugMechC));

            foreach(Mech mech in mechs)
                mech.MechColor = boomer.TeamColor;
            boomer.AddMechs(mechs);


            TeamDatabase.teams.Add(frag);
            TeamDatabase.teams.Add(scrapyard);
            TeamDatabase.teams.Add(boomer);
        }
        protected override void UnloadContent()
        {
        }
        public void ChangeScreen(Screen newScreen)
        {
            previousScreen = currentScreen;
            currentScreen = newScreen;
        }
        internal void RevertScreen()
        {
            ChangeScreen(previousScreen);
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

