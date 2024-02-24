using SQLite;

namespace WidgetBoard.Models;

/// <summary>
/// Represents each widget that is placed on the board and where it is positioned.
/// </summary>
public class BoardWidget
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int BoardId { get; set; }
    public int Position { get; set; }
    public string WidgetType { get; set; }
}
