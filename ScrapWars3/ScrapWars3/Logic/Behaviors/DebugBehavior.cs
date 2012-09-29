using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Data;
using Microsoft.Xna.Framework;
using ScrapWars3.Screens;

namespace ScrapWars3.Logic.Behaviors
{
    class DebugBehavior : BehaviorState
    {
        private Mech currentTarget;
        public void Update(MechAiStateMachine stateMachine, GameTime gameTime, Battle battle)
        {
            if(currentTarget == null)
                ChooseTarget(stateMachine, battle);

            stateMachine.CurrentTargetLocation = currentTarget.Location;
            stateMachine.DesiredDistance = stateMachine.Rng.Next(1, 8) * 40;
        }

        private void ChooseTarget(MechAiStateMachine stateMachine, Battle battle)
        {
            Team enemyTeam = battle.GetOtherTeam(stateMachine.Owner.Team);

            int enemyNumber = stateMachine.Rng.Next(0, enemyTeam.Mechs.Length);
            currentTarget = enemyTeam.Mechs[enemyNumber];
        }
    }
}
