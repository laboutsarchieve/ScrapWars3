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
        private BehaviorState previousBehavior;
        private BehaviorState behavior;
        private Mech owner;
        private BehaviorState globalBehavior;
        private Vector2 currentTargetLocation;
        private float desiredDistance;
        private int nodeOnPath;        
        private bool followingPath = false;        
        private List<Vector2> path = new List<Vector2>();

        private static Random rng = new Random();

        public MechAiStateMachine(Mech owner, BehaviorState startBehavior, BehaviorState globalBehavior)
        {
            this.owner = owner;
            previousBehavior = startBehavior;
            this.behavior = startBehavior;
            this.globalBehavior = globalBehavior;
        }
        public MechAiStateMachine(Mech owner, BehaviorState startBehavior)
        {
            this.owner = owner;
            previousBehavior = startBehavior;
            this.behavior = startBehavior;
            this.globalBehavior = new DummyBehavior();

            rng = new Random();
        }
        internal void Think(GameTime gameTime, Battle battle)
        {
            globalBehavior.Update(this, gameTime, battle);
            behavior.Update(this, gameTime, battle);

            if(followingPath)
            {
                float closeEnough = Math.Max(owner.Size.X,owner.Size.Y) * 1.5f;
                if((currentTargetLocation - owner.Location).LengthSquared() < closeEnough * closeEnough)
                { 
                    nodeOnPath++;
                    if(nodeOnPath >= path.Count)
                    { 
                        followingPath = false;
                        return;
                    }

                    currentTargetLocation = GameSettings.TileSize*path[nodeOnPath];                    
                }
            }
        }
        public void ChangeBehavior(BehaviorState newBehavior)
        {
            previousBehavior = newBehavior;
            behavior = newBehavior;
        }
        public Vector2 CurrentTargetLocation
        {
            get { return currentTargetLocation; }
            set { currentTargetLocation = value; }
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
                    desiredDistance = Math.Max(owner.Size.X,owner.Size.Y) * 1.5f;
                    currentTargetLocation = GameSettings.TileSize*path[nodeOnPath];
                }
            }
        }
        internal BehaviorState Behavior
        {
            get { return behavior; }
        }
        internal BehaviorState GlobalBehavior
        {
            get { return globalBehavior; }
        }
        internal Mech Owner
        {
            get { return owner; }
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
    }
}
