using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

/// <summary>
/// The view model for the FixedBoardPage.
/// </summary>
public class FixedBoardPageViewModel : BaseViewModel, IQueryAttributable
{
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var board = query["Board"] as Board;
    }
}
