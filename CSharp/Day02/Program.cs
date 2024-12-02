using static System.Int32;

namespace Day02
{
    /// <summary>
    /// Solution of Advent of Code 2024, Day 2 <see cref="https://adventofcode.com/2024/day/2"/>
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();

            Console.WriteLine($"Day 2: Part 1: {Part1(input)}");
            Console.WriteLine($"Day 2: Part 2: {Part2(input)}");
        }


        private static List<List<int>> ReadInput()
        {
            var result = new List<List<int>>();
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                result.Add(
                    line.Split(" ")
                        .Select(Parse)
                        .ToList()
                );
            }
            return result;
        }

        private static int Part1(List<List<int>> input)
        {
            return input.Count(IsSafeReport);
        }

        private static int Part2(List<List<int>> input)
        {
            return input.Count(IsProblemDampenerSafe);
        }

        private static bool IsSafeReport(List<int> levels)
        {
            var isIncreasing = true;
            var isDecreasing = true;
            var isDiffInRange = true;
            for (var i = 0; i < levels.Count - 1; i++)
            {
                if (levels[i] < levels[i + 1])
                {
                    isDecreasing = false;
                }
                if (levels[i] > levels[i + 1])
                {
                    isIncreasing = false;
                }
                if (levels[i] == levels[i + 1])
                {
                    isDiffInRange = false;
                }
                if (Math.Abs(levels[i] - levels[i + 1]) > 3)
                {
                    isDiffInRange = false;
                }
            }
            return (isIncreasing || isDecreasing) && isDiffInRange;
        }

        private static bool IsProblemDampenerSafe(List<int> levels)
        {
            if (IsSafeReport(levels))
            {
                return true;
            }

            for (var i = 0; i < levels.Count; i++)
            {
                var newLevels = new List<int>(levels);
                newLevels.RemoveAt(i);
                if (IsSafeReport(newLevels))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
