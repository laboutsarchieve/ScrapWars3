﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Data;
using ScrapWars3.Screens;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Logic.Behaviors
{
    class AttackWeakestBehvior : BehaviorState
    {
        private Mech currentTarget;

        public void EnterState(MechAiStateMachine stateMachine, Battle battle)
        {
            ChooseTarget(stateMachine, battle);
        }
        public void ExitState(MechAiStateMachine stateMachine, Battle battle)
        {
            // Nothing to do
        }
        public void Update(MechAiStateMachine stateMachine, GameTime gameTime, Battle battle)
        {
            if(battle.CurrentBattleState == BattleState.Unfinished)
            {
                if(currentTarget == null || !currentTarget.IsAlive || stateMachine.Rng.NextDouble() > 0.999)
                {
                    ChooseTarget(stateMachine, battle);
                }

                float gunRangeSq = stateMachine.Owner.MainGun.Range * stateMachine.Owner.MainGun.Range;

                if(stateMachine.DistanceToMainEnemySq() < gunRangeSq )
                {
                    stateMachine.Owner.FacePoint(stateMachine.CurrentMainEnemy.Position);
                    stateMachine.Owner.Shoot((int)gameTime.TotalGameTime.TotalMilliseconds);
                }
            }

        }
        private void ChooseTarget(MechAiStateMachine stateMachine, Battle battle)
        {
            Team enemyTeam = battle.GetOtherTeam(stateMachine.Owner.Team);

            List<Mech> possibleTargets = new List<Mech>();
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
                int lowestHp = int.MaxValue;
                foreach(Mech target in possibleTargets)
                {
                    if(target.CurrHp < lowestHp)
                    { 
                        currentTarget = target;
                        lowestHp = target.CurrHp;
                    }
                }
            }
        }
    }
}