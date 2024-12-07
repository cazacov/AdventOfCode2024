namespace Day06
{
    internal class Guard : Position
    {
        public Guard(int x, int y, int dir) : base(x, y)
        {
            Dir = dir;
        }

        public Guard(Guard other) : this(other.X, other.Y, other.Dir)
        {
        }

        public int Dir { get; private set; }

        public Guard Go()
        {
            return Dir switch
            {
                0 => new Guard(X, Y - 1, Dir),
                1 => new Guard(X + 1, Y, Dir),
                2 => new Guard(X, Y + 1, Dir),
                3 => new Guard(X - 1, Y, Dir),
                _ => throw new InvalidOperationException()
            };
        }

        public void Turn()
        {
            Dir = (Dir + 1) % 4;
        }
    }
}
