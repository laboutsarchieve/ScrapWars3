﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Data
{
    class Mech
    {
        string name;
        MechType mechType;
        int mechId;

        Vector2 facing;        
        Vector2 location;
        Vector2 size;
        
        float IMAGE_FACING_OFFSET = 3*(float)Math.PI/2;

        public Mech(string name, int mechId, MechType mechType)
        {
            this.name = name;
            this.mechType = mechType;
            this.mechId = mechId;

            Location = Vector2.Zero;
            facing = Vector2.UnitX;

            size = GameSettings.GetMechSize(mechType);
        }
        public void FacePoint( Vector2 target )
        {
            if( target == location ) // A mech can't face it's own center
                return;

            Vector2 toTarget = target - location;
            toTarget.Normalize( );
            facing = toTarget;
        }
        public string Name
        {
            get { return name; }
        }
        internal MechType MechType
        {
            get { return mechType; }
        }
        public int MechId
        {
            get { return mechId; }
        }
        public float FacingAngle
        {
            get { return (float)Math.Atan2(facing.Y, facing.X) + IMAGE_FACING_OFFSET; }
        }
        public Vector2 FacingVector
        {
            get { return facing; }         
        }
        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }
        public Vector2 Size
        {
            get { return size; }            
        }
        public Rectangle BoundingBox         
        { 
            get
            {
                return new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y);
            }
        }
    }
}