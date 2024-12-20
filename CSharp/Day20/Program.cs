namespace Day20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 20 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Day 20 Part 2: {Part2(input)}");
        }

        private static string Part1(Map map)
        {
            // Finds the shortest path from Start to End

            var cells = new HashSet<Pos>();
            cells.UnionWith(map.Free);

            var start = cells.FirstOrDefault(c => c.X == map.Start.X && c.Y == map.Start.Y);
            var end = cells.FirstOrDefault(c => c.X == map.End.X && c.Y == map.End.Y);

            foreach (var c in cells)
            {
                c.Cost = Int32.MaxValue;
                c.Cost2 = Int32.MaxValue;
            }

            start.Cost = 0;

            var queue = new HashSet<Pos>() { start };
            var visited = new HashSet<Pos>();

            while (true)
            {
                var current = queue.OrderBy(c => c.Cost).FirstOrDefault();
                if (current == null)
                {
                    break;
                }
                visited.Add(current);
                queue.Remove(current);

                foreach (Pos neighbor in current.Neighbors(cells, map.Width, map.Height))
                {
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }
                    if (neighbor.Cost > current.Cost + 1)
                    {
                        neighbor.Cost = current.Cost + 1;
                    }
                    queue.Add(neighbor);
                }
            }

            int N = end.Cost;

            end.Cost2 = 0;
            queue.Clear();
            queue.Add(end);
            visited.Clear();

            while (true)
            {
                var current = queue.OrderBy(c => c.Cost).FirstOrDefault();
                if (current == null)
                {
                    break;
                }
                visited.Add(current);
                queue.Remove(current);

                foreach (Pos neighbor in current.Neighbors(cells, map.Width, map.Height))
                {
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }
                    if (neighbor.Cost2 > current.Cost2 + 1)
                    {
                        neighbor.Cost2 = current.Cost2 + 1;
                    }
                    queue.Add(neighbor);
                }
            }

            int cheats = 0; 

            for (int y = 1; y < map.Height - 1; y++)
            {
                for (int x = 1; x < map.Width - 1; x++)
                {
                    cheats += IsCheat(new Pos(x, y), cells, map, N);
                }
            }

            return cheats.ToString();
        }

        private static int IsCheat(Pos p1, HashSet<Pos> cells, Map map, int N)
        {
            if (!map.Walls.Contains(p1))
            {
                return 0;
            }

            var n = p1.AllNeighbors(map.Width, map.Height);

            var c = cells.Where(c => n.Contains(c)).ToList();
            if (c.Count < 2)
            {
                return 0;
            }
            long cost1 = c.Min(cc => cc.Cost);
            long cost2 = c.Min(cc => cc.Cost2);

            var len = cost1 + cost2 + 2;
            var save = N - len;

            if (save >= 100)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private static string Part2(Map input)
        {
            return "Not implemented";
        }

        private static Map ReadInput()
        {
            return Map.ReadFromFile();
        }
    }
}
