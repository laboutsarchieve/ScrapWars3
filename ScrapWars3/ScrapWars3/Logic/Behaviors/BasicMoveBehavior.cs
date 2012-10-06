using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Data;
using Microsoft.Xna.Framework;
using ScrapWars3.Screens;

namespace ScrapWars3.Logic.Behaviors
{
    class BasicMoveBehavior : BehaviorState
    {
        private int stepsSincePathfinder;        

        public void EnterState(MechAiStateMachine stateMachine, Battle battle)
        {
            if(stateMachine.CurrentMainEnemy != null)
                Pathfind(stateMachine, battle);
        }
        public void ExitState(MechAiStateMachine stateMachine, Battle battle)
        {
            stateMachine.FollowingPath = false;
            stateMachine.Path = new List<Vector2>( );
        }
        public void Update(MechAiStateMachine stateMachine, GameTime gameTime, Battle battle)
        {
            PlanLongTermMovement(stateMachine, gameTime, battle);
            UpdatePath(stateMachine, battle);
            PlanNextMove(stateMachine, battle);
        }
        private void PlanLongTermMovement(MechAiStateMachine stateMachine, GameTime gameTime, Battle battle)
        {
            if(stateMachine.CurrentMainEnemy != null && !stateMachine.EnemyAtDesiredDistance( ))
            {
                if(stateMachine.Path.Count == 0 || (stepsSincePathfinder > 100 && stateMachine.Rng.NextDouble() > 0.5)) // This randomization helps make the mechs pathfind on diffrent cycles
                {
                    Pathfind(stateMachine, battle);
                }

                stepsSincePathfinder++;
            }
        }
        private void UpdatePath(MechAiStateMachine stateMachine, Battle battle)
        {
            if(stateMachine.FollowingPath)
            {
                float closeEnough = Math.Max(stateMachine.Owner.Size.X, stateMachine.Owner.Size.Y) * 1.5f;
                if((stateMachine.CurrentTargetPosition - stateMachine.Owner.Position).LengthSquared() < closeEnough * closeEnough)
                {
                    stateMachine.NodeOnPath++;
                    if(stateMachine.NodeOnPath >= stateMachine.Path.Count)
                    {
                        stateMachine.FollowingPath = false;
                        return;
                    }
                }
            }
        }
        private void PlanNextMove(MechAiStateMachine stateMachine, Battle battle)
        {
            if(stateMachine.FollowingPath && stateMachine.Path.Count > stateMachine.NodeOnPath)
            { 
                if(stateMachine.EnemyAtDesiredDistance())       
                {                    
                    stateMachine.CurrentTargetPosition = stateMachine.Owner.Position;  
                }
                else
                    stateMachine.CurrentTargetPosition = stateMachine.Path[stateMachine.NodeOnPath];
            }
        }
        private void Pathfind(MechAiStateMachine stateMachine, Battle battle)
        {
            List<Vector2> path = Pathfinder.FindPath(stateMachine.Owner, battle.Map, stateMachine.CurrentMainEnemy.Position);
            if(path.Count > 0)
                TrimPath(path);

            for(int index = 0; index < path.Count; index++)
            {
                path[index] *= GameSettings.TileSize;
            }

            if(path.Count > 0)
                stateMachine.FollowingPath = true;

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
                Vector2 moveOne = path[index + 1] - path[index];
                Vector2 moveTwo = path[index + 2] - path[index + 1];

                if(moveOne == moveTwo) // While the next two steps are moving in the same direction
                {
                    toRemove.Add(index + 1);
                }
            }

            for(int index = 0; index < toRemove.Count; index++)
            {
                path.RemoveAt(toRemove[index] - index);
            }
        }
    }
}
