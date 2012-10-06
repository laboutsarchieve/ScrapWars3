﻿using System;
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
        public void Fire(Object shooter, Vector2 position, Vector2 direction)
        {
            if(direction.LengthSquared() != 1)
                direction.Normalize();

            Bullet bullet = new Bullet(shooter, damage, bulletType, position, bulletSpeed * direction);
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
            return new Gun(5, 1000, BulletType.Basic);
        }
    }
}
