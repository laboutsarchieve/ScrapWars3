using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScrapWars3.Resources;
using Microsoft.Xna.Framework.Input;
using ScrapWars3.Data;
using ScrapWars3.Logic;
using ScrapWars3.View;

namespace ScrapWars3.Screens
{
    class Battle : Screen
    {
        private Map map;
        private Vector2 upperLeftOfView;
        private Team teamOne;
        private Team teamTwo;
        private List<Mech> allMechs;

        private BattleDrawer battleDrawer;
        private BattleInput battleInput;
        private BattleLogic battleLogic;

        private bool battlePaused;
        private bool mapChanged;
        private double roundStart = 0;
        private int timePerRound = 5000; // 15 seconds

        public Battle(ScrapWarsApp scrapWarsApp, GraphicsDevice graphics, GameWindow window, Map map, Team teamOne, Team teamTwo)
            : base(scrapWarsApp, graphics, window)
        {
            this.map = map;
            this.teamOne = teamOne;
            this.teamTwo = teamTwo;

            allMechs = new List<Mech>();
            foreach(Mech mech in teamOne.Mechs)
            {
                allMechs.Add(mech);
            }
            foreach(Mech mech in teamTwo.Mechs)
            {
                allMechs.Add(mech);
            }

            mapChanged = true;
            battlePaused = true;
            upperLeftOfView = Vector2.Zero;
            GameSettings.TileSize = GameTextureRepo.tileDirt.Width;

            battleDrawer = new BattleDrawer(this, graphics, spriteBatch);
            battleInput = new BattleInput(this);
            battleLogic = new BattleLogic(this);

            battleLogic.PlaceTeams(teamOne, teamTwo);
        }
        internal void EndBattle()
        {
            scrapWarsApp.RevertScreen(); // TODO: Make this show a battle report screen
        }
        internal Team GetOtherTeam(Team team)
        {
            return (team == teamOne) ? teamTwo : teamOne;
        }
        public override void Refresh(GraphicsDevice graphics, GameWindow window)
        {
            mapChanged = true;
            base.Refresh(graphics, window);
        }
        public override void Update(GameTime gameTime)
        {
            battleInput.Update(gameTime);
            battleLogic.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            battleDrawer.Draw(gameTime);
        }
        internal Map Map
        {
            get { return map; }
        }
        public Vector2 UpperLeftOfView
        {
            get { return upperLeftOfView; }
            set { upperLeftOfView = value; }
        }
        internal Team TeamOne
        {
            get { return teamOne; }
        }
        internal Team TeamTwo
        {
            get { return teamTwo; }
        }
        internal List<Mech> AllMechs
        {
            get { return allMechs; }
        }
        public bool BattlePaused
        {
            get { return battlePaused; }
            set { battlePaused = value; }
        }
        public bool MapChanged
        {
            get { return mapChanged; }
            set { mapChanged = value; }
        }
        public double RoundStart
        {
            get { return roundStart; }
            set { roundStart = value; }
        }
        public int TimePerRound
        {
            get { return timePerRound; }
        }
    }
}
