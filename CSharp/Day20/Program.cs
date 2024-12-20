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
            }

            start.Cost = 0;

            var queue = new HashSet<Pos>() { start };
            var visited = new HashSet<Pos>();

            while (true)
            {
                var current = queue.OrderBy(c => c.Cost).FirstOrDefault();
                visited.Add(current);
                queue.Remove(current);

                if (current.X == end.X && current.Y == end.Y)
                {
                    break;
                }

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

            var maxPathLength = end.Cost;

            return "Not implemented";
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
