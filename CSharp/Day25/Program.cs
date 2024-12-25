namespace Day25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            var locks= input.Item1;
            var keys = input.Item2;
            Console.WriteLine($"Day 25 Part 1: {Part1(locks, keys)}");
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Day 25 Part 2: {Part2(locks, keys)}");
        }

        private static string Part1(List<List<int>> locks, List<List<int>> keys)
        {
            int matches = 0;

            foreach (var llock in locks)
            {
                foreach (var key in keys)
                {
                    if (DoMatch(llock, key))
                    {
                        matches++;
                    }
                }
            }
            return matches.ToString();
        }

        private static bool DoMatch(List<int> llock, List<int> key)
        {
            for (int i = 0; i < llock.Count; i++)
            {
                if (llock[i] + key[i] > 5)
                {
                    return false;
                }
            }
            return true;
        }

        private static string Part2(List<List<int>> locks, List<List<int>> keys)
        {
            return "";
        }

        private static Tuple<List<List<int>>, List<List<int>>> ReadInput()
        {
            var locks = new List<List<int>>();
            var keys = new List<List<int>>();

            void processBlock(List<string> block)
            {
                var key = new List<int>();
                for (var x = 0; x < block[0].Length; x++)
                {
                    key.Add(-1);
                    for (var y = 0; y < block.Count; y++)
                    {
                        if (block[y][x] == '#')
                        {
                            key[x] += 1;
                        }
                    }
                }

                if (block[0].StartsWith("#"))
                {
                    locks.Add(key);
                }
                else
                {
                    keys.Add(key);
                }
            }

            var lines = File.ReadAllLines("input.txt");
            var current = new List<string>();
            foreach (var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    processBlock(current);
                    current.Clear();
                }
                else
                {
                    current.Add(line);
                }
            }
            processBlock(current);
            return new Tuple<List<List<int>>, List<List<int>>>(locks, keys);
        }
    }
}
