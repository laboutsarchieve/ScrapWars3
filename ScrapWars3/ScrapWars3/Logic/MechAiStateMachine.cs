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
    class MechAiStateMachine
    {
        private BehaviorState previousBehavior;
        private BehaviorState behavior;
        private Mech owner;
        private BehaviorState globalBehavior;
        private Vector2 currentTargetLocation;
        private float desiredDistance;

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
        public Random Rng
        {
            get { return rng; }
            set { rng = value; }
        }
    }
}
