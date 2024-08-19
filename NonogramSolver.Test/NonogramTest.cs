using NonogramSolver;

namespace NonogramSolver.Test;

[TestFixture]
[TestOf(typeof(Nonogram))]
public class NonogramTest
{

    [Test]
    public void IntegrationTestSolve()
    {
        // Act
        var importText = File.ReadAllText("exampleImport.json");
        NonogramImporter importer = new();
        var nonogram = importer.Create(importText);
        var solution = nonogram.Solve();

        // Assert
        Assert.That(solution.GetSolutionAtLocation(0, 7), Is.EqualTo(SolutionValue.Marked));
        Assert.That(solution.GetSolutionAtLocation(9, 4), Is.EqualTo(SolutionValue.NotMarked));
        Assert.That(solution.GetSolutionAtLocation(9, 5), Is.EqualTo(SolutionValue.NotMarked));
        Assert.That(solution.GetSolutionAtLocation(9, 7), Is.EqualTo(SolutionValue.Marked));
    }
}