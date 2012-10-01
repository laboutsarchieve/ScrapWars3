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
        private BulletType bulletType;

        public Gun(int damage, float bulletSpeed, BulletType bulletType)
        {
            this.damage = damage;
            this.bulletSpeed = bulletSpeed;
            this.bulletType = bulletType;
        }
        public void Fire(Vector2 location, Vector2 direction)
        {
            if(direction.LengthSquared() != 1)
                direction.Normalize();

            Bullet bullet = new Bullet(damage, bulletType, location, bulletSpeed * direction);
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
        internal static Gun DefaultGun()
        {
            return new Gun(1, 2000, BulletType.Basic);
        }
    }
}
