using System.Text.Json;

namespace NonogramSolver;

public class NonogramImporter
{
    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public Nonogram Create(string json)
    {
        var deserialized = JsonSerializer.Deserialize<NonogramImport>(json, jsonSerializerOptions);
        if (deserialized is null) throw new InvalidOperationException("json is not valid");

        return new Nonogram(
            rows: deserialized.Rows.Select(x => new NonogramClue(x)).ToList(),
            columns: deserialized.Columns.Select(x => new NonogramClue(x)).ToList());
    }

    public record NonogramImport(int[][] Rows, int[][] Columns);
}