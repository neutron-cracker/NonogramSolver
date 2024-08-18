namespace NonogramSolver;

public class Nonogram(IList<NonogramClue> columns, IList<NonogramClue> rows)
{
    public LocationCues GetCluesAtLocation(int columnNumber, int rowNumber) => 
        new(Column: columns[columnNumber], Row: rows[rowNumber]);

    public NonogramSolution Solve()
    {
        throw new NotImplementedException();
    }
}

public record LocationCues(NonogramClue Column, NonogramClue Row);

/**
Ways to solve:
- If next blocked location in either direction is equal or less than number to fill. 

*/
public class NonogramSolveStrategy
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="que"></param>
    /// <param name="solution"></param>
    /// <param name="cellNumber">This should be in range from 1 to n</param>
    /// <returns></returns>
    public static SolutionValue Solve(NonogramClue que, List<SolutionValue> solution, int cellNumber)
    {
        int size = solution.Count;
        if (que.BiggestQueue * 2 < size)
            return SolutionValue.NotSolved;

        int distanceToOtherSide = size > cellNumber ? size - cellNumber : cellNumber;
        return que.BiggestQueue > distanceToOtherSide ? SolutionValue.White : SolutionValue.NotSolved;
    }
}

