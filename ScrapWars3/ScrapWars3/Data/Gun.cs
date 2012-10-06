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
        private float bulletScale;        

        private BulletType bulletType;        

        public Gun(int damage, float bulletSpeed, float range, float bulletScale, BulletType bulletType)
        {
            this.damage = damage;
            this.bulletSpeed = bulletSpeed;            
            this.range = range;
            this.bulletScale = bulletScale;
            this.bulletType = bulletType;
        }
        public void Fire(Mech shooter, Vector2 position, Vector2 direction)
        {
            if(direction.LengthSquared() != 1)
                direction.Normalize();

            Bullet bullet = new Bullet(shooter.Team, damage, range, bulletScale, bulletType, position, bulletSpeed, direction);
            ScrapWarsEventManager.GetManager().SendEvent(new BulletFiredEvent(bullet));
        }
        public static Gun GetRandomGun(Point possibleDamage, Vector2 possibleRange, Vector2 possibleSpeed)
        {
            int damage = rng.Next(possibleDamage.X, possibleDamage.Y + 1);
            float range = (float)(rng.NextDouble() * (possibleRange.Y - possibleRange.X) + possibleRange.X);
            float speed = (float)(rng.NextDouble() * (possibleRange.Y - possibleRange.X) + possibleRange.X);

            return new Gun(damage, speed, range, 1, BulletType.Basic);
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
        public float BulletScale
        {
            get { return bulletScale; }
            set { bulletScale = value; }
        }
        internal static Gun DefaultGun
        {
            get
            {
                return new Gun(5, 1000, 500,1, BulletType.Basic);
            }
        }
    }
}
