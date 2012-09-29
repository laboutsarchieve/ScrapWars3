using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScrapWars3.Screens;
using ScrapWars3.Logic;
using ScrapWars3.Logic.Behaviors;

namespace ScrapWars3.Data
{
    class Mech
    {
        private MechType mechType;
        private MechAiStateMachine brain;
        private string name;
        private int mechId;
        private Team team;

        private Vector2 facing;
        private Vector2 location;
        private Vector2 size;

        private Color mechColor;
        private float IMAGE_FACING_OFFSET = 3 * (float)Math.PI / 2;

        public Mech(string name, int mechId, MechType mechType)
        {
            this.name = name;
            this.mechType = mechType;
            this.mechId = mechId;

            Location = Vector2.Zero;
            facing = Vector2.UnitX;

            size = GameSettings.GetMechSize(mechType);

            mechColor = Color.White;

            this.brain = new MechAiStateMachine(this, new DebugBehavior());
        }
        public Mech(string name, int mechId, MechType mechType, Color color)
        {
            this.name = name;
            this.mechType = mechType;
            this.mechId = mechId;

            Location = Vector2.Zero;
            facing = Vector2.UnitX;

            size = GameSettings.GetMechSize(mechType);

            mechColor = color;
        }
        public void Think(GameTime gameTime, Battle battle)
        {
            brain.Think(gameTime, battle);
        }
        public void Update(GameTime gameTime, Battle battle)
        {
            Move(gameTime, battle);
        }
        private void Move(GameTime gameTime, Battle battle)
        {
            // TODO pathfinding
            // TODO make speed and actual attribute

            float speed = 80; // 40 pixels per second

            Vector2 toTarget = brain.CurrentTargetLocation - location;

            float tooClose = brain.DesiredDistance - Math.Max(size.X, size.Y) * 2;
            float distance = toTarget.Length();

            bool towards;

            if(distance > brain.DesiredDistance)
                towards = true;
            else if(distance - tooClose < 0)
                towards = false;
            else
                return;

            if(toTarget != Vector2.Zero)
                toTarget.Normalize();

            facing = toTarget;

            toTarget *= speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            if(towards)
                Location += toTarget;
            else
                Location -= toTarget;
        }
        public void FacePoint(Vector2 target)
        {
            if(target == location) // A mech can't face its own center
                return;

            Vector2 toTarget = target - location;
            toTarget.Normalize();
            facing = toTarget;
        }
        public string Name
        {
            get { return name; }
        }
        internal MechType MechType
        {
            get { return mechType; }
        }
        public int MechId
        {
            get { return mechId; }
        }
        public float FacingAngle
        {
            get { return (float)Math.Atan2(facing.Y, facing.X); }
        }
        public Vector2 FacingVector
        {
            get { return facing; }
        }
        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }
        public Vector2 Size
        {
            get { return size; }
        }
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y); }
        }
        public float ImageFacingOffset
        {
            get { return IMAGE_FACING_OFFSET; }
        }
        internal Team Team
        {
            get { return team; }
            set { team = value; }
        }
        public Color MechColor
        {
            get { return mechColor; }
            set { mechColor = value; }
        }
    }
}
