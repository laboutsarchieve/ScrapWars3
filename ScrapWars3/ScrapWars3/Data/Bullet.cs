using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Data
{
    class Bullet
    {
        private object shooter;
        private int damage;
        private float range;
        private float distTraveled;        
        private float speed;
        private Vector2 position;
        private Vector2 direction;
        private BulletType bulletType;
        

        public Bullet(Object shooter, int damage, float range, BulletType bulletType, Vector2 startPosition, float speed, Vector2 direction)
        {
            distTraveled = 0;

            this.shooter = shooter;
            this.damage = damage;
            this.range = range;
            this.speed = speed;
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
        public object Shooter
        {
            get { return shooter; }
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
        public int Damage
        {
            get { return damage; }
        }
        public bool RangeExceeded
        {
            get{ return distTraveled > range; }
        }
    }
}
