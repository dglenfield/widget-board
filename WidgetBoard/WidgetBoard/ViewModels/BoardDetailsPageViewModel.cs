using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

/// <summary>
/// The view model for the BoardDetailsPage.
/// </summary>
public class BoardDetailsPageViewModel : BaseViewModel
{
    private string _boardName;
    public string BoardName
    {
        get => _boardName;
        set 
        { 
            SetProperty(ref _boardName, value);
            SaveCommand.ChangeCanExecute();
        }
    }

    private bool _isFixed = true;
    public bool IsFixed
    {
        get => _isFixed;
        set => SetProperty(ref _isFixed, value);
    }

    private int _numberOfColumns = 3;
    public int NumberOfColumns
    {
        get => _numberOfColumns; 
        set => SetProperty(ref _numberOfColumns, value);
    }

    private int _numberOfRows = 2;
    public int NumberOfRows
    {
        get => _numberOfRows; 
        set => SetProperty(ref _numberOfRows, value);
    }

    public Command SaveCommand { get; }

    public BoardDetailsPageViewModel()
    {
        SaveCommand = new Command(
            () => Save(),
            () => !string.IsNullOrWhiteSpace(BoardName));
    }

    private async void Save()
    {
        System.Diagnostics.Debug.WriteLine("Save button clicked");

        var board = new Board
        {
            Name = BoardName, 
            Layout = new FixedLayout
            {
                NumberOfColumns = NumberOfColumns,
                NumberOfRows = NumberOfRows
            }
        };

        await Shell.Current.GoToAsync(
            "fixedboard",
            new Dictionary<string, object>
            {
                { "Board", board}
            });
    }
}
