using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScrapWars3.Data;
using ScrapWars3.Resources;

namespace ScrapWars3.Logic
{
    struct CollisionObject
    {
        private Texture2D texture;
        private Vector2 upperLeft;
        private float facing;
        private Color[,] colorArray;
        private Matrix textureTransformation;
        private Matrix textureTransformationInverse;

        public CollisionObject(Texture2D texture, Vector2 upperLeft, float facing)
        {
            this.texture = texture;
            this.upperLeft = upperLeft;
            this.facing = facing;

            textureTransformation = Matrix.CreateTranslation(new Vector3(upperLeft.X, upperLeft.Y, 0)) * Matrix.CreateScale(GameSettings.ArtScale.X) * Matrix.CreateRotationZ(facing);
            textureTransformationInverse = Matrix.Invert(textureTransformation);

            colorArray = new Color[texture.Width, texture.Height];
            Color[] flatArray = new Color[texture.Width*texture.Height];
            texture.GetData<Color>(flatArray);

            for(int x = 0; x < texture.Width; x++)
            {
                for(int y = 0; y < texture.Height; y++)
                {
                    colorArray[x,y] = flatArray[x*texture.Width+y];
                }
            }
        }        
        public Vector2 GetTransformedScreenLocation(int x, int y)
        {
            Vector2 location = new Vector2(x, y);
            Vector2.Transform(location, textureTransformation);

            return location;
        }
        public Color GetColorAtScreenLocation(int x, int y)
        {
            Vector2 location = new Vector2(x, y);
            Vector2.Transform(location, textureTransformationInverse);

            if(location.X > colorArray.GetLength(0) || location.Y > colorArray.GetLength(1))
                return Color.Transparent;

            return colorArray[(int)location.X, (int)location.Y];
        }
        public Color[,] ColorArray
        {
            get { return colorArray; }
        }
        public CollisionObject FromMech(Mech mech)
        {
            return new CollisionObject(GameTextureRepo.GetMechTexture(mech.MechType), mech.Location - mech.Size / 2, mech.FacingAngle);
        }
    }
}
