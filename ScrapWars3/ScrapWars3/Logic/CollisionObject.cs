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
        private Vector2 velocity;
        private float facing;
        private Color[,] colorArray;
        private Matrix textureTransformation;
        private Matrix textureTransformationInverse;

        public CollisionObject(Texture2D texture, Vector2 upperLeft, Vector2 velocity, float facing)
        {
            this.texture = texture;
            this.upperLeft = upperLeft;
            this.velocity = velocity;
            this.facing = facing;

            colorArray = new Color[texture.Width, texture.Height];
            Color[] flatArray = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(flatArray);

            for(int x = 0; x < texture.Width; x++)
            {
                for(int y = 0; y < texture.Height; y++)
                {
                    colorArray[y, x] = flatArray[x * texture.Width + y];
                }
            }

            textureTransformation = Matrix.CreateTranslation(new Vector3(upperLeft.X, upperLeft.Y, 0)) * Matrix.CreateRotationZ(facing);
            textureTransformationInverse = Matrix.Invert(textureTransformation);
        }
        public void Step(float numSeconds)
        {
            upperLeft += velocity * numSeconds;
            textureTransformation = Matrix.CreateTranslation(new Vector3(upperLeft.X, upperLeft.Y, 0)) * Matrix.CreateRotationZ(facing);
            textureTransformationInverse = Matrix.Invert(textureTransformation);

            UpdateTransformation();
        }
        private void UpdateTransformation()
        {
            textureTransformation = Matrix.CreateTranslation(new Vector3(upperLeft.X, upperLeft.Y, 0)) * Matrix.CreateScale(GameSettings.ArtScale.X) * Matrix.CreateRotationZ(facing);
            textureTransformationInverse = Matrix.Invert(textureTransformation);
        }

        public Vector2 GetTransformedScreenPosition(int x, int y)
        {
            Vector2 position = new Vector2(x, y);
            Vector2.Transform(position, textureTransformation);

            return position;
        }
        public Color GetColorAtScreenPosition(int x, int y)
        {
            Vector2 position = new Vector2(x, y);
            Vector2.Transform(position, textureTransformationInverse);

            if(position.X > colorArray.GetLength(0) || position.Y > colorArray.GetLength(1))
                return Color.Transparent;

            return colorArray[(int)position.X, (int)position.Y];
        }
        public Color[,] ColorArray
        {
            get { return colorArray; }
        }
        public static CollisionObject FromMech(Mech mech)
        {
            return new CollisionObject(GameTextureRepo.GetMechTexture(mech.MechType), mech.Position - mech.Size/2, Vector2.Zero, mech.FacingAngle+mech.ImageFacingOffset);
        }
        public static CollisionObject FromBullet(Bullet bullet)
        {
            Texture2D bulletTexture = GameTextureRepo.GetScaledTexture(GameTextureRepo.GetBulletTexture(bullet.BulletType), bullet.BulletScale);
            return new CollisionObject(bulletTexture, bullet.Position, bullet.Velocity, 0.0f);
        }
    }
}
