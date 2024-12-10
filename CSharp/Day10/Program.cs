namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 10 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Day 10 Part 2: {Part2(input)}");
        }

        private static string Part1(int[,] input)
        {
            var height = input.GetUpperBound(0) + 1;
            var width = input.GetUpperBound(1) + 1;

            int result = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (input[i, j] == 0)
                    {
                        result += Trailheadscore(j, i, input, width, height);
                    }

                }
            }
            return result.ToString();
        }

        private static int Trailheadscore(int x, int y, int[,] map, int width, int height)
        {
            List<Pos> trail = new List<Pos>() { new Pos(x, y, 0)};
            HashSet<Pos> visited = new HashSet<Pos>();


            while (trail.Count > 0)
            {
                var current = trail[0];
                trail.RemoveAt(0);

                if (visited.Contains(current))
                {
                    continue;
                }
                visited.Add(current);
                var neighbours = current.GetNeighbours(map, width, height, current.Height);
                trail.AddRange(neighbours.Where(_ => !visited.Contains(_)));
            }

            var result = visited.Count(_ => _.Height == 9);
            /*
            for (int yy = 0; yy < height; yy++)
            {
                for (int xx = 0; xx < width; xx++)
                {
                    if (visited.Contains(new Pos(xx,yy, map[yy,xx])))
                    {
                        Console.Write(map[yy,xx]);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            */
            return result;
        }

        private static string Part2(int[,] input)
        {
            return "Not implemented";
        }

        private static int[,] ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var result = new int[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    if (lines[i][j] != '.')
                        result[i, j] = lines[i][j] - '0';
                    else
                    {
                        result[i, j] = -1;
                    }
                }
            }
            return result;
        }
    }
}
