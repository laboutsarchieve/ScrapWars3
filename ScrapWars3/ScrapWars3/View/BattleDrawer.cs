using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScrapWars3.Resources;
using ScrapWars3.Data;

namespace ScrapWars3.View
{
    class BattleDrawer
    {
        private Battle battle;
        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;

        private RenderTarget2D mapTexture;

        private Vector2 startOfBatlefield;

        public BattleDrawer(Battle battle, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            this.battle = battle;
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;

            startOfBatlefield = new Vector2(0, GameSettings.Resolution.Y / 8);

            mapTexture = new RenderTarget2D(graphics, GameSettings.TileSize * battle.Map.Width, GameSettings.TileSize * battle.Map.Height);
        }
        public void Draw(GameTime gameTime)
        {
            graphics.Clear(Color.White);
            spriteBatch.Begin();
            DrawBattleField();
            DrawMechs();
            DrawHud(gameTime);
            spriteBatch.End();
        }
        public void DrawHud(GameTime gameTime)
        {
            spriteBatch.Draw(ScreenTextureRepo.battleGUIFrame, GameSettings.ScreenRectangle, Color.White);

            if(!battle.BattlePaused)
            {
                int secondsLeft = (int)(battle.TimePerRound - (gameTime.TotalGameTime.TotalMilliseconds - battle.RoundStart)) / 1000 + 1;

                string secondsString = "" + secondsLeft + " Seconds Left";
                float stringTop = GameSettings.Resolution.Y - FontRepo.generalFont.MeasureString(secondsString).Y;
                float stringLeft = GameSettings.Resolution.X - FontRepo.generalFont.MeasureString(secondsString).X;

                spriteBatch.DrawString(FontRepo.generalFont, secondsString, new Vector2(stringLeft, stringTop), Color.Black);
            }
        }
        public void DrawBattleField()
        {
            if(battle.MapChanged)
                RefreshMap();

            spriteBatch.Draw((Texture2D)mapTexture, startOfBatlefield - GameSettings.TileSize * battle.UpperLeftOfView, Color.White);

            DrawMechs();
            DrawBullets();
        }
        private void DrawMechs()
        {
            foreach(Mech mech in battle.AllMechs)
            {
                if(mech.IsAlive)
                { 
                    Vector2 screenPosition = startOfBatlefield + mech.Position - GameSettings.TileSize * battle.UpperLeftOfView;
                    spriteBatch.Draw(GameTextureRepo.GetMechTexture(mech.MechType),
                                     screenPosition,
                                     null,
                                     mech.MechColor,
                                     mech.FacingAngle + mech.ImageFacingOffset,
                                     mech.Size / 2,
                                     GameSettings.ArtScale,
                                     SpriteEffects.None,
                                     0.5f);
                }
            }
        }
        private void DrawBullets()
        {
            foreach(Bullet bullet in battle.Bullets)
            {
                Vector2 screenPosition = startOfBatlefield + bullet.Position - GameSettings.TileSize * battle.UpperLeftOfView;

                spriteBatch.Draw(GameTextureRepo.GetBulletTexture(bullet.BulletType),
                                 screenPosition,
                                 null,
                                 Color.Black,
                                 0.0f,
                                 Vector2.Zero,
                                 GameSettings.ArtScale,
                                 SpriteEffects.None,
                                 0.5f);
            }
        }
        private void RefreshMap()
        {
            mapTexture = new RenderTarget2D(graphics, GameSettings.TileSize * battle.Map.Width, GameSettings.TileSize * battle.Map.Height);
            spriteBatch.End();
            graphics.SetRenderTarget(mapTexture);
            graphics.Clear(Color.Black);

            spriteBatch.Begin();
            DrawTiles();
            //Draw Obstacles
            spriteBatch.End();

            graphics.SetRenderTarget(null);
            battle.MapChanged = false;

            spriteBatch.Begin();
        }
        private void DrawTiles()
        {
            Point startTile = new Point((int)battle.UpperLeftOfView.X, (int)battle.UpperLeftOfView.Y);
            Point numTiles = new Point(battle.Map.Width, battle.Map.Height);

            for(int x = 0; x < battle.Map.Width; x++)
            {
                for(int y = 0; y < battle.Map.Height; y++)
                {
                    Tile tile = battle.Map[startTile.X + x, startTile.Y + y];
                    DrawTile(tile, new Vector2(x * GameSettings.TileSize, y * GameSettings.TileSize));
                }
            }
        }
        private void DrawTile(Tile tile, Vector2 position)
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

            spriteBatch.Draw(tileTexture, position, Color.White);
        }
    }
}
