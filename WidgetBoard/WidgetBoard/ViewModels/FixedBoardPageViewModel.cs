using System.Collections.ObjectModel;
using System.Windows.Input;
using WidgetBoard.Data;
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
    private Board _board;
    
    private readonly IBoardRepository _boardRepository;
    private readonly WidgetFactory _widgetFactory;
    private readonly IPreferences _preferences;

    public FixedBoardPageViewModel(WidgetTemplateSelector widgetTemplateSelector, WidgetFactory widgetFactory, IBoardRepository boardRepository, IPreferences preferences)
    {
        WidgetTemplateSelector = widgetTemplateSelector;
        _widgetFactory = widgetFactory;
        _boardRepository = boardRepository;
        _preferences = preferences;

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
        var boardParameter = query["Board"] as Board;

        _board = _boardRepository.LoadBoard(boardParameter.Id);

        _preferences.Set("LastUsedBoardId", _board.Id);

        BoardName = _board.Name;
        NumberOfColumns = _board.NumberOfColumns;
        NumberOfRows = _board.NumberOfRows;

        foreach (var boardWidget in _board.BoardWidgets)
        {
            var widgetViewModel = _widgetFactory.CreateWidgetViewModel(boardWidget.WidgetType);
            widgetViewModel.Position = boardWidget.Position;

            Widgets.Add(widgetViewModel);
        }
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

        SaveWidget(widgetViewModel);

        IsAddingWidget = false;
    }

    /// <summary>
    /// Creates a new BoardWidget model class and saves it into the database.
    /// </summary>
    /// <param name="widgetViewModel"></param>
    private void SaveWidget(IWidgetViewModel widgetViewModel)
    {
        var boardWidget = new BoardWidget
        {
            BoardId = _board.Id,
            Position = widgetViewModel.Position,
            WidgetType = widgetViewModel.Type
        };

        _boardRepository.CreateBoardWidget(boardWidget);
    }
}
