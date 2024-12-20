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

        public static Map ReadFromFile()
        {
            var result = new Map();
            var lines = File.ReadAllLines("input.txt");

            result.Height = lines.Length;
            result.Width = lines[0].Length;

            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    var c = lines[y][x];
                    if (c == '#')
                    {
                        result.Walls.Add(new Pos(x, y));
                    }
                    else if (c == 'S')
                    {
                        result.Start = new Pos(x, y);
                        result.Free.Add(result.Start);
                    }
                    else if (c == 'E')
                    {
                        result.End = new Pos(x, y);
                        result.Free.Add(result.End);
                    }
                    else if (c == '.')
                    {
                        result.Free.Add(new Pos(x, y));
                    }
                }
            }
            return result;
        }
    }
}
