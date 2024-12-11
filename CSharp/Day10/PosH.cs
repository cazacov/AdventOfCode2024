using System.Diagnostics;

namespace Day10
{
    [DebuggerDisplay("{X}-{Y}-{Height} -> {TargetCount}")]
    internal class Pos(int X, int Y, int Height)   
    {
        protected bool Equals(Pos other)
        {
            return X == other.X && Y == other.Y && Height == other.Height;
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
            return HashCode.Combine(X, Y, Height);
        }

        public List<Pos> GetNeighboursUp(int[,] map, int width, int height, int sourceHeight)
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

        public List<Pos> GetNeighboursDown(int[,] map, int width, int height, int sourceHeight)
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

                    if (map[Y + dy, X + dx] != sourceHeight - 1)
                    {
                        continue;
                    }
                    result.Add(new Pos(X + dx, Y + dy, Height - 1));
                }
            }
            return result;
        }

        public bool IsOnMap(int width, int height)
        {
            return X >= 0 && X < width && Y >= 0 && Y < height;
        }

        public List<Pos> Targets = new List<Pos>();
        public int X { get; init; } = X;
        public int Y { get; init; } = Y;
        public int Height { get; init; } = Height;

        public void Deconstruct(out int X, out int Y, out int Height)
        {
            X = this.X;
            Y = this.Y;
            Height = this.Height;
        }

        public int TargetCount => Targets.Count;
    }
}
