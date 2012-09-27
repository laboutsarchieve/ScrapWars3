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
        private Vector2 startOfBatlefield;
        private Vector2 endOfBatlefield;
        private Vector2 upperLeftOfView;
        private Team teamOne;
        private Team teamTwo;

        private bool DrawDebug;
        private int TILE_SIZE;

        public Battle(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window, Map map, Team teamOne, Team teamTwo)
            : base(scrapWarsApp, graphics, window)
        {
            this.map = map;
            this.teamOne = teamOne;
            this.teamTwo = teamTwo;

            startOfBatlefield = new Vector2(0, GameSettings.Resolution.Y / 8);
            upperLeftOfView = Vector2.Zero;
            TILE_SIZE = GameTextureRepo.tileDirt.Width;
            DrawDebug = false;

            PlaceTeams(teamOne, teamTwo);
        }
        private void PlaceTeams(Team teamOne, Team teamTwo)
        {
            Vector2 OffsetFromSize = new Vector2(4, map.Height / 2);
            for(int mechNum = 0; mechNum < teamOne.Mechs.Length; mechNum++)
            {
                teamOne.Mechs[mechNum].Location = TILE_SIZE * (OffsetFromSize + new Vector2(0, 5 * mechNum));
            }
            for(int mechNum = 0; mechNum < teamTwo.Mechs.Length; mechNum++)
            {
                teamTwo.Mechs[mechNum].Location = TILE_SIZE * (new Vector2(map.Width, map.Height + 5 * mechNum) - OffsetFromSize);
            }
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Escape))
                scrapWarsApp.ChangeScreen(new MapSelection(scrapWarsApp, graphics, window));

            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.OemTilde))
                DrawDebug = !DrawDebug;

            // TODO modify extended keyboard to facilitate "time between" logic
            if(ExtendedKeyboard.IsKeyDown(Keys.Up) || ExtendedKeyboard.IsKeyDown(Keys.W))
                MoveView(0, -1);
            if(ExtendedKeyboard.IsKeyDown(Keys.Down) || ExtendedKeyboard.IsKeyDown(Keys.S))
                MoveView(0, 1);
            if(ExtendedKeyboard.IsKeyDown(Keys.Left) || ExtendedKeyboard.IsKeyDown(Keys.A))
                MoveView(-1, 0);
            if(ExtendedKeyboard.IsKeyDown(Keys.Right) || ExtendedKeyboard.IsKeyDown(Keys.D))
                MoveView(1, 0);

            // Get Player Input
            // Battle Logic
        }

        private void MoveView(int x, int y)
        {
            upperLeftOfView += new Vector2(x, y);

            Vector2 maxUpperLeft = new Vector2(map.Width, map.Height) - GameSettings.Resolution / TILE_SIZE;

            upperLeftOfView = Vector2.Clamp(upperLeftOfView, Vector2.Zero, maxUpperLeft);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            graphics.Clear(Color.White);
            spriteBatch.Begin();
            DrawBattleField();
            DrawMechs();
            DrawHud();
            if(DrawDebug)
                DrawDebugInfo();
            spriteBatch.End();
        }


        public void DrawHud()
        {
            spriteBatch.Draw(GameTextureRepo.pixel,
                             new Rectangle(0, 0, (int)GameSettings.Resolution.X, (int)GameSettings.Resolution.Y / 6),
                             Color.White);
            spriteBatch.Draw(GameTextureRepo.pixel,
                             new Rectangle(0, 5 * (int)GameSettings.Resolution.Y / 6, (int)GameSettings.Resolution.X, (int)GameSettings.Resolution.Y / 6),
                             Color.White);
        }
        public void DrawBattleField()
        {
            DrawTiles();
            //Draw Obstacles
            DrawMechs();
        }
        private void DrawMechs()
        {
            foreach(Mech mech in teamOne.Mechs)
            {
                Vector2 screenLocation = startOfBatlefield + mech.Location - TILE_SIZE * upperLeftOfView;
                spriteBatch.Draw(GameTextureRepo.GetMechTexture(mech.MechType),
                                 screenLocation,
                                 teamOne.TeamColor);
            }
            foreach(Mech mech in teamTwo.Mechs)
            {
                Vector2 screenLocation = startOfBatlefield + mech.Location - TILE_SIZE * upperLeftOfView;
                spriteBatch.Draw(GameTextureRepo.GetMechTexture(mech.MechType),
                                 screenLocation,
                                 teamTwo.TeamColor);
            }
        }
        private void DrawTiles()
        {
            Point startTile = new Point((int)upperLeftOfView.X, (int)upperLeftOfView.Y);
            Point numTiles = new Point((int)(GameSettings.Resolution.X) / TILE_SIZE,
                                      (int)(6 * GameSettings.Resolution.Y / 8) / TILE_SIZE);

            for(int x = 0; x < numTiles.X; x++)
            {
                if(startTile.X + x > map.Width - 1)
                    break;

                for(int y = 0; y < numTiles.Y; y++)
                {
                    if(startTile.Y + y > map.Height - 1)
                        break;

                    Tile tile = map[startTile.X + x, startTile.Y + y];

                    DrawTile(tile, new Vector2(startOfBatlefield.X + x * TILE_SIZE, startOfBatlefield.Y + y * TILE_SIZE));
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

        public void DrawDebugInfo()
        {
            string upperLeftString = "Upper left Of View - (" + upperLeftOfView.X + ", " + upperLeftOfView.Y + ")";
            string mapSizeString = "Map Size - (" + map.Width + ", " + map.Height + ")";

            float stringHeight = FontRepo.SelectScreenFont.MeasureString(upperLeftOfView + mapSizeString).Y;

            spriteBatch.DrawString(FontRepo.SelectScreenFont, mapSizeString, new Vector2(0, GameSettings.Resolution.Y - stringHeight), Color.Black);
            spriteBatch.DrawString(FontRepo.SelectScreenFont, upperLeftString, new Vector2(0, GameSettings.Resolution.Y - stringHeight * 2), Color.Black);
            spriteBatch.DrawString(FontRepo.SelectScreenFont, "Debug Info", new Vector2(0, GameSettings.Resolution.Y - stringHeight * 3), Color.Black);
        }
    }
}
