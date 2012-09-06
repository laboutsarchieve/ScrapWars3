using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars3.Screens
{
    abstract class Screen
    {
        protected GraphicsDevice graphics;
        protected SpriteBatch spriteBatch;
        protected GameWindow window;
        protected ScrapWarsApp scrapWarsApp;

        public Screen(ScrapWarsApp scrapWarsApp,GraphicsDevice graphics, GameWindow window)
        {
            this.scrapWarsApp = scrapWarsApp;
            Refresh(graphics, window);
        }
        public virtual void Refresh(GraphicsDevice graphics, GameWindow window)
        {
            this.graphics = graphics;
            this.window = window;

            spriteBatch = new SpriteBatch(graphics);
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
