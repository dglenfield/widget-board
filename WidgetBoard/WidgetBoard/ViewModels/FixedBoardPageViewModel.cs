using System.Collections.ObjectModel;
using System.Windows.Input;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

/// <summary>
/// The view model for the FixedBoardPage.
/// </summary>
public class FixedBoardPageViewModel : BaseViewModel, IQueryAttributable
{
    public ICommand AddWidgetCommand { get; }
    public ICommand AddNewWidgetCommand { get; }
    public IList<string> AvailableWidgets => _widgetFactory.AvailableWidgets;

    private string _boardName;
    public string BoardName
    {
        get => _boardName;
        set => SetProperty(ref _boardName, value);
    }

    private bool _isAddingWidget;
    public bool IsAddingWidget
    {
        get => _isAddingWidget;
        set => SetProperty(ref _isAddingWidget, value);
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

    private string _selectedWidget;
    public string SelectedWidget
    {
        get => _selectedWidget;
        set => SetProperty(ref _selectedWidget, value);
    }

    public WidgetTemplateSelector WidgetTemplateSelector { get; }
    public ObservableCollection<IWidgetViewModel> Widgets { get; }

    private int _addingPosition;

    private readonly WidgetFactory _widgetFactory;

    public FixedBoardPageViewModel(WidgetTemplateSelector widgetTemplateSelector, WidgetFactory widgetFactory)
    {
        WidgetTemplateSelector = widgetTemplateSelector;
        _widgetFactory = widgetFactory;

        Widgets = new ObservableCollection<IWidgetViewModel>();

        AddWidgetCommand = new Command(OnAddWidget);

        AddNewWidgetCommand = new Command<int>(index =>
        {
            IsAddingWidget = true;
            _addingPosition = index;
        });
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var board = query["Board"] as Board;

        BoardName = board.Name;
        NumberOfColumns = ((FixedLayout)board.Layout).NumberOfColumns;
        NumberOfRows = ((FixedLayout)board.Layout).NumberOfRows;
    }

    private void OnAddWidget()
    {
        if (SelectedWidget is null)
        {
            return;
        }

        var widgetViewModel = _widgetFactory.CreateWidgetViewModel(SelectedWidget);
        widgetViewModel.Position = _addingPosition;

        Widgets.Add(widgetViewModel);

        IsAddingWidget = false;
    }
}
