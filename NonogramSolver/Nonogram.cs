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