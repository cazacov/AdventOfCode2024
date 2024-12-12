using System.Diagnostics;

namespace Day12
{
    [DebuggerDisplay("{X1}-{X2}  {Y1}-{Y2}  {Dir}")]
    internal class Fence
    {
        protected bool Equals(Fence other)
        {
            return X1 == other.X1 && Y1 == other.Y1 && Dir == other.Dir;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Fence)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X1, Y1);
        }

        public int X1;
        public int X2;
        public int Y1;
        public int Y2;
        public int Dir;
    }
}
