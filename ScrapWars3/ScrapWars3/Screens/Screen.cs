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
        protected GameWindow window;

        public Screen(GraphicsDevice graphics, GameWindow window)
        {
            Refresh(graphics, window);
        }
        public virtual void Refresh(GraphicsDevice graphics, GameWindow window)
        {
            this.graphics = graphics;
            this.window = window;
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
