namespace Day01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var input = ReadInput();
            Console.WriteLine($"Day 1: Part 1: {Part1(input)}");
            Console.WriteLine($"Day 1: Part 2: {Part2(input)}");
        }

        private static Tuple<List<int>, List<int>> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");
            var result = new Tuple<List<int>, List<int>>(new List<int>(), new List<int>());
            foreach (var line in lines)
            {
                var l = line.Replace("   ", " ");
                var parts = l.Split(' ');
                result.Item1.Add(int.Parse(parts[0]));
                result.Item2.Add(int.Parse(parts[1]));
            }
            return result;
        }

        private static long Part1(Tuple<List<int>, List<int>> input)
        {
            var firstList = new List<int>();
            firstList.AddRange(input.Item1);
            var secondList = new List<int>();
            secondList.AddRange(input.Item2);
            
            firstList.Sort();
            secondList.Sort();

            return 
                firstList
                    .Select((item, i) => Math.Abs(item - secondList[i]))
                    .Sum();
        }

        private static long Part2(Tuple<List<int>, List<int>> input)
        {
            var firstList = new List<int>();
            firstList.AddRange(input.Item1);
            var secondList = new List<int>();
            secondList.AddRange(input.Item2);
            
            long result = 0;
            foreach (var item in firstList)
            {
                var cnt = secondList.Count(x => x == item);
                result += item * cnt;
            }
            return result;
        }
    }
}
