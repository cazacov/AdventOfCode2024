using System.Runtime.ExceptionServices;

namespace Day18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 18 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Day 18 Part 2: {Part2(input)}");
        }

        private static string Part1(List<Pos> bytes)
        {
            var width = 71;
            var height = 71;

            var corrupted = new HashSet<Pos>();
            corrupted.UnionWith(bytes.Take(1024));

            var free = new HashSet<Pos>();
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++)
                {
                    var pos = new Pos(x, y);
                    pos.Cost = int.MaxValue;
                    if (!corrupted.Contains(pos))
                    {
                        free.Add(pos);
                    }
                }
            }

            var start = free.FirstOrDefault(f => f.X == 0 && f.Y == 0);
            var finish = free.FirstOrDefault(f => f.X == width - 1 && f.Y == height - 1);
            start.Cost = 0;


            var visited = new HashSet<Pos>();
            var queue = new HashSet<Pos>();
            queue.Add(start);

            while (true)
            {
                var current = queue.OrderBy(q => q.Cost).First();
                visited.Add(current);
                queue.Remove(current);

                if ( current.Equals(finish))
                {
                    break;
                }

                foreach (var neighbor in current.Neigbors(free, width, height)) { 
                    if (visited.Contains(neighbor)) continue;
                    var newCost = current.Cost + 1;
                    if (newCost < neighbor.Cost)
                    {
                        neighbor.Cost = newCost;
                    }
                    queue.Add(neighbor);
                }

            }
            return finish.Cost.ToString();

        }

        private static string Part2(List<Pos> bytes)
        {
            var width = 71;
            var height = 71;
            int startCount = 1024;

            var corrupted = new HashSet<Pos>();
            corrupted.UnionWith(bytes.Take(startCount));

            var free = new HashSet<Pos>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pos = new Pos(x, y);
                    pos.Cost = int.MaxValue;
                    if (!corrupted.Contains(pos))
                    {
                        free.Add(pos);
                    }
                }
            }
            var start = free.FirstOrDefault(f => f.X == 0 && f.Y == 0);
            var finish = free.FirstOrDefault(f => f.X == width - 1 && f.Y == height - 1);
            start.Cost = 0;

            var index = startCount;
            while (index < bytes.Count)
            {
                var next = bytes[index];
                free.RemoveWhere(f => f.X == next.X && f.Y == next.Y);
                Console.WriteLine(free.Count);
                Reset(free);
                if (!PathExists(free, start, finish, width, height))
                {
                    return $"{next.X},{next.Y}";
                }
                index++;
            }
            return "ERROR";
        }

        private static bool PathExists(HashSet<Pos> free, Pos? start, Pos? finish, int width, int height)
        {
            var visited = new HashSet<Pos>();
            var queue = new HashSet<Pos>();
            queue.Add(start);

            while (true)
            {
                if (queue.Count == 0) return false;
                var current = queue.First();

                visited.Add(current);
                queue.Remove(current);

                if (current.Equals(finish))
                {
                    return true;
                }

                foreach (var neighbor in current.Neigbors(free, width, height))
                {
                    if (visited.Contains(neighbor)) continue;
                    if (queue.Contains(neighbor)) continue;
                    queue.Add(neighbor);
                }
            }
            return true;
        }

        private static void Reset(HashSet<Pos> free)
        {
            foreach (var pos in free)
            {
                if (pos.X != 0 && pos.Y != 0)
                {
                    pos.Cost = int.MaxValue;
                }
            }
        }

        private static List<Pos> ReadInput()
        {
            var result = new List<Pos>();
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                result.Add(new Pos(int.Parse(parts[0]), int.Parse(parts[1])));
            }
            return result;
        }
    }
}
