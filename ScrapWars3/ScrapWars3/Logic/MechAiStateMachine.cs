using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScrapWars3.Screens;
using ScrapWars3.Logic.Behaviors;
using ScrapWars3.Data;

namespace ScrapWars3.Logic
{
    // TODO: Have seperate moving and shooting AI
    class MechAiStateMachine
    {
        private BehaviorState moveBehavior;
        private BehaviorState attackBehavior;
        private Mech owner;
        private Mech currentMainEnemy;
        private BehaviorState globalBehavior;
        private Vector2 currentTargetPosition;
        private int nodeOnPath;
        private bool followingPath = false;
        private List<Vector2> path = new List<Vector2>();

        private Random rng = new Random();
        private float desiredDistance;
        private Battle battle;        

        public MechAiStateMachine(Mech owner, BehaviorState startMoveBehavior, BehaviorState startAttackBehavior, BehaviorState globalBehavior)
        {
            this.owner = owner;
            this.moveBehavior = startMoveBehavior;
            this.attackBehavior = startAttackBehavior;
            this.globalBehavior = globalBehavior;
        }
        public MechAiStateMachine(Mech owner, BehaviorState startMoveBehavior, BehaviorState startAttackBehavior)
        {
            this.owner = owner;
            this.moveBehavior = startMoveBehavior;
            this.attackBehavior = startAttackBehavior;
            this.globalBehavior = new DummyBehavior();

            rng = new Random();
        }

        public MechAiStateMachine(MechAiStateMachine sourceMechAiStateMachine)
        {   
            this.owner = sourceMechAiStateMachine.owner;
            this.globalBehavior = sourceMechAiStateMachine.globalBehavior;            
            this.moveBehavior = sourceMechAiStateMachine.moveBehavior;
            this.attackBehavior = sourceMechAiStateMachine.attackBehavior;
            this.rng = sourceMechAiStateMachine.Rng;
        }
        internal void Think(GameTime gameTime, Battle battle)
        {
            this.battle = battle;
            globalBehavior.Update(this, gameTime, battle);
            attackBehavior.Update(this, gameTime, battle);
            moveBehavior.Update(this, gameTime, battle);
        }
        public float DistanceToMainEnemySq()
        {
            if(CurrentMainEnemy == null)
                return float.MaxValue;

            return (Owner.Position - CurrentMainEnemy.Position).LengthSquared();
        }
        public bool EnemyAtDesiredDistance()
        {
            return (Owner.Position - CurrentMainEnemy.Position).LengthSquared() < desiredDistance * desiredDistance;
        }
        public Vector2 CurrentTargetPosition
        {
            get { return currentTargetPosition; }
            set { currentTargetPosition = value; }
        }
        public float DesiredDistance
        {
            get { return desiredDistance; }
            set { desiredDistance = value; }
        }
        public List<Vector2> Path
        {
            get { return path; }
            set
            {
                path = value;
                nodeOnPath = 0;

                if(path.Count > 0)
                {
                    currentTargetPosition = GameSettings.TileSize * path[nodeOnPath];
                }
            }
        }
        internal BehaviorState GlobalBehavior
        {
            get { return globalBehavior; }
        }
        internal Mech Owner
        {
            get { return owner; }
        }
        internal Mech CurrentMainEnemy
        {
            get { return currentMainEnemy; }
            set { currentMainEnemy = value; }
        }
        public bool FollowingPath
        {
            get { return followingPath; }
            set { followingPath = value; }
        }
        public int NodeOnPath
        {
            get { return nodeOnPath; }
            set { nodeOnPath = value; }
        }
        public Random Rng
        {
            get { return rng; }            
        }
        internal BehaviorState MoveBehavior
        {
            get { return moveBehavior; }
            set
            {
                moveBehavior.ExitState(this, battle);
                moveBehavior = value;
                moveBehavior.EnterState(this, battle);
            }
        }
        internal BehaviorState AttackBehavior
        {
            get { return attackBehavior; }
            set 
            { 
                attackBehavior.ExitState(this, battle);
                attackBehavior = value;
                attackBehavior.EnterState(this, battle);
            }
        }
    }
}
