using Day06;

namespace Day08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 8 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Day 8 Part 2: {Part2(input)}");
        }

        private static string Part1(char[,] map)
        {
            var height = map.GetUpperBound(0) + 1;
            var width = map.GetUpperBound(1) + 1;
            
            var antennas = ExtractAntennas(map, width, height);
            var antinodes = FindAntinodes(antennas, width, height);
            var unique = antinodes.DistinctBy(a => a.PosKey).ToList();

            Display(map, unique, width, height);

            return unique.Count().ToString();
        }
        
        private static string Part2(char[,] map)
        {
            var height = map.GetUpperBound(0) + 1;
            var width = map.GetUpperBound(1) + 1;

            var antennas = ExtractAntennas(map, width, height);
            var antinodes = FindAntinodes2(antennas, width, height);
            var unique = antinodes.DistinctBy(a => a.PosKey).ToList();

            Display(map, unique, width, height);

            return unique.Count().ToString();
        }
        
        private static void Display(char[,] map, List<Antenna> antinodes, int width, int height)
        {
            var color = Console.ForegroundColor;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (map[y, x] != '.')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(map[y,x]);
                    }
                    else
                    {
                        var antenne = antinodes.FirstOrDefault(a => a.X == x && a.Y == y);
                        if (antenne != null)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("#");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(".");
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = color;
        }
        

        private static List<Antenna> FindAntinodes(List<Antenna> antennas, int width, int height) {
        
            var result = new List<Antenna>();
            
            var frequencies = antennas.Select(a => a.Frequency).Distinct();
            foreach (var frequency in frequencies)
            {
                var antennasWithFrequency = antennas.Where(a => a.Frequency == frequency).ToList();
                // Iterate through all pairs
                for (var i = 0; i < antennasWithFrequency.Count - 1; i++)
                {
                    for (var j = i + 1; j < antennasWithFrequency.Count; j++)
                    {
                        var first = antennasWithFrequency[i];
                        var second = antennasWithFrequency[j];
                        var dx = second.X - first.X;
                        var dy = second.Y - first.Y;
                        result.Add(new Antenna(first.X - dx, first.Y - dy, frequency));
                        result.Add(new Antenna(second.X + dx, second.Y + dy, frequency));
                    }
                }
            }
            return result.Where(x => !x.IsOutOfMap(width, height)).ToList();
        }

        private static List<Antenna> FindAntinodes2(List<Antenna> antennas, int width, int height)
        {
            var result = new List<Antenna>();
            var frequencies = antennas.Select(a => a.Frequency).Distinct();

            foreach (var frequency in frequencies)
            {
                var antennasWithFrequency = antennas.Where(a => a.Frequency == frequency).ToList();
                for (var i = 0; i < antennasWithFrequency.Count - 1; i++)
                {
                    for (var j = i + 1; j < antennasWithFrequency.Count; j++)
                    {
                        var first = antennasWithFrequency[i];
                        var second = antennasWithFrequency[j];
                        var dx = second.X - first.X;
                        var dy = second.Y - first.Y;

                        // Find smallest step
                        var gcd = Gcd(dx, dy);
                        dx /= gcd;
                        dy /= gcd;

                        var range = Math.Max(width, height);
                        for (var k = -range; k < range; k++)
                        {
                            var c = new Antenna(first.X + dx * k, first.Y + dy * k, frequency);
                            if (!c.IsOutOfMap(width, height))
                            {
                                result.Add(c);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static int Gcd(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a == 0)
            {
                return b;
            }
            if (b == 0)
            {
                return a;
            }

            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }
            return a + b;
        }

        private static List<Antenna> ExtractAntennas(char[,] map, int width, int height)
        {
            var result = new List<Antenna>();
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (map[y, x] != '.')
                    {
                        var frequency = map[y, x];
                        result.Add(new Antenna(x, y, frequency));
                    }
                }
            }
            return result;
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
