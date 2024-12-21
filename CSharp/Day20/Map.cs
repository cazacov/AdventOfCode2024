namespace Day20
{
    internal class Map
    {
        public int Width;
        public int Height;
        public List<Pos> Walls = new List<Pos>();
        public Pos Start;
        public Pos End;
        public List<Pos> Free = new List<Pos>();

        public bool[,] IsWall;

        public static Map ReadFromFile()
        {
            var result = new Map();
            var lines = File.ReadAllLines("input.txt");

            result.Height = lines.Length;
            result.Width = lines[0].Length;

            result.IsWall = new bool[result.Height,result.Width];
            result.CostStart = new int[result.Height, result.Width];
            result.CostEnd = new int[result.Height, result.Width];

            for (var i = 0; i < result.Height; i++)
            {
                for (var j = 0; j < result.Width; j++)
                {
                    result.CostStart[i, j] = int.MaxValue;
                    result.CostEnd[i, j] = int.MaxValue;
                }
            }


            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    var c = lines[y][x];
                    if (c == '#')
                    {
                        result.Walls.Add(new Pos(x, y));
                        result.IsWall[y, x] = true;
                    }
                    else if (c == 'S')
                    {
                        result.Start = new Pos(x, y);
                        result.Free.Add(result.Start);
                        result.IsWall[y, x] = false;
                    }
                    else if (c == 'E')
                    {
                        result.End = new Pos(x, y);
                        result.Free.Add(result.End);
                        result.IsWall[y, x] = false;
                    }
                    else if (c == '.')
                    {
                        result.Free.Add(new Pos(x, y));
                        result.IsWall[y, x] = false;
                    }
                }
            }
            return result;
        }

        public int[,] CostEnd { get; set; }

        public int[,] CostStart { get; set; }
    }
}
