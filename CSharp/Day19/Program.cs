namespace Day19
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 18 Part 1: {Part1(input.Item1, input.Item2)}");
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Day 18 Part 2: {Part2(input.Item1, input.Item2)}");
        }

        public static Dictionary<string, long> cache = new Dictionary<string, long>();

        private static string Part1(List<string> towels, List<string> designs)
        {
            return  designs.Where(d => IsPossible(d, towels)).Count().ToString();
        }

        private static bool IsPossible(string design, List<string> towels)
        {
            if (design.Length == 0)
            {
                return true;
            }
            foreach (var towel in towels)
            {
                if (design.StartsWith(towel))
                {
                    if (IsPossible(design.Substring(towel.Length), towels))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static string Part2(List<string> towels, List<string> designs)
        {
            return designs.Sum(d => CountPossibilities(d, towels)).ToString();
        }

        private static long CountPossibilities(string design, List<string> towels)
        {
            if (design.Length == 0)
            {
                return 1;
            }

            if (cache.ContainsKey(design))
            {
                return cache[design];
            }

            var cnt = 0L;
            foreach (var towel in towels)
            {
                if (design.StartsWith(towel))
                {
                    cnt+=CountPossibilities(design.Substring(towel.Length), towels);
                }
            }
            cache[design] = cnt;
            return cnt;
        }

        private static Tuple<List<string>, List<string>> ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");

            var towels = new List<string>();
            var designs = new List<string>();    
            bool isTowel = true;
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    isTowel = false;
                    continue;
                }

                if (isTowel)
                {
                    towels.AddRange(lines[i].Split(", ", StringSplitOptions.RemoveEmptyEntries));
                }
                else
                {
                    designs.Add(lines[i]);
                }
            }
            return new Tuple<List<string>, List<string>>(towels, designs);
        }
    }
}
