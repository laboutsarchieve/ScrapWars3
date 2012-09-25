using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapWars3.Data;
using Microsoft.Xna.Framework;
using GameTools.Noise2D;

namespace ScrapWars3.Logic
{
    class MapGenerator
    {
        PerlinNoiseSettings2D noiseSettings;
        // TODO add generator settings

        public MapGenerator()
        {
            ResetNoiseSettings();
        }

        private void ResetNoiseSettings()
        {
            noiseSettings = new PerlinNoiseSettings2D();
            noiseSettings.persistence = 0.5f;
            noiseSettings.frequencyMulti = 2;
            noiseSettings.startingPoint = Vector2.Zero;
            noiseSettings.zoom = 20;
        }

        public Map GenerateMap(Vector2 size)
        {
            noiseSettings.size = size;
            Map map = new Map(new Point((int)size.X, (int)size.Y));

            FastPerlinNoise2D noiseMaker = new FastPerlinNoise2D(noiseSettings);

            float[] noise = new float[(int)(size.X * size.Y)];
            noiseMaker.FillWithPerlinNoise2D(noise);

            for(int index = 0; index < noise.Length; index++)
            {
                Tile tile = GetTileFromNoise(noise[index]);

                int x = index/map.Width;
                int y = index % map.Width;

                map[x,y] = tile;
            }

            return map;
        }

        public Tile GetTileFromNoise(float noise)
        {
            if(noise < -0.3f)
                return Tile.Water;
            else if(noise < -0.1f)
                return Tile.Sand;
            else if(noise < 0.0f)
                return Tile.Dirt;
            else
                return Tile.Grass;
        }
    }
}
