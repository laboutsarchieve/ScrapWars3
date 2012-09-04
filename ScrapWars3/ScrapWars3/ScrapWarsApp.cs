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

namespace ScrapWars3
{
    public class ScrapWarsApp : Microsoft.Xna.Framework.Game
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
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            
            graphics.ApplyChanges();

            ScrapWarsEventManager.SetManager(new BasicEventManager());
            currentScreen = new MainMenu(GraphicsDevice, Window);
            previousScreen = currentScreen;
            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
        }

        private void ChangeScreen(Screen newScreen)
        {
            previousScreen = currentScreen;
            currentScreen = newScreen;
        }

        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

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
