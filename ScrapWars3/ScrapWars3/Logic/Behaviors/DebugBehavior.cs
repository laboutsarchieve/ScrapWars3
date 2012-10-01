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
        private int stepsSincePathfinder;
        public void Update(MechAiStateMachine stateMachine, GameTime gameTime, Battle battle)
        {
            if(currentTarget == null)
            { 
                ChooseTarget(stateMachine, battle);                
                stateMachine.Path = Pathfinder.FindPath(stateMachine.Owner, battle.Map, currentTarget.Location); 
                stepsSincePathfinder = 0;
            }

            if(stepsSincePathfinder > 100 && stateMachine.Rng.NextDouble( ) > 0.5) // This randomization helps make the mechs pathfind on diffrent cycles
            { 
                stateMachine.Path = Pathfinder.FindPath(stateMachine.Owner, battle.Map, currentTarget.Location);                
                stepsSincePathfinder = 0;
            }

            stepsSincePathfinder++;

            if(stateMachine.Rng.NextDouble() > 0.99)
            {
                stateMachine.Owner.FacePoint(currentTarget.Location);
                stateMachine.Owner.Shoot( );
            }
        }

        private void ChooseTarget(MechAiStateMachine stateMachine, Battle battle)
        {
            Team enemyTeam = battle.GetOtherTeam(stateMachine.Owner.Team);

            int enemyNumber = stateMachine.Rng.Next(0, enemyTeam.Mechs.Length);
            currentTarget = enemyTeam.Mechs[enemyNumber];
        }
    }
}
