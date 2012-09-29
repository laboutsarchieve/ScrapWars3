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

            mapTexture = new RenderTarget2D(graphics, battle.TileSize * battle.Map.Width, battle.TileSize * battle.Map.Height);
        }
        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            graphics.Clear(Color.White);
            spriteBatch.Begin();
            DrawBattleField();
            DrawMechs();
            DrawHud();
            spriteBatch.End();
        }
        public void DrawHud()
        {
            spriteBatch.Draw(ScreenTextureRepo.battleGUIFrame, GameSettings.ScreenRectangle, Color.White);
        }
        public void DrawBattleField()
        {
            if(battle.MapChanged)
                RefreshMap();

            spriteBatch.Draw((Texture2D)mapTexture, startOfBatlefield - battle.TileSize * battle.UpperLeftOfView, Color.White);

            DrawMechs();
        }
        private void DrawMechs()
        {
            foreach(Mech mech in battle.AllMechs)
            {
                Vector2 screenLocation = startOfBatlefield + mech.Location - battle.TileSize * battle.UpperLeftOfView;
                spriteBatch.Draw(GameTextureRepo.GetMechTexture(mech.MechType),
                                 screenLocation,
                                 null,
                                 mech.MechColor,
                                 mech.FacingAngle + mech.ImageFacingOffset,
                                 mech.Size / 2,
                                 Vector2.One,
                                 SpriteEffects.None,
                                 0.5f);
            }
        }
        private void RefreshMap()
        {
            mapTexture = new RenderTarget2D(graphics, battle.TileSize * battle.Map.Width, battle.TileSize * battle.Map.Height);
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
                    DrawTile(tile, new Vector2(x * battle.TileSize, y * battle.TileSize));
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
