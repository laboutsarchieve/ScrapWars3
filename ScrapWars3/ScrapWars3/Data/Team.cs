using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Resources;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Data
{
    class Team
    {
        private string name;
        private int logoIndex;
        private Color logoColor;

        private List<Mech> mechs;        

        public Team( string name, int logoIndex, Color logoColor)
        {            
            Name = name;
            LogoIndex = logoIndex;
            LogoColor = logoColor;

            mechs = new List<Mech>();
        }
        public void AddMech( Mech newMech )
        {
            mechs.Add(newMech);
        }
        public void removeMechs( int mechID )
        {
            for (int index = 0; index < mechs.Count; index++)
            {
                if (mechs[index].MechId == mechID)
                {
                    mechs.RemoveAt(index);
                    break;
                }
            }
        }
        public Texture2D Logo
        {
            get{ return GameTextureRepo.teamLogos[logoIndex]; }
        }
        public int LogoIndex
        {
            get { return logoIndex; }
            set { logoIndex = value; }
        }
        public Color LogoColor
        {
            get { return logoColor; }
            set { logoColor = value; }
        }
        public string Name
        {
            get { return name; }            
            set { name = value; }
        }
        internal Mech[] Mechs //This is returned as an array to prevent modification of the list
        {
            get { return mechs.ToArray( ); }
        }
    }
}
