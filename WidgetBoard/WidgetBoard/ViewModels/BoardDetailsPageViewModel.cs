using WidgetBoard.Data;
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

    private readonly IBoardRepository _boardRepository;
    private readonly ISemanticScreenReader _semanticScreenReader;

    public BoardDetailsPageViewModel(ISemanticScreenReader semanticScreenReader, IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
        _semanticScreenReader = semanticScreenReader;

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
            NumberOfColumns = NumberOfColumns,
            NumberOfRows = NumberOfRows
        };

        _boardRepository.CreateBoard(board);

        _semanticScreenReader.Announce($"A new board with the name {BoardName} was created successfully.");

        await Shell.Current.GoToAsync(
            "fixedboard",
            new Dictionary<string, object>
            {
                { "Board", board}
            });
    }
}
