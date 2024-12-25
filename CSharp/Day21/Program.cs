namespace Day21
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 21 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Day 21 Part 2: {Part2(input)}");
        }

        private static string Part1(List<string> input)
        {
            return "not implemented";
        }

        private static string Part2(List<string> input)
        {
            return "not implemented";
        }

        private static List<string> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            return lines.ToList();
        }
    }
}
