using WidgetBoard.ViewModels;

namespace WidgetBoard.Views;

/// <summary>
/// An interface to represent all widget views.
/// </summary>
public interface IWidgetView
{
    int Position
    {
        get => WidgetViewModel.Position;
        set => WidgetViewModel.Position = value;
    }

    IWidgetViewModel WidgetViewModel { get; set; }
}
