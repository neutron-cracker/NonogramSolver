using NonogramSolver;

namespace NonogramSolver.Test;

[TestFixture]
[TestOf(typeof(NonogramSolution))]
public class NonogramSolutionTest
{

    [Test]
    public void TestCreateEmpty()
    {
        // Arrange
        int width = 2;
        int height = 5;
        
        // Act
        NonogramSolution solution = NonogramSolution.CreateEmpty(width, height);
        var actualValue = solution.GetSolutionAtLocation(1, 4);
        
        // Assert
        Assert.That(actualValue, Is.EqualTo(SolutionValue.NotSolved));
    }
}