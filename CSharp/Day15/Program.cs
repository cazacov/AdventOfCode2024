using System.Data;

namespace Day15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            //Console.WriteLine($"Day 15 Part 1: {Part1(input.Item1, input.Item2)}");
            Console.WriteLine("\n\n\n");
            input = ReadInput();
            Console.WriteLine($"Day 15 Part 2: {Part2(input.Item1, input.Item2)}");
        }

        private static string Part1(Map map, List<int> directions)
        {

            map.Display();
            var robot = map.Start;
            foreach (int direction in directions)
            {
//                map.DisplayDirection(direction);
                var nextPos = robot.NextInDir(direction);
                if (!map.Walls.Contains(nextPos) && !map.Containers.Contains(nextPos))
                {
                    robot = nextPos;
                    Console.WriteLine("move");
                }
                else if (map.Walls.Contains(nextPos))
                {
                    Console.WriteLine("block");
                }
                else
                {
                    var container = map.Containers.First(c => c.Equals(nextPos));
                    if (TryMoveContainer(container, direction, map))
                    {
                        robot = nextPos;
                        Console.WriteLine("move");
                    }
                    else
                    {
                        Console.WriteLine("block");
                    }
                }
                map.Start = robot;
//                map.Display();
//                Console.WriteLine();
//                Console.ReadLine();
            }

            int score = 0;
            foreach (var container in map.Containers)
            {
                score += container.Y * 100 + container.X;
            }
            return score.ToString();
        }

        private static bool TryMoveContainer(Pos container, int direction, Map map)
        {
            var nextPos = container.NextInDir(direction);
            if (!map.Walls.Contains(nextPos) && !map.Containers.Contains(nextPos))
            {
                map.Containers.Remove(container);
                map.Containers.Add(nextPos);
                return true;
            }
            if (map.Walls.Contains(nextPos))
            {
                return false;
            }
            var nextContainer = map.Containers.First(c => c.Equals(nextPos));
            var success = TryMoveContainer(nextContainer, direction, map);
            if (success)
            {
                map.Containers.Remove(container);
                map.Containers.Add(nextPos);
            }
            return success;
        }

        private static string Part2(Map map, List<int> directions)
        {
            map.Upscale();
            Console.WriteLine("Initial state");
            map.Display2();
            Console.WriteLine();
            var robot = map.Start;
            for (int i = 0; i < directions.Count; i++) {
                var direction = directions[i];
//                map.DisplayDirection(direction, i);
                var nextPos = robot.NextInDir(direction);
                var nextWall = map.Walls.FirstOrDefault(w => w.Equals(nextPos));
                var nextContainer = map.ContainerInPos(nextPos);

                if (nextWall == null && nextContainer == null)
                {
                    robot = nextPos;
//                    Console.WriteLine("move");
                }
                else if (nextWall != null)
                {
//                    Console.WriteLine("block");
                }
                else
                {
                    var backup = new List<Pos>(map.Containers);
                    if (TryMoveContainer2(nextContainer, direction, map))
                    {
                        robot = nextPos;
//                        Console.WriteLine("move");
                    }
                    else
                    {
//                        Console.WriteLine("block");
                        map.Containers.Clear();
                        map.Containers.UnionWith(backup);
                    }
                }
                map.Start = robot;
//                map.Display2();
//                Console.WriteLine();
//                Console.ReadLine();
            }

            Console.WriteLine();
            Console.WriteLine("Final state");
            map.Display2();

            int score = 0;
            foreach (var container in map.Containers)
            {
                score += container.Y * 100 + container.X;
            }
            return score.ToString();
        }

        private static bool TryMoveContainer2(Pos container, int direction, Map map)
        {
            List<Pos> edges = new List<Pos>();
            switch (direction)
            {
                case 0:
                case 2:
                    edges.Add(container.NextInDir(direction));
                    edges.Add(new Pos(container.X + 1, container.Y).NextInDir(direction));
                    break;
                case 1:
                    edges.Add(new Pos(container.X + 1, container.Y).NextInDir(direction));
                    break;
                case 3:
                    edges.Add(container.NextInDir(direction));
                    break;
            }


            if (edges.Any(e => map.Walls.Contains(e)))
            {
                return false;
            }
            if (edges.All(e => map.ContainerInPos(e) == null))
            {
                map.Containers.Remove(container);
                map.Containers.Add(container.NextInDir(direction));
                return true;
            }

            var toPush = edges.Select(e => map.ContainerInPos(e)).Where(x => x != null).Distinct().ToList();
            var success = toPush.All(c => TryMoveContainer2(c, direction, map));
            if (success)
            {
                map.Containers.Remove(container);
                map.Containers.Add(container.NextInDir(direction));
            }
            return success;
        }

        private static Tuple<Map, List<int>> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var map = new Map();

            int index = 0;
            for (var y = 0; y < lines.Length; y++)
            {
                if (String.IsNullOrWhiteSpace(lines[y]))
                {
                    index = y;
                    break;
                }
                for (var x = 0; x < lines[y].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '#':
                            map.Walls.Add(new Pos(x, y));
                            break;
                        case 'O':
                            map.Containers.Add(new Pos(x, y));
                            break;
                        case '@':
                            map.Start = new Pos(x, y);
                            break;
                    }
                }
            }
            map.Init();

            var directions = new List<int>();
            for (var i = index + 1; i < lines.Length; i++)
            {
                for (var d = 0; d < lines[i].Length; d++)
                {
                    switch (lines[i][d])
                    {
                        case '^':
                            directions.Add(0);
                            break;
                        case '>':
                            directions.Add(1);
                            break;
                        case 'v':
                            directions.Add(2);
                            break;
                        case '<':
                            directions.Add(3);
                            break;
                    }
                }
            }
            return new Tuple<Map, List<int>>(map, directions);
        }
    }
}
