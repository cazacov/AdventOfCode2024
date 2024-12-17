namespace Day17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 17 Part 1: {Part1(input)}");
            Console.WriteLine("\n\n\n");
            Console.WriteLine($"Day 17 Part 2: {Part2(input)}");
        }

        private static string Part1(Computer computer)
        {
            computer.RunToEnd();
            return string.Join(",", computer.Output);
        }

        private static string Part2(Computer computer)
        {
            var areg = FindRecursive(computer, 0L, computer.Instructions.Count - 1);
           
            computer.Reset(areg);
            computer.RunToEnd();

            // Confirm the output is correct
            if (computer.Output.Count == computer.Instructions.Count)
            {
                for (var i = 0; i < computer.Output.Count; i++)
                {
                    if (computer.Output[i] != computer.Instructions[i])
                    {
                        throw new Exception("Part 2 has errors");
                    }
                }
            }
            else
            {
                throw new Exception("Part 2 has errors");
            }
                
            return areg.ToString();
        }

        private static long FindRecursive(Computer computer, long leftPart, int level)
        {
            if (level < 0)
            {
                return leftPart;
            }
            
            var expectedOutput = computer.Instructions[level];
            for (var digit = 0; digit < 8; digit++)
            {
                var candidateInput = leftPart * 8L + digit;
                computer.Reset(candidateInput);
                var output = computer.RunToFirstOut();
                if (output == expectedOutput)
                {
                    var result = FindRecursive(computer, leftPart * 8 + digit, level - 1);
                    if (result > 0)
                    {
                        return result;
                    }
                }
            }
            // That should never happen
            return -1;
        }

        private static Computer ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");

            var computer = new Computer
            {
                RegisterA = long.Parse(lines[0][12..]),
                RegisterB = long.Parse(lines[1][12..]),
                RegisterC = long.Parse(lines[2][12..])
            };

            var program = lines[4][9..];
            computer.Instructions = program.Split(",").Select(int.Parse).ToList();
            return computer;
        }
    }
}
