using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Screens;
using Microsoft.Xna.Framework;
using ScrapWars3.Data;

namespace ScrapWars3.Logic
{
    class BattleLogic
    {
        private Battle battle;

        public BattleLogic(Battle battle)
        {
            this.battle = battle;
        }
        public void PlaceTeams(Team teamOne, Team teamTwo)
        {
            Vector2 offsetFromSide = GameSettings.TileSize * new Vector2(4, 0);

            Vector2 middleLeft = GameSettings.TileSize * new Vector2(0, battle.Map.Height / 2) + offsetFromSide;
            Vector2 middleRight = GameSettings.TileSize * new Vector2(battle.Map.Width, battle.Map.Height / 2) - offsetFromSide;
            Vector2 mechSpacing = GameSettings.TileSize * 4 * Vector2.UnitY;

            PlaceTeam(teamOne, middleLeft, mechSpacing, Vector2.UnitX);
            PlaceTeam(teamTwo, middleRight, mechSpacing, -Vector2.UnitX);
        }
        private void PlaceTeam(Team team, Vector2 preferedStart, Vector2 spacing, Vector2 facing)
        {
            Vector2 waterAvoidXMove;
            Vector2 waterAvoidYMove;

            // Decide what direction to move the mechs if they are spawning on water
            if(battle.Map.Width / 2 - preferedStart.X / GameSettings.TileSize > 0)
                waterAvoidXMove = GameSettings.TileSize * Vector2.UnitX;
            else
                waterAvoidXMove = GameSettings.TileSize * -Vector2.UnitX;
            if(battle.Map.Height / 2 - preferedStart.Y / GameSettings.TileSize > 0)
                waterAvoidYMove = GameSettings.TileSize * Vector2.UnitY;
            else
                waterAvoidYMove = GameSettings.TileSize * -Vector2.UnitY;

            Vector2 waterAvoidance = Vector2.Zero;
            bool mechInWater;
            do // Until all mech spawn outside of water
            {
                mechInWater = false;
                for(int mechNum = 0; mechNum < team.Mechs.Length; mechNum++)
                {
                    team.Mechs[mechNum].Location = waterAvoidance + preferedStart + spacing * mechNum;
                    team.Mechs[mechNum].FacePoint(team.Mechs[mechNum].Location + facing);

                    // Test the mech's spawn area for water
                    if(CollisionDetector.IsMechOnTile(team.Mechs[mechNum], battle.Map, Tile.Water))
                    {
                        // If water is found, move the spawn area and try again
                        waterAvoidance += waterAvoidYMove;
                        if((waterAvoidance + preferedStart + spacing * team.Mechs.Length).Y < 0 ||
                           (waterAvoidance + preferedStart + spacing * team.Mechs.Length).Y > battle.Map.Height)
                        {
                            waterAvoidance.Y = 0;
                            waterAvoidance += waterAvoidXMove;
                        }
                        mechInWater = true;
                        break;
                    }
                }
            }
            while(mechInWater);
        }
        public void Update(GameTime gameTime)
        {
            if(!battle.BattlePaused)
                StepBattle(gameTime);
        }
        private void StepBattle(GameTime gameTime)
        {
            // Battle Logic
            UpdateMech(gameTime);
            UpdateBullets(gameTime);
            HandleCollisions(gameTime);

            if(gameTime.TotalGameTime.TotalMilliseconds - battle.RoundStart > battle.TimePerRound)
                battle.BattlePaused = true;
        }                
        private void UpdateMech(GameTime gameTime)
        {
            foreach(Mech mech in battle.AllMechs)
            {
                mech.Think(gameTime, battle);
                mech.Update(gameTime, battle);
            }
        }
        private void UpdateBullets(GameTime gameTime)
        {
            foreach(Bullet bullet in battle.Bullets)
            {
                bullet.Update(gameTime);
            }            
        }
        private void HandleCollisions(GameTime gameTime)
        {
            // Mech Bullet collisions
            // Mech Mech collisions

            // Bullet map edge collisions
            // Mech map edge collisions
        }
    }
}
