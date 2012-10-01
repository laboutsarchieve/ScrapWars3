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
                Pathfind(stateMachine, battle);
            }

            if(stepsSincePathfinder > 100 && stateMachine.Rng.NextDouble( ) > 0.5) // This randomization helps make the mechs pathfind on diffrent cycles
            { 
                Pathfind(stateMachine, battle);
            }

            stepsSincePathfinder++;

            if(stateMachine.Rng.NextDouble() > 0.99)
            {
                stateMachine.Owner.FacePoint(currentTarget.Location);
                stateMachine.Owner.Shoot( );
            }
        }

        private void Pathfind(MechAiStateMachine stateMachine, Battle battle)
        {
            List<Vector2> path = Pathfinder.FindPath(stateMachine.Owner, battle.Map, currentTarget.Location);      
            if( path.Count > 0 )
                TrimPath(path);
            stateMachine.Path = path;
            stateMachine.NodeOnPath = 0;
            stepsSincePathfinder = 0;
        }

        private void TrimPath(List<Vector2> path)
        {
            List<int> toRemove = new List<int>();
            path.RemoveAt(0); // The first node is just where the mech is standing
            for(int index = 0; index < path.Count - 2; index++)
            {
                Vector2 moveOne = path[index+1] - path[index];
                Vector2 moveTwo = path[index+2] - path[index+1];

                if(moveOne == moveTwo ) // While the next two steps are moving in the same direction
                {                       
                    toRemove.Add(index+1);                    
                }                    
            }

            for(int index = 0; index < toRemove.Count; index++)
            {
                path.RemoveAt(toRemove[index]-index);
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
