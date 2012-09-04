using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScrapWars3.Screens
{
    class MainMenu : Screen
    {
        public MainMenu(GraphicsDevice graphics, GameWindow window) : base(graphics, window)
        {
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {            
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            graphics.Clear(Color.Black);           
        }
    }
}
