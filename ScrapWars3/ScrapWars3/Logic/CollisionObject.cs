using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Logic
{
    class CollisionObject
    {
        Texture2D texture;
        Vector2 upperLeft;
        float facing;
        Color[,] colorArray;
        Matrix textureTransformation;
        Matrix textureTransformationInverse;

        CollisionObject(Texture2D texture, Vector2 upperLeft, float facing)
        {
            this.texture = texture;
            this.upperLeft = upperLeft;
            this.facing = facing;

            textureTransformation = Matrix.CreateTranslation(new Vector3(upperLeft.X,upperLeft.Y,0)) * Matrix.CreateRotationZ(facing);
            textureTransformationInverse = Matrix.Invert(textureTransformation);

            colorArray = new Color[texture.Width, texture.Height];
        }
        public Vector2 GetTransformedScreenLocation(int x, int y)
        {
            Vector2 location = new Vector2(x,y);
            Vector2.Transform(location, textureTransformation);

            return location;
        }
        public Color GetColorAtScreenLocation(int x, int y)
        {
            Vector2 location = new Vector2(x,y);
            Vector2.Transform(location, textureTransformationInverse);

            if(location.X > colorArray.GetLength(0) || location.Y > colorArray.GetLength(1))
                return Color.Transparent;

            return colorArray[(int)location.X, (int)location.Y];
        }
        public Color[,] ColorArray
        {
            get { return colorArray; }            
        }
    }
}
