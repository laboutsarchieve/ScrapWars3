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
        private int damage;        
        private float bulletSpeed;
        private float range;

        private BulletType bulletType;
        
        public Gun(int damage, float bulletSpeed, float range, BulletType bulletType)
        {
            this.damage = damage;
            this.bulletSpeed = bulletSpeed;
            this.range = range;
            this.bulletType = bulletType;
        }
        public void Fire(Object shooter, Vector2 position, Vector2 direction)
        {
            if(direction.LengthSquared() != 1)
                direction.Normalize();

            Bullet bullet = new Bullet(shooter, damage, range, bulletType, position, bulletSpeed, direction);
            ScrapWarsEventManager.GetManager().SendEvent(new BulletFiredEvent(bullet));
        }
        internal BulletType BulletType
        {
            get { return bulletType; }
        }
        public int Damage
        {
            get { return damage; }
        }
        public float Range
        {
            get { return range; }            
        }
        internal static Gun DefaultGun()
        {
            return new Gun(5, 1000, 500,  BulletType.Basic);
        }
    }
}
