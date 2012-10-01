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
    // TODO: Add stats, guns and missles
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
        private Gun mainGun;        

        private Color mechColor;
        private float IMAGE_FACING_OFFSET = 3 * (float)Math.PI / 2;

        public Mech(string name, int mechId, MechType mechType)
        {
            Init(name, mechId, mechType, Color.White);
        }
        public Mech(string name, int mechId, MechType mechType, Color color)
        {
            Init(name, mechId, mechType, color);
        }
        private void Init(string name, int mechId, MechType mechType, Color color)
        {
            this.name = name;
            this.mechType = mechType;
            this.mechId = mechId;

            Location = Vector2.Zero;
            facing = Vector2.UnitX;

            size = GameSettings.GetMechSize(mechType);

            this.brain = new MechAiStateMachine(this, new DebugBehavior());

            mechColor = color;

            mainGun = Gun.DefaultGun( );
        }
        public void Think(GameTime gameTime, Battle battle)
        {
            brain.Think(gameTime, battle);
        }
        public void Update(GameTime gameTime, Battle battle)
        {
            if(brain.FollowingPath)
                Move(gameTime, battle);

            if( brain.Rng.NextDouble( ) > 0.995 )
                mainGun.Fire(location, facing);
        }
        private void Move(GameTime gameTime, Battle battle)
        {            
            // TODO make speed an actual attribute

            float speed = 80; // 40 pixels per second

            Vector2 toTarget = brain.CurrentTargetLocation - location;
            float distance = toTarget.Length();            

            bool towards = distance > brain.DesiredDistance;

            if(toTarget == Vector2.Zero)
                return;

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
        public Rectangle BoundingRect
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
        internal Gun MainGun
        {
            get { return mainGun; }
            set { mainGun = value; }
        }
    }
}
