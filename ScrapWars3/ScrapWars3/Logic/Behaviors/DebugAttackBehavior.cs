using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Data;
using Microsoft.Xna.Framework;
using ScrapWars3.Screens;

namespace ScrapWars3.Logic.Behaviors
{
    class DebugAttackBehavior : BehaviorState
    {
        private Mech currentTarget;

        public void Update(MechAiStateMachine stateMachine, GameTime gameTime, Battle battle)
        {
            if(battle.CurrentBattleState == BattleState.Unfinished)                
            {
                if(currentTarget == null || !currentTarget.IsAlive)
                { 
                    ChooseTarget(stateMachine, battle);                    
                }

                float gunRangeSq = stateMachine.Owner.MainGun.Range*stateMachine.Owner.MainGun.Range;

                if(DistanceToTargetSq(stateMachine) < gunRangeSq && stateMachine.Rng.NextDouble() > 0.99)
                {
                    stateMachine.Owner.FacePoint(stateMachine.CurrentTarget.Position);
                    stateMachine.Owner.Shoot();
                }
            }
            
        }
        private float DistanceToTargetSq(MechAiStateMachine stateMachine)
        {
            if(stateMachine.CurrentTarget == null)
                return float.MaxValue;

            return (stateMachine.Owner.Position - stateMachine.CurrentTarget.Position).LengthSquared( );
        }
        private void ChooseTarget(MechAiStateMachine stateMachine, Battle battle)
        {            
            Team enemyTeam = battle.GetOtherTeam(stateMachine.Owner.Team);

            List<Mech> possibleTargets = new List<Mech>( );
            foreach(Mech mech in enemyTeam.Mechs)
            {
                if(mech.IsAlive)
                {
                    possibleTargets.Add(mech);                    
                }
            }

            if(possibleTargets.Count == 0)
            {
                currentTarget = null;
                stateMachine.FollowingPath = false;
            }
            else
            { 
                int enemyNumber = stateMachine.Rng.Next(0, possibleTargets.Count);
                stateMachine.CurrentTarget = possibleTargets[enemyNumber];            
            }
        }
    }
}
