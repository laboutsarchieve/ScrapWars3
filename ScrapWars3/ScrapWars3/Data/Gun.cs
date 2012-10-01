using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
            // Send an event that the above bullet has been fired
        }

        internal BulletType BulletType
        {
            get { return bulletType; }
        }
        public int Damage
        {
            get { return damage; }
        }

    }
}
