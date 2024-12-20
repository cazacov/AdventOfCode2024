using System.Diagnostics;

namespace Day20
{
    [DebuggerDisplay("{X},{Y}  {Cost}")]
    internal class Pos
    {
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

        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IEnumerable<Pos> Neighbors(HashSet<Pos> cells, int mapWidth, int mapHeight)
        {
            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    if (Math.Abs(dx) + Math.Abs(dy) != 1)
                    {
                        continue;
                    }
                    var x = X + dx;
                    var y = Y + dy;
                    if (x <= 0 || x >= mapWidth - 1 || y <= 0 || y >= mapHeight - 1)
                    {
                        continue;
                    }
                    var c = cells.FirstOrDefault(c => c.X == x && c.Y == y);
                    if (c != null)
                    {
                        yield return c;
                    }
                }
            }
        }
    }
}
