namespace WidgetBoard.Models;

/// <summary>
/// Fixed layout option.
/// </summary>
public class FixedLayout : BaseLayout
{
    public int NumberOfColumns { get; init; }
    public int NumberOfRows { get; init; }
}
