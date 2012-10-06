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
        private BulletType bulletType;
        private Vector2 position;
        private Vector2 velocity;

        public Bullet(Object shooter, int damage, BulletType bulletType, Vector2 startPosition, Vector2 velocity)
        {
            this.shooter = shooter;
            this.damage = damage;
            this.bulletType = bulletType;
            this.position = startPosition;
            this.velocity = velocity;
        }
        public void Update(GameTime gameTime)
        {
            position += velocity * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
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
            get { return velocity; }
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
