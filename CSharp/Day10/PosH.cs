using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    internal record Pos(int X, int Y, int Height)   
    {
        public List<Pos> GetNeighbours(int[,] map, int width, int height, int sourceHeight)
        {
            var result = new List<Pos>();
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx != 0 && dy != 0)
                    {
                        continue;
                    }
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }

                    if (X + dx < 0 || X + dx >= width || Y + dy < 0 || Y + dy >= height)
                    {
                        continue;
                    }

                    if (map[Y + dy, X + dx] != sourceHeight + 1)
                    {
                        continue;
                    }
                    result.Add(new Pos(X + dx, Y + dy, Height + 1));
                }
            }
            return result;
        }

        public bool IsOnMap(int width, int height)
        {
            return X >= 0 && X < width && Y >= 0 && Y < height;
        }
    }
}
