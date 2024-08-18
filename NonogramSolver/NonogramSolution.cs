namespace NonogramSolver;

public class NonogramSolution
{
    /// <summary>
    /// The solutionValues indexed by column, row
    /// </summary>
    private SolutionValue[][] solutionValues;

    private NonogramSolution(SolutionValue[][] solutionValues)
    {
        this.solutionValues = solutionValues;
    }

    public static NonogramSolution CreateEmpty(int width, int height)
    {
        SolutionValue[][] mainArray = new SolutionValue[width][];
        for (var i = 0; i < mainArray.Length; i++)
        {
            var newArray = new SolutionValue[height];
            Array.Fill(newArray, SolutionValue.NotSolved);
            mainArray[i] = newArray;
        }

        return new NonogramSolution(mainArray);
    }

    public SolutionValue GetSolutionAtLocation(int width, int height) => solutionValues[width][height];
}