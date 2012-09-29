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
        public bool ContainsWater( Rectangle area )
        {
            for(int x = area.X; x < area.X+area.Width; x++)
            {
                if(!IsOnMap(x,0))
                    break;

                for(int y = area.Y; y < area.Y+area.Height; y++)
                {
                    if(!IsOnMap(x,y))
                        break;
                    Tile tile = tiles[x,y];
                    if(tiles[x,y] == Tile.Water)
                        return true;
                }
            }

            return false;
        }
        public bool IsOnMap( int x, int y )
        {
            return (x >= 0 && x <= tiles.GetLength(0) - 1 &&
                    y >= 0 && y <= tiles.GetLength(1) - 1 );
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