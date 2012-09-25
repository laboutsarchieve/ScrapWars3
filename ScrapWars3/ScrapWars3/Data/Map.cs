using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ScrapWars3.Data
{
    class Map
    {
        private Tile[,] tiles;

        public Map(Point size)
        {
            tiles = new Tile[size.X, size.Y];
            ClearTo(Tile.Grass);
        }
        public void ClearTo(Tile tileType)
        {
            for(int x = 0; x < tiles.GetLength(0); x++)
            {
                for(int y = 0; y < tiles.GetLength(1); y++)
                {
                    tiles[x, y] = tileType;
                }
            }
        }
        public Tile this[int x, int y]
        {
            get { return tiles[x, y]; }
            set { tiles[x, y] = value; }
        }
        public int Width
        {
            get { return tiles.GetLength(0); }
        }
        public int Height
        {
            get { return tiles.GetLength(1); }
        }
    }
}