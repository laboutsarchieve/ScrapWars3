﻿using System;
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
        private int roundStart;
        private int ROUND_TIME = 15000;

        Battle battle;
        public BattleLogic(Battle battle)
        {
            this.battle = battle;
        }
       public void PlaceTeams(Team teamOne, Team teamTwo)
        {
            Vector2 offsetFromSide = battle.TileSize * new Vector2(4, 0);

            PlaceTeam(teamOne, battle.TileSize * new Vector2(0, battle.Map.Height / 2) + offsetFromSide, battle.TileSize * 4 * Vector2.UnitY, Vector2.UnitX);
            PlaceTeam(teamTwo, battle.TileSize * new Vector2(battle.Map.Width, battle.Map.Height / 2) - offsetFromSide, battle.TileSize * 4 * Vector2.UnitY, -Vector2.UnitX);
        }
        private void PlaceTeam(Team team, Vector2 preferedStart, Vector2 spacing, Vector2 facing)
        {
            facing.Normalize();

            Vector2 waterAvoidXMove;
            Vector2 waterAvoidYMove;

            if(battle.Map.Width / 2 - preferedStart.X / battle.TileSize > 0)
                waterAvoidXMove = battle.TileSize * Vector2.UnitX;
            else
                waterAvoidXMove = battle.TileSize * -Vector2.UnitX;

            if(battle.Map.Height / 2 - preferedStart.Y / battle.TileSize > 0)
                waterAvoidYMove = battle.TileSize * Vector2.UnitY;
            else
                waterAvoidYMove = battle.TileSize * -Vector2.UnitY;

            Vector2 waterAvoidance = Vector2.Zero;
            bool mechInWater;
            do
            {
                mechInWater = false;
                for(int mechNum = 0; mechNum < team.Mechs.Length; mechNum++)
                {
                    team.Mechs[mechNum].Location = waterAvoidance + preferedStart + spacing * mechNum;
                    team.Mechs[mechNum].FacePoint(team.Mechs[mechNum].Location + facing); // Face Right

                    if(CollisionDetector.IsMechOnTile(team.Mechs[mechNum], battle.Map, Tile.Water, battle.TileSize))
                    {
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
            if( !battle.BattlePaused )
                StepBattle(gameTime);
        }
        private void StepBattle(GameTime gameTime)
        {
            // Battle Logic

            if(gameTime.ElapsedGameTime.Milliseconds - roundStart > ROUND_TIME)
                battle.BattlePaused = true;
        }
    }
}