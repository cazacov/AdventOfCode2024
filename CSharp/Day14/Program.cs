using System.Text.RegularExpressions;

namespace Day14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 14 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            input = ReadInput();
            Console.WriteLine($"Day 14 Part 2: {Part2(input)}");                          
        }

        private static string Part1(List<Robot> robots)
        {
            var width = 101;
            var height = 103;


            var steps = 100;

            var quadrants = new List<long>() { 0, 0, 0, 0 };
            foreach (var robot in robots)
            {
                var dx = (width + robot.Speed.X) % width;
                var dy = (height + robot.Speed.Y) % height;

                var x = (robot.Position.X + dx * steps) % width;
                var y = (robot.Position.Y + dy * steps) % height;

                if (x == width / 2) 
                {
                    continue;
                }

                if (y == height / 2)
                {
                    continue;
                }
                var qx = (x < width / 2) ? 0 : 1;
                var qy = (y < height / 2) ? 0 : 1;
                quadrants[qy * 2 + qx] += 1;
            }
            return (quadrants[0] * quadrants[1] * quadrants[2] * quadrants[3]).ToString();
        }


        private static string Part2(List<Robot> robots)
        {
            var width = 101;
            var height = 103;

            var step = 1;

            while (true) {
                var end = new List<Pos>();
                foreach (var robot in robots)
                {
                    var dx = (width + robot.Speed.X) % width;
                    var dy = (height + robot.Speed.Y) % height;

                    var x = (robot.Position.X + dx * step) % width;
                    var y = (robot.Position.Y + dy * step) % height;

                    end.Add(new Pos(x, y));
                }

                // Check if no robots share the same position
                var group = end.GroupBy(x => x).ToDictionary(x => x.Key, g => g.ToList());
                if (group.All(p => p.Value.Count == 1))
                {
                    DisplayTree(height, width, end, step);
                    break;
                }
                else
                {
                    step++;
                }
            }
            return step.ToString();
        }

        private static void DisplayTree(int height, int width, List<Pos> end, int s)
        {
            var lines = new List<string>();
            for (var y = 0; y < height; y++)
            {
                var line = "";
                for (int x = 0; x < width; x++)
                {
                    var c = end.Count(e => e.X == x && e.Y == y);
                    if (c == 0)
                    {
                        line += " ";
                        continue;
                    }
                    line += c.ToString();
                }
                lines.Add(line);
            }

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        private static List<Robot> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var robots = new List<Robot>();
            foreach (var line in lines)
            {
                var match = Regex.Match(line, @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");
                if (match.Success)
                {
                    var pos = new Pos(long.Parse(match.Groups[1].Value), long.Parse(match.Groups[2].Value));
                    var speed = new Pos(long.Parse(match.Groups[3].Value), long.Parse(match.Groups[4].Value));
                    robots.Add(new Robot(pos, speed));
                }
            }
            return robots;
        }
    }
}
