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
        private Mech currentTarget;
        private BehaviorState globalBehavior;
        private Vector2 currentTargetPosition;
        private float desiredDistance;
        private int nodeOnPath;
        private bool followingPath = false;
        private List<Vector2> path = new List<Vector2>();

        private static Random rng = new Random();

        public MechAiStateMachine(Mech owner, BehaviorState startMoveBehavior, BehaviorState startAttackBehavior, BehaviorState globalBehavior)
        {
            this.owner = owner;            
            this.moveBehavior = startMoveBehavior;
            this.attackBehavior = startAttackBehavior;
            this.globalBehavior = globalBehavior;
        }
        public MechAiStateMachine(Mech owner,  BehaviorState startMoveBehavior, BehaviorState startAttackBehavior)
        {
            this.owner = owner;            
            this.moveBehavior = startMoveBehavior;
            this.attackBehavior = startAttackBehavior;
            this.globalBehavior = new DummyBehavior();

            rng = new Random();
        }
        internal void Think(GameTime gameTime, Battle battle)
        {
            globalBehavior.Update(this, gameTime, battle);
            attackBehavior.Update(this, gameTime, battle);
            moveBehavior.Update(this, gameTime, battle);

            if(followingPath)
            {
                float closeEnough = Math.Max(owner.Size.X, owner.Size.Y) * 1.5f;
                if((currentTargetPosition - owner.Position).LengthSquared() < closeEnough * closeEnough)
                {
                    nodeOnPath++;
                    if(nodeOnPath >= path.Count)
                    {
                        followingPath = false;
                        return;
                    }

                    currentTargetPosition = GameSettings.TileSize * path[nodeOnPath];
                }
            }
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
                    followingPath = true;
                    desiredDistance = Math.Max(owner.Size.X, owner.Size.Y) * 1.5f;
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
        internal Mech CurrentTarget
        {
            get { return currentTarget; }
            set { currentTarget = value; }
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
            set { rng = value; }
        }
        internal BehaviorState MoveBehavior
        {
            get { return moveBehavior; }
            set { moveBehavior = value; }
        }
        internal BehaviorState AttackBehavior
        {
            get { return attackBehavior; }
            set { attackBehavior = value; }
        }
    }
}
