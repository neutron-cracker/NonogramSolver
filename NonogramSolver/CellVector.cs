namespace NonogramSolver;

public record CellVector(NonogramClue Clue, IList<SolutionValue> Solutions)
{
    private int size = Solutions.Count;
    public static IEnumerable<CellVector> GetAllPossibleVectors(NonogramClue clue, int size)
    {
        var firstVector = GetMostLeftSolutionVector(clue, size);
        if (clue.FilledArea == size)
            return [firstVector];

        HashSet<CellVector> vectors = new(EqualityComparer<CellVector>.Create((a, b) => a?.ToString() == b?.ToString(), vector => vector.GetHashCode()));
        Queue<CellVector> toProcessQueue = new();

        toProcessQueue.Enqueue(firstVector);
        vectors.Add(firstVector);

        while (toProcessQueue.Count != 0)
        {
            var cellVector = toProcessQueue.Dequeue();
            int currentIndex = size - 1;
            while (currentIndex > 0)
            {
                var cellToLeft = cellVector.Solutions[currentIndex - 1];
                SolutionValue? cellToRight = currentIndex == size - 1 ? null : cellVector.Solutions[currentIndex + 1];
                switch (cellToLeft, cellToRight)
                {
                    case (SolutionValue.Marked, SolutionValue.Marked):
                    case (SolutionValue.NotMarked, _):
                        currentIndex--;
                        continue;
                    // Marked cells can move to right.
                    case (SolutionValue.Marked, _):
                        var newVector = cellVector.MoveNotMarkedToLeft(ref currentIndex);
                        var wasAdded = vectors.Add(newVector);
                        if (wasAdded)
                            toProcessQueue.Enqueue(newVector);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        }

        return vectors;
    }

    public static CellVector GetMostLeftSolutionVector(NonogramClue clue, int size)
    {
        var solutions = new SolutionValue[size];
        int currentIndex = 0;
        
        using var enumerator = clue.Values.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var currentIncrease = enumerator.Current;
            Array.Fill(solutions, SolutionValue.Marked, currentIndex, currentIncrease);
            currentIndex += currentIncrease;

            // Only do this when there is an empty space at the end.
            if (currentIndex < size - 1)
            {
                solutions[currentIndex] = SolutionValue.NotMarked;
                currentIndex++;
            }

        }

        // Fill till the end.
        for (int i = currentIndex; i < size; i++)
        {
            solutions[i] = SolutionValue.NotMarked;
        }

        return new CellVector(clue, solutions);
    }
    
    
    /// <summary>
    /// Create x--x- from x-x--
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public CellVector MoveNotMarkedToLeft(ref int position)
    {
        var startingPosition = position;
        if (Solutions[position] != SolutionValue.NotMarked)
            throw new InvalidOperationException();

        position--;
        while (position >= 0 && Solutions[position] == SolutionValue.Marked)
        {
            position--;
        }

        position++;
        
        SolutionValue[] newSolutions = [..Solutions]; 
        newSolutions[startingPosition] = SolutionValue.Marked;
        newSolutions[position] = SolutionValue.NotMarked;
        
        return this with { Solutions = newSolutions };
    }

    public override string ToString()
    {
        var items = Solutions.Select(x => x switch
        {
            SolutionValue.Marked => 'M',
            SolutionValue.NotMarked => 'N',
            SolutionValue.NotSolved => ' ',
            _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
        });

        return string.Concat(items);
    }

    public override int GetHashCode()
    {
        var items = Solutions.Select((t, i) => Math.Pow(3, i) * (int)t);
        return (int)items.Sum();
    }
}