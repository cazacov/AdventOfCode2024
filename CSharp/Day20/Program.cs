namespace Day20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 20 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            input = ReadInput();
            Console.WriteLine($"Day 20 Part 2: {Part2(input)}");
        }

        private static string Part1(Map map)
        {

            var N = CalculateDistances(map);

            int cheats = 0; 

            for (int y = 1; y < map.Height - 1; y++)
            {
                for (int x = 1; x < map.Width - 1; x++)
                {
                    cheats += IsCheat(x, y, map, N);
                }
            }

            return cheats.ToString();
        }

        private static string Part2(Map map)
        {
            var N = CalculateDistances(map);
            int cheats = 0;


            for (int y = 1; y < map.Height - 1; y++)
            {
                for (int x = 1; x < map.Width - 1; x++)
                {
                    for (int yy = 1; yy < map.Height - 1; yy++)
                    {
                        for (int xx = 1; xx < map.Width - 1; xx++)
                        {
                            cheats += IsCheat2(x, y, xx, yy, map, N, 20);
                        }
                    }
                }
            }
            return cheats.ToString();
        }




        private static int CalculateDistances(Map map)
        {
            map.CostStart[map.Start.Y, map.Start.X] = 0;

            var queue = new HashSet<Pos>() { map.Start };
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

                foreach (var neighbor in current.AllNeighbors(map.Width, map.Height))
                {
                    if (map.IsWall[neighbor.Y, neighbor.X])
                    {
                        continue;
                    }

                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }

                    var nextCost = map.CostStart[current.Y, current.X] + 1;
                    if (map.CostStart[neighbor.Y, neighbor.X] > nextCost)
                    {
                        map.CostStart[neighbor.Y, neighbor.X] = nextCost;
                    }
                    queue.Add(neighbor);
                }
            }

            int N = map.CostStart[map.End.Y, map.End.X];

            map.CostEnd[map.End.Y, map.End.X] = 0;
            queue.Clear();
            queue.Add(map.End);
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

                foreach (Pos neighbor in current.AllNeighbors(map.Width, map.Height))
                {
                    if (map.IsWall[neighbor.Y, neighbor.X])
                    {
                        continue;
                    }
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }
                    var nextCost = map.CostEnd[current.Y, current.X] + 1;
                    if (map.CostEnd[neighbor.Y, neighbor.X] > nextCost)
                    {
                        map.CostEnd[neighbor.Y, neighbor.X] = nextCost;
                    }
                    queue.Add(neighbor);
                }
            }

            return N;
        }

        private static int IsCheat(int x, int y, Map map, int N)
        {
            if (!map.IsWall[y,x])
            {
                return 0;
            }

            long toStart = Int32.MaxValue;
            long toEnd = Int32.MaxValue;

            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    if (Math.Abs(dx) + Math.Abs(dy) != 1)
                    {
                        continue;
                    }

                    var xx = x + dx;
                    var yy = y + dy;

                    if (!map.IsWall[yy, xx])
                    {
                        toStart = Math.Min(toStart, map.CostStart[yy, xx]);
                        toEnd = Math.Min(toEnd, map.CostEnd[yy, xx]);
                    }
                }
            }
            long len = toStart + toEnd + 2;
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

        private static int IsCheat2(int x1, int y1, int x2, int y2, Map map, int N, int cheatLength)
        {
            if (map.IsWall[y1, x1])
            {
                return 0;
            }

            if (map.IsWall[y2, x2])
            {
                return 0;
            }


            var len = Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
            if (len > cheatLength)
            {
                return 0;
            }

            long toStart = map.CostStart[y1, x1];
            long toEnd = map.CostEnd[y2, x2];
/*
            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    if (Math.Abs(dx) + Math.Abs(dy) != 1)
                    {
                        continue;
                    }

                    var xxx = x1 + dx;
                    var yyy = y1 + dy;
                    if (!map.IsWall[yyy, xxx])
                    {
                        toStart = Math.Min(toStart, map.CostStart[yyy, xxx]);
                    }
                }
            }

            toEnd = Math.Min(toEnd, map.CostEnd[y2, x2]);
*/


            long pathLen = toStart + toEnd + len;
            var save = N - pathLen;

            if (save >= 100)
            { 
//                if (save == 76)
//                {
//                    Console.WriteLine($"76: {x1},{y1} {toStart}  ->  {x2},{y2} {toEnd}    {len} / {save} ");
//                }
                return 1;
            }
            else
            {
                return 0;
            }

        }



        private static Map ReadInput()
        {
            return Map.ReadFromFile();
        }
    }
}
