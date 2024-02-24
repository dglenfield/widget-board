using SQLite;

namespace WidgetBoard.Models;

/// <summary>
/// Represents the overall board.
/// </summary>
public class Board
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; init; }
    public int NumberOfColumns { get; init; }
    public int NumberOfRows { get; init; }
    [Ignore]
    public IList<BoardWidget> BoardWidgets { get; set; }
}
