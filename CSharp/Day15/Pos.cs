using System.Diagnostics;

namespace Day15
{
    [DebuggerDisplay("{X}-{Y}")]
    public class Pos
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

        public int X { get; set; }
        public int Y { get; set; }

        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }


        public Pos NextInDir(int direction)
        {
            switch (direction)
            {
                case 0:
                    return new Pos(X, Y - 1);
                case 1:
                    return new Pos(X + 1, Y);
                case 2:
                    return new Pos(X, Y + 1);
                case 3:
                    return new Pos(X - 1, Y);
            }
            return this;
        }
    }
}
