using System.Collections.ObjectModel;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

/// <summary>
/// The view model for the FixedBoardPage.
/// </summary>
public class FixedBoardPageViewModel : BaseViewModel, IQueryAttributable
{
    private string _boardName;
    public string BoardName
    {
        get => _boardName;
        set => SetProperty(ref _boardName, value);
    }

    private int _numberOfColumns;
    public int NumberOfColumns
    {
        get => _numberOfColumns;
        set => SetProperty(ref _numberOfColumns, value);
    }

    private int _numberOfRows;
    public int NumberOfRows
    {
        get => _numberOfRows; 
        set => SetProperty(ref _numberOfRows, value);
    }

    public WidgetTemplateSelector WidgetTemplateSelector { get; }
    public ObservableCollection<IWidgetViewModel> Widgets { get; }

    public FixedBoardPageViewModel(WidgetTemplateSelector widgetTemplateSelector)
    {
        WidgetTemplateSelector = widgetTemplateSelector;
        Widgets = new ObservableCollection<IWidgetViewModel>();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var board = query["Board"] as Board;

        BoardName = board.Name;
        NumberOfColumns = ((FixedLayout)board.Layout).NumberOfColumns;
        NumberOfRows = ((FixedLayout)board.Layout).NumberOfRows;
    }
}
