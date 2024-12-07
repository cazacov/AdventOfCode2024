namespace Day06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 6: Part 1: {Part1(input)}");
            input = ReadInput();
            Console.WriteLine($"Day 6: Part 2: {Part2(input)}");
        }

        private static string Part1(char[,] map)
        {
            var height = map.GetUpperBound(0)+1;
            var width = map.GetUpperBound(1)+1;

            var guard = LocateGuard(map, height, width);
            var visited = new HashSet<Position> { guard };

            while (true)
            {
                var nextPos = guard.Go();
                if (nextPos.IsOutOfMap(width, height))
                {
                    break;
                }
                if (map[nextPos.Y, nextPos.X] == '#')
                {
                    guard.Turn();
                }
                else
                {
                    guard = nextPos;
                    visited.Add(guard);
                }
            }
            return visited.Count.ToString();
        }


        private static string Part2(char[,] map)
        {
            var height = map.GetUpperBound(0) + 1;
            var width = map.GetUpperBound(1) + 1;

            var guard = LocateGuard(map, height, width);

            var initPos = new Guard(guard);
            var visited = new Dictionary<Position, List<int>>();

            IsALoop(guard, map, null, width, height, visited);
            var loopCandidates = new HashSet<Position>(visited.Keys);
            loopCandidates.Remove(initPos);

            var loopCount = 0;
            Parallel.ForEach(loopCandidates, candidate =>
            {
                var path = new Dictionary<Position, List<int>>();
                guard = new Guard(initPos);
                if (IsALoop(guard, map, candidate, width, height, path))
                {
                    loopCount++;
                }
            });
            return loopCount.ToString();
        }

        private static Guard LocateGuard(char[,] map, int height, int width)
        {
            var x = 0;
            var y = 0;
            var dir = 0;
            var found = false;

            // locate guard
            for (var i = 0; i < height && !found; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (map[i, j] != '#' && map[i, j] != '.')
                    {
                        x = j;
                        y = i;
                        found = true;
                        dir = map[i, j] switch
                        {
                            '^' => 0,
                            '>' => 1,
                            'v' => 2,
                            '<' => 3,
                            _ => dir
                        };
                        break;
                    }
                }
            }
            map[y, x] = '.';

            var guard = new Guard(x, y, dir);
            return guard;
        }

        private static bool IsALoop(Guard guard, char[,] map,  Position? extraObstruction, int width, int height, Dictionary<Position, List<int>> visited)
        {
            visited[guard] = new List<int> { guard.Dir };
            while (true)
            {
                var nextPos = guard.Go();
                if (nextPos.IsOutOfMap(width, height))
                {
                    return false;
                }
                if (map[nextPos.Y, nextPos.X] == '#' || (extraObstruction != null && nextPos.Equals(extraObstruction)))
                {
                    guard.Turn();
                }
                else
                {
                    guard = nextPos;
                }
                if (visited.ContainsKey(guard))
                {
                    if (visited[guard].Contains(guard.Dir))
                    {
                        return true;
                    }
                    else
                    {
                        visited[guard].Add(guard.Dir);
                    }
                }
                else
                {
                    visited[guard] = new List<int> { guard.Dir };
                }
            }
        }

        private static char[,] ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var result = new char[lines.Length, lines[0].Length];
            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines[0].Length; j++)
                {
                    result[i, j] = lines[i][j];
                }
            }
            return result;
        }
    }
}
