using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Data
{
    struct Bullet
    {
        private Team shooterTeam;
        private int damage;
        private float range;
        private float distTraveled;        
        private float speed;
        private float bulletScale;
        private Vector2 position;
        private Vector2 direction;
        private BulletType bulletType;
        

        public Bullet(Team shooterTeam, int damage, float range, float bulletScale, BulletType bulletType, Vector2 startPosition, float speed, Vector2 direction)
        {
            distTraveled = 0;

            this.shooterTeam = shooterTeam;
            this.damage = damage;
            this.range = range;
            this.speed = speed;                        
            this.bulletScale = bulletScale;
            this.bulletType = bulletType;
            this.position = startPosition;
            this.direction = direction;
        }        
        public void Update(GameTime gameTime)
        {
            if(!RangeExceeded)
            { 
                Vector2 movement = speed*direction* gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                distTraveled +=  speed*gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                position += movement;           
            }
        }
        public Team ShooterTeam
        {
            get { return shooterTeam; }
        }
        public int Damage
        {
            get { return damage; }
        }
        public float BulletScale
        {
            get { return bulletScale; }
        }
        public Vector2 Position
        {
            get { return position; }
        }
        public Vector2 Velocity
        {
            get { return speed*direction; }
        }
        internal BulletType BulletType
        {
            get { return bulletType; }
        }        
        public bool RangeExceeded
        {
            get{ return distTraveled > range; }
        }
    }
}
