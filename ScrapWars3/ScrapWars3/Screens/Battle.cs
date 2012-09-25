using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScrapWars3.Resources;
using Microsoft.Xna.Framework.Input;
using ScrapWars3.Data;

namespace ScrapWars3.Screens
{
    class Battle : Screen
    {
        private Map map;
        private Vector2 upperLeftOfView;

        private int TILE_SIZE;        

        public Battle(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window, Map map)
            : base(scrapWarsApp, graphics, window)
        {
            this.map = map;
            upperLeftOfView = Vector2.Zero;
            TILE_SIZE = GameTextureRepo.tileDirt.Width;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if(ExtendedKeyboard.IsKeyDown(Keys.Escape))
                scrapWarsApp.ChangeScreen(new MapSelection(scrapWarsApp, graphics, window));

            // TODO modify extended keyboard to facilitate "time between" logic
            if (ExtendedKeyboard.IsKeyDown(Keys.Up) || ExtendedKeyboard.IsKeyDown(Keys.W)) 
                MoveView(0,-1);
            if (ExtendedKeyboard.IsKeyDown(Keys.Down) || ExtendedKeyboard.IsKeyDown(Keys.S))
                MoveView(0,1);
            if (ExtendedKeyboard.IsKeyDown(Keys.Left) || ExtendedKeyboard.IsKeyDown(Keys.A)) 
                MoveView(-1, 0);
            if (ExtendedKeyboard.IsKeyDown(Keys.Right) || ExtendedKeyboard.IsKeyDown(Keys.D))
                MoveView(1, 0);

            // Get Player Input
            // Battle Logic
        }

        private void MoveView(int x, int y)
        {
            upperLeftOfView += new Vector2(x,y);

            Vector2 maxUpperLeft = new Vector2(map.Width, map.Height) - GameSettings.Resolution/TILE_SIZE;

            upperLeftOfView = Vector2.Clamp(upperLeftOfView, Vector2.Zero, maxUpperLeft);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            graphics.Clear(Color.White);
            spriteBatch.Begin();
            DrawBattleField();
            DrawHud();
            spriteBatch.End();
        }
        public void DrawHud()
        {            
            // Draw Avalible cards at top 1/6
            // Draw Stats at bottom 1/6th
        }
        public void DrawBattleField()
        {
            DrawTiles();
            //Draw Obstacles
            //Draw Units
        }
        private void DrawTiles()
        {
            Vector2 startLocation = new Vector2(0, GameSettings.Resolution.Y / 8);

            Point startTile = new Point((int)upperLeftOfView.X, (int)upperLeftOfView.Y);
            Point numTiles = new Point((int)(GameSettings.Resolution.X)/TILE_SIZE,
                                      (int)(6 * GameSettings.Resolution.Y / 8) / TILE_SIZE);            

            for(int x = 0; x < numTiles.X; x++)
            {
                if(startTile.X + x > map.Width - 1)
                    break;

                for(int y = 0; y < numTiles.Y; y++)
                {
                    if(startTile.Y + y > map.Height - 1)
                        break;

                    Tile tile = map[startTile.X+x, startTile.Y+y];

                    DrawTile(tile, new Vector2(startLocation.X + x*TILE_SIZE, startLocation.Y + y*TILE_SIZE));
                }                
            }
        }
        private void DrawTile(Tile tile, Vector2 location)
        {
            Texture2D tileTexture;
            switch(tile)
            {
                case Tile.Dirt:
                    tileTexture = GameTextureRepo.tileDirt;
                    break;
                case Tile.Grass:
                    tileTexture = GameTextureRepo.tileGrass;
                    break;
                case Tile.Sand:
                    tileTexture = GameTextureRepo.tileSand;
                    break;
                case Tile.Water:
                    tileTexture = GameTextureRepo.tileWater;
                    break;
                default:
                    tileTexture = GameTextureRepo.errorTexture;
                    break;
            }

            spriteBatch.Draw(tileTexture, location, Color.White);
        }
    }
}
