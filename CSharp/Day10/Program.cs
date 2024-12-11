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
                var neighbours = current.GetNeighboursUp(map, width, height, current.Height);
                trail.AddRange(neighbours.Where(_ => !visited.Contains(_)));
            }

            var result = visited.Count(_ => _.Height == 9);
            return result;
        }

        private static string Part2(int[,] input)
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
                        result += Trailheadscore2(j, i, input, width, height);
                    }

                }
            }
            return result.ToString();
        }

        private static int Trailheadscore2(int x, int y, int[,] map, int width, int height)
        {
            List<Pos> trail = new List<Pos>() { new Pos(x, y, 0) };
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
                var neighbours = current.GetNeighboursUp(map, width, height, current.Height);
                trail.AddRange(neighbours
                    .Where(_ => !visited.Contains(_) && !trail.Contains(_)));
            }

            // Cleanup dead ends
            for (var level = 9; level > 0; level--)
            {
                var nodes = visited.Where(v => v.Height == level).ToList();
                var candidates = new HashSet<Pos>();
                foreach (var node in nodes)
                {
                    var neighbours = node.GetNeighboursDown(map, width, height, level);
                    candidates.UnionWith(neighbours);
                }
                visited.RemoveWhere(v => v.Height == level - 1 && !candidates.Contains(v));
            }

            // Track targets
            for (var level = 0; level <9; level++)
            {
                var nodes = visited.Where(v => v.Height == level).ToList();
                var candidates = new HashSet<Pos>();
                foreach (var node in nodes)
                {
                    var neighbours = node.GetNeighboursUp(map, width, height, level);
                    node.Targets = visited.Where(v => neighbours.Contains(v)).ToList();
                }
            }

            var source = visited.First(v => v.Height == 0);

            return CountTargets(source);
        }

        private static int CountTargets(Pos source)
        {
            if (source.Height == 9)
            {
                return 1;
            }

            var result = 0;
            foreach (var target in source.Targets)
            {
                result += CountTargets(target);
            }
            return result;
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
