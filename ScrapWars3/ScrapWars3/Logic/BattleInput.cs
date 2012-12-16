using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ScrapWars3.Data;

namespace ScrapWars3.Logic
{
    class BattleInput
    {
        private Battle battle;

        public BattleInput(Battle battle)
        {
            this.battle = battle;
        }
        public void Update(GameTime gameTime)
        {
            DebugControls(gameTime);
            // Get Player Input          
        }
        private void DebugControls(GameTime gameTime)
        {
            if(ExtendedKeyboard.IsKeyDownAfterUp(Keys.Escape))
                battle.EndBattle();

            // TODO modify extended keyboard to facilitate "time between" logic
            if(ExtendedKeyboard.IsKeyDown(Keys.W))
                MoveView(0, -1);
            if(ExtendedKeyboard.IsKeyDown(Keys.S))
                MoveView(0, 1);
            if(ExtendedKeyboard.IsKeyDown(Keys.A))
                MoveView(-1, 0);
            if(ExtendedKeyboard.IsKeyDown(Keys.D))
                MoveView(1, 0);

            if (battle.BattlePaused)
            {

                if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Left))
                    MoveCardSelectLeft();
                if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Right))
                    MoveCardSelectRight();

                if (ExtendedKeyboard.IsKeyDownAfterUp(Keys.Space))
                {
                    battle.BattlePaused = false;
                    battle.RoundStart = gameTime.TotalGameTime.TotalMilliseconds;
                    //TODO: This should be moved to battle logic

                    battle.playerHand[battle.currCard].ApplyToMechs(battle.TeamOne.Mechs, 0);
                    battle.lastCardPlayed = battle.playerHand[battle.currCard];
                }
            }
        }        
        private void MoveCardSelectLeft()
        {
            battle.currCard--;
            if (battle.currCard < 0)
                battle.currCard = GameSettings.handSize - 1;
        }
        private void MoveCardSelectRight()
        {
            battle.currCard++;
            battle.currCard %= GameSettings.handSize;
        }
        private void MoveView(int x, int y)
        {
            battle.UpperLeftOfView += new Vector2(x, y);
            Vector2 maxUpperLeft = new Vector2(battle.Map.Width, battle.Map.Height) - GameSettings.Resolution / GameSettings.TileSize;
            battle.UpperLeftOfView = Vector2.Clamp(battle.UpperLeftOfView, Vector2.Zero, maxUpperLeft);
        }
    }
}
