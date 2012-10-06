using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ScrapWars3.Data.Event;

namespace ScrapWars3.Data
{
    class Gun
    {
        static Random rng = new Random();
        private int damage;
        private float bulletSpeed;        
        private float range;
        private float rateOfFire;
        private float bulletScale;        
        private int timeLastFired;

        private BulletType bulletType;        

        public Gun(int damage, float bulletSpeed, float range, float rateOfFire, float bulletScale, BulletType bulletType)
        {
            this.damage = damage;
            this.bulletSpeed = bulletSpeed;            
            this.range = range;
            this.rateOfFire = rateOfFire;
            this.bulletScale = bulletScale;
            this.bulletType = bulletType;
        }
        public void Fire(Mech shooter, Vector2 position, Vector2 direction, int time)
        {
            if (time - timeLastFired > 1000/rateOfFire)
            {
                timeLastFired = time;
                if (direction.LengthSquared() != 1)
                    direction.Normalize();

                Bullet bullet = new Bullet(shooter.Team, damage, range, bulletScale, bulletType, position, bulletSpeed, direction);
                ScrapWarsEventManager.GetManager().SendEvent(new BulletFiredEvent(bullet));
            }
        }
        internal BulletType BulletType
        {
            get { return bulletType; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public float BulletSpeed
        {
            get { return bulletSpeed; }
            set { bulletSpeed = value; }
        }
        public float Range
        {
            get { return range; }
            set { range = value; }
        }
        public float BulletScale
        {
            get { return bulletScale; }
            set { bulletScale = value; }
        }
        internal static Gun DefaultGun
        {
            get
            {
                return new Gun(5, 1000, 500,10,1, BulletType.Basic);
            }
        }
    }
}
