﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Data
{
    class Bullet
    {
        private int damage;
        private BulletType bulletType;
        private Vector2 location;
        private Vector2 velocity;

        public Bullet( int damage, BulletType bulletType, Vector2 startLocation, Vector2 velocity )
        {
            this.damage = damage;
            this.bulletType = bulletType;
            this.location = startLocation;
            this.velocity = velocity;
        }
        public void Update(GameTime gameTime)
        {
            location += velocity * gameTime.ElapsedGameTime.Milliseconds/1000.0f;
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