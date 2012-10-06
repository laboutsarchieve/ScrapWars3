using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScrapWars3.Screens;
using ScrapWars3.Logic;
using ScrapWars3.Logic.Behaviors;
using GameTools.Events;
using ScrapWars3.Data.Event;

namespace ScrapWars3.Data
{
    // TODO: Change to velocity based movement
    // TODO: Add stats, guns and missles
    // TODO: Collect stats in a structure
    class Mech
    {
        private MechType mechType;
        private MechAiStateMachine brain;
        private string name;
        private int mechId;
        private int maxSpeed;
        private int currHp;
        private int maxHp;

        private Team team;
        private Vector2 facing;
        private VectorSmoother smoothFacing;
        private Vector2 position;
        private Vector2 size;
        private Gun mainGun;

        private Color mechColor;
        private float IMAGE_FACING_OFFSET = 3 * (float)Math.PI / 2;

        // The hp and speed settings here are temporary defaults
        public Mech(string name, int mechId, MechType mechType)
        {
            Init(name, mechId, mechType, 20, 80, Color.White);
            Subscribe( );
        }
        public Mech(string name, int mechId, MechType mechType, Color color)
        {
            Init(name, mechId, mechType, 20, 80, color);
            Subscribe( );
        }
        ~Mech()
        {
            ScrapWarsEventManager.GetManager().UpsubscribeFromAll(this);
        }
        public void Subscribe()
        {
            ScrapWarsEventManager.GetManager().Subscribe(this, AnalyzeCollision, "BulletHitMech");
        }
        public bool AnalyzeCollision(BaseGameEvent theEvent)
        {
            BulletHitMechEvent bulletHit = (BulletHitMechEvent)theEvent;

            if(bulletHit.Mech.mechId == mechId)
            {
                currHp -= bulletHit.Bullet.Damage;
                if(!IsAlive)
                    ScrapWarsEventManager.GetManager().SendEvent(new MechDiedEvent(this));
            }

            return false;
        }
        public void Restore()
        {
            Init(name, mechId, mechType, 20, 80, mechColor);
        }
        private void Init(string name, int mechId, MechType mechType, int maxHp, int maxSpeed, Color color)
        {
            this.name = name;
            this.mechType = mechType;
            this.mechId = mechId;
            this.maxHp = maxHp;
            this.currHp = maxHp;
            this.maxSpeed = maxSpeed;

            Position = Vector2.Zero;
            facing = Vector2.UnitX;
            smoothFacing = new VectorSmoother(20);
            smoothFacing.SetSmoothVector(facing);

            size = GameSettings.GetMechSize(mechType);

            this.brain = new MechAiStateMachine(this, new BasicMoveBehavior(), new BasicAttackBehavior( ));

            mechColor = color;

            mainGun = Gun.DefaultGun();
        }
        public void Think(GameTime gameTime, Battle battle)
        {
            brain.Think(gameTime, battle);
        }
        public void Update(GameTime gameTime, Battle battle)
        {
            if(brain.FollowingPath)
                Move(gameTime, battle);
        }
        public void Shoot()
        {
            mainGun.Fire(this, position + facing * size.X, facing);
        }
        private void Move(GameTime gameTime, Battle battle)
        {
            // TODO: add velocity attribute            

            Vector2 toTarget = brain.CurrentTargetPosition - position;
            float distance = toTarget.Length();

            bool towards = distance > brain.DesiredDistance;

            if(toTarget == Vector2.Zero)
                return;

            toTarget.Normalize();
            ChangeFacing(toTarget);
            toTarget *= maxSpeed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            
            Position += toTarget;          
        }

        private void ChangeFacing(Vector2 target)
        {
            facing = target;
            smoothFacing.AddVector(facing);
        }
        public void FacePoint(Vector2 target)
        {
            // TODO: This need to be non-instantanious

            if(target == position) // A mech can't face its own center
                return;

            Vector2 toTarget = target - position;
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
        internal MechAiStateMachine Brain
        {
            get { return brain; }
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
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 Size
        {
            get { return size; }
        }
        public int MaxSpeed
        {
            get { return maxSpeed; }
        }
        public int CurrHp
        {
            get { return currHp; }
        }
        public bool IsAlive
        {
            get{ return currHp > 0; }
        }
        public Rectangle BoundingRect
        {
            get { return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y); }
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
