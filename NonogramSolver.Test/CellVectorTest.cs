using NonogramSolver;

namespace NonogramSolver.Test;

[TestFixture]
[TestOf(typeof(CellVector))]
public class CellVectorTest
{

    [Test]
    public void GetMostLeftVector_EmptyEnd()
    {
        // Arrange
        // X-X-XX----
        int size = 10;
        NonogramClue clue = new(1, 1, 2);
        List<SolutionValue> expected =
        [
            SolutionValue.Marked,
            SolutionValue.NotMarked,
            SolutionValue.Marked,
            SolutionValue.NotMarked,
            SolutionValue.Marked,
            SolutionValue.Marked,
            SolutionValue.NotMarked,
            SolutionValue.NotMarked,
            SolutionValue.NotMarked,
            SolutionValue.NotMarked
        ];
        
        // Act
        var result = CellVector.GetMostLeftSolutionVector(clue, size);
        
        // Assert
        Assert.That(result.Solutions, Is.EqualTo(expected));
    }
    
    [Test]
    public void GetMostLeftVector_TotallyFilled()
    {
        // Arrange
        // X-X-XX----
        int size = 6;
        NonogramClue clue = new(1, 1, 2);
        List<SolutionValue> expected =
        [
            SolutionValue.Marked,
            SolutionValue.NotMarked,
            SolutionValue.Marked,
            SolutionValue.NotMarked,
            SolutionValue.Marked,
            SolutionValue.Marked,
        ];
        
        // Act
        var result = CellVector.GetMostLeftSolutionVector(clue, size);
        
        // Assert
        Assert.That(result.Solutions, Is.EqualTo(expected));
    }

    [Test]
    public void GetAllPossibleVectors()
    {
        // Arrange
        // X-X-
        int size = 5;
        NonogramClue clue = new(1, 1);
        List<List<SolutionValue>> expectedSolutions = 
        [
            [
                SolutionValue.Marked,
                SolutionValue.NotMarked,
                SolutionValue.Marked,
                SolutionValue.NotMarked,
                SolutionValue.NotMarked
            ],
            [
                SolutionValue.Marked,
                SolutionValue.NotMarked,
                SolutionValue.NotMarked,
                SolutionValue.Marked,
                SolutionValue.NotMarked
            ],
            [
                SolutionValue.Marked,
                SolutionValue.NotMarked,
                SolutionValue.NotMarked,
                SolutionValue.NotMarked,
                SolutionValue.Marked
            ],
            [
                SolutionValue.NotMarked,
                SolutionValue.Marked,
                SolutionValue.NotMarked,
                SolutionValue.Marked,
                SolutionValue.NotMarked
            ],
            [
                SolutionValue.NotMarked,
                SolutionValue.Marked,
                SolutionValue.NotMarked,
                SolutionValue.NotMarked,
                SolutionValue.Marked
            ],
            [
                SolutionValue.NotMarked,
                SolutionValue.NotMarked,
                SolutionValue.Marked,
                SolutionValue.NotMarked,
                SolutionValue.Marked
            ]
        ];
        var expected = expectedSolutions.Select(x => new CellVector(clue, x));
        
        // Act
        var result = CellVector.GetAllPossibleVectors(clue, size);
        
        // Assert
        Assert.That(result, Is.EquivalentTo(expected).Using((CellVector actual, CellVector expected1) => actual.ToString() == expected1.ToString()));
    }

    [Test]
    public void TestMoveToLeft()
    {
        // Arrange
        NonogramClue clue = new(1, 1);
        List<SolutionValue> solutions =
        [
            SolutionValue.Marked,
            SolutionValue.NotMarked,
            SolutionValue.Marked,
            SolutionValue.NotMarked,
        ];
        
        List<SolutionValue> expectedSolutions =
        [
            SolutionValue.Marked,
            SolutionValue.NotMarked,
            SolutionValue.NotMarked,
            SolutionValue.Marked,
        ];
        
        CellVector cellVector = new(clue, solutions);
        int position = 3;
        
        // Act
        var actual = cellVector.MoveNotMarkedToLeft(ref position);
        
        // Assert
        Assert.That(actual.Solutions, Is.EqualTo(expectedSolutions));
        Assert.That(position, Is.EqualTo(2));
    }

    [Test]
    public void TestMoveToLeftAtLeft()
    {
        NonogramClue clue = new(1);
        List<SolutionValue> solutions =
        [
            SolutionValue.Marked,
            SolutionValue.NotMarked,
        ];
        
        List<SolutionValue> expectedSolutions =
        [
            SolutionValue.NotMarked,
            SolutionValue.Marked,
        ];
        
        CellVector cellVector = new(clue, solutions);
        int position = 1;
        
        // Act
        var actual = cellVector.MoveNotMarkedToLeft(ref position);
        
        // Assert
        Assert.That(actual.Solutions, Is.EqualTo(expectedSolutions));
        Assert.That(position, Is.EqualTo(0));
    }

    [Test]
    public void TestHashCode()
    {
        // Arrange
        CellVector vector = new(null, [SolutionValue.NotSolved, SolutionValue.Marked, SolutionValue.NotMarked]);
        var expected = 1 * 0 + 3 * 1 + 9 * 2; 
        
        // Act
        var actual = vector.GetHashCode();
        
        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}