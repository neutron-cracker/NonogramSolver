using System.Text.Json;
using NonogramSolver;

namespace NonogramSolver.Test;

[TestFixture]
[TestOf(typeof(NonogramImporter))]
public class NonogramImporterTest
{

    [Test]
    public void TestCreation()
    {
        NonogramImporter.NonogramImport aa = new([[1, 2]], [[2]]);
        LocationCues expectedQues = new(new(1, 2), new(2));
        Nonogram nn = new([expectedQues.Row], [expectedQues.Column]);
        string ddd = JsonSerializer.Serialize(aa);
        NonogramImporter.NonogramImport bb = JsonSerializer.Deserialize<NonogramImporter.NonogramImport>(ddd);
        
        // Arrange
        var importText = File.ReadAllText("exampleImport.json");
        NonogramImporter importer = new();
        
        // Act
        var nonogram = importer.Create(importText);
        var locationQues = nonogram.GetCluesAtLocation(0, 0);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(locationQues.Column.Values, Is.EquivalentTo(expectedQues.Column.Values));
            Assert.That(locationQues.Row.Values, Is.EquivalentTo(expectedQues.Row.Values));
        });
    }
}