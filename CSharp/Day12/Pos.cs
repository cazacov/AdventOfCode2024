using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Day12
{
    [DebuggerDisplay("{X}-{Y}")]
    internal record Pos(int X, int Y)
    {
        public List<Pos> GetNeighbors(int width, int height)
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
                    var x = X + dx;
                    var y = Y + dy;
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        result.Add(new Pos(x, y));
                    }
                }
            }
            return result;
        }

        public List<Pos> AllNeighbors()
        {
            var result = new List<Pos>();
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    if (dx != 0 && dy != 0)
                    {
                        continue;
                    }
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }
                    var x = X + dx;
                    var y = Y + dy;
                        result.Add(new Pos(x, y));
                }
            }
            return result;
        }
    }
}
