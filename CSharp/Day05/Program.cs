namespace Day05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();
            Console.WriteLine($"Day 5: Part 1: {Part1(input)}");
            Console.WriteLine($"Day 5: Part 2: {Part2(input)}");
        }

        private static Manual ReadInput()
        {
            var lines = File.ReadAllLines("input.txt");

            var result = new Manual([], []);
            
            bool isRulesSection = true;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    isRulesSection = false;
                    continue;
                }

                if (isRulesSection)
                {
                    var parts = line.Split("|");
                    result.Rules.Add(new Rule(int.Parse(parts.First()), int.Parse(parts.Last())));
                }
                else
                {
                    var pages = line.Split(",").Select(int.Parse).ToList();
                    result.Updates.Add(pages);
                }
            }
            return result;
        }

        private static string Part1(Manual manual)
        {
            var result = 0;
            foreach (var update in manual.Updates)
            {
                if (IsValidUpdate(update, manual.Rules))
                {
                    result += update[update.Count / 2];
                }
            }
            return result.ToString();
        }

        private static string Part2(Manual manual)
        {
            var result = 0;
            foreach (var update in manual.Updates)
            {
                if (!IsValidUpdate(update, manual.Rules))
                {
                    var correctedList = Reorder(update, manual.Rules);
                    result += correctedList[correctedList.Count / 2];
                }
            }
            return result.ToString();
        }

        private static bool IsValidUpdate(List<int> update, List<Rule> rules)
        {
            return rules.All(rule => rule.Matches(update));
        }


        private static List<int> Reorder(List<int> update, List<Rule> rules)
        {
            var toBeFixed = true;
            while (toBeFixed)
            {
                toBeFixed = false;
                foreach (var rule in rules)
                {
                    if (!rule.Matches(update))
                    {
                        rule.ApplyFix(update);
                        toBeFixed = true;
                    }
                }
            }
            return update;
        }
    }
}
