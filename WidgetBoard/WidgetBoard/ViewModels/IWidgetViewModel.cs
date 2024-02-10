namespace WidgetBoard.ViewModels;

/// <summary>
/// An interface to represent all widget view models.
/// </summary>
public interface IWidgetViewModel
{
    int Position { get; set; }
    string Type { get; }
}
