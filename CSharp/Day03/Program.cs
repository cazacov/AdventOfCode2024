using System.Text.RegularExpressions;

namespace Day03
{
    /// <summary>
    /// Solution of Advent of Code 2024, Day 3 <see cref="https://adventofcode.com/2024/day/3"/>
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 3: Part 1: {Part1(input)}");
            Console.WriteLine($"Day 3: Part 2: {Part2(input)}");
        }

        static List<string> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            return lines.ToList();
        }
        private static string Part1(List<string> input)
        {
            var multiplicationCommand = @"mul\((\d{1,3}),(\d{1,3})\)";

            long result = 0;
            foreach (var line in input)
            {
                var matches = Regex.Matches(line, multiplicationCommand);
                foreach (Match match in matches)
                {
                    var param1 = match.Groups[1].Value;
                    var param2 = match.Groups[2].Value;
                    result += long.Parse(param1) * long.Parse(param2);
                }
            }
            return result.ToString();
        }

        private static string Part2(List<string> input)
        {
            var multiplications = new List<(int, int)>();
            var dos = new List<int>();
            var donts = new List<int>();

            var mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
            var doPattern = @"do\(\)";
            var dontPattern = @"don't\(\)";
            
            var offset = 0;
            foreach (var line in input)
            {
                foreach (Match match in Regex.Matches(line, mulPattern))
                {
                    var index = match.Index;
                    var param1 = match.Groups[1].Value;
                    var param2 = match.Groups[2].Value;
                    multiplications.Add((index + offset, int.Parse(param1) * int.Parse(param2)));
                }

                foreach (Match match in Regex.Matches(line, doPattern))
                {
                    dos.Add(match.Index + offset);
                }

                foreach (Match match in Regex.Matches(line, dontPattern))
                {
                    donts.Add(match.Index + offset);
                }
                offset += line.Length;
            }

            long result = 0;
            foreach (var (index, product) in multiplications)
            {
                var lastDo = dos.LastOrDefault(x => x < index);
                var lastDont = donts.LastOrDefault(x => x < index);

                if (lastDo == 0 && lastDont == 0)
                {
                    // At the beginning of the program, mul instructions are enabled
                    result += product;
                }
                else if (lastDo > lastDont)
                {
                    result += product;
                }
            }
            return result.ToString();
        }
    }
}
