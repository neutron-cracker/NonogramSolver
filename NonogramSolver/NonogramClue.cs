namespace NonogramSolver;

public class NonogramClue
{
    private ICollection<int> values;
    public NonogramClue(ICollection<int> values)
    {
        this.values = values;
        Sum = values.Sum();
    }
    
    public NonogramClue(params int[] values)
    {
        this.values = values;
    }

    public IEnumerable<int> Values => values;
    public int Sum { get; }
    public int BiggestQueue => values.Max();
}