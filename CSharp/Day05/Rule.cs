namespace Day05
{
    public record Rule(int Before, int After)
    {
        public bool Matches(List<int> update)
        {
            var idx1 = update.IndexOf(Before);
            var idx2 = update.IndexOf(After);
            if (idx1 >= 0 && idx2 >= 0) // both values are in the list
            {
                if (idx2 < idx1)    // but in the wrong order
                {
                    return false;
                }
            }
            return true;
        }

        public void ApplyFix(List<int> update)
        {
            var idx1 = update.IndexOf(Before);
            var idx2 = update.IndexOf(After);
            if (idx1 >= 0 && idx2 >= 0) // both values are in the list
            {
                if (idx2 < idx1) // but in the wrong order
                {
                    // swap them
                    var temp = update[idx1];
                    update[idx1] = update[idx2];
                    update[idx2] = temp;
                }
            }
        }
    }
}
