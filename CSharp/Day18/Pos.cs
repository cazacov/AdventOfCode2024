using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    internal class Pos
    {
        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }

        protected bool Equals(Pos other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Pos)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public int X;
        public int Y;
        public int Cost;

        public IEnumerable<Pos> Neigbors(HashSet<Pos> free, int width, int height)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;
                    if (dx != 0 && dy != 0) continue;
                    var x = X + dx;
                    var y = Y + dy;
                    if (x < 0 || x >= width || y < 0 || y >= height) continue;
                    var pos = free.FirstOrDefault(f => f.X == x && f.Y == y);
                    if (pos != null)
                    {
                        yield return pos;
                    }
                }
            }
        }
    }
}
