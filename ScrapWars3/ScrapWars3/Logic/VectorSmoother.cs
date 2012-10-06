using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Logic
{
    class VectorSmoother
    {
        private int currentIndex;
        private Vector2[] previousVectors;

        public VectorSmoother(int numToRemeber)
        {
            previousVectors = new Vector2[numToRemeber];
            currentIndex = 0;
        }
        public void AddVector(Vector2 newVector)
        {
            previousVectors[currentIndex] = newVector;
            currentIndex++;

            if(currentIndex > previousVectors.Length - 1)
                currentIndex = 0;
        }
        public void SetSmoothVector(Vector2 vector)
        {
            for(int index = 0; index < previousVectors.Length; index++)
                previousVectors[index] = vector;
        }
        public Vector2 GetSmoothVector()
        {
            Vector2 meanVector = Vector2.Zero;

            foreach(Vector2 vector in previousVectors)
                meanVector += vector;

            return meanVector / previousVectors.Length;
        }
    }
}
