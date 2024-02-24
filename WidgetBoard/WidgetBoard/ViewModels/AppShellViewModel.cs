using System.Collections.ObjectModel;
using WidgetBoard.Data;
using WidgetBoard.Models;

namespace WidgetBoard.ViewModels;

/// <summary>
/// The view model for the AppShell.
/// </summary>
public class AppShellViewModel : BaseViewModel
{
    public ObservableCollection<Board> Boards { get; } = new ObservableCollection<Board>();

    private Board _currentBoard;
    public Board CurrentBoard
    {
        get => _currentBoard;
        set
        {
            if (SetProperty(ref _currentBoard, value))
            {
                BoardSelected(value);
            }
        }
    }

    private readonly IBoardRepository _boardRepository;
    private readonly IPreferences _preferences;

    public AppShellViewModel(IBoardRepository boardRepository, IPreferences preferences) 
    {
        _boardRepository = boardRepository;
        _preferences = preferences;
        //Boards.Add(new Board
        //{
        //    Name = "My first board",
        //    Layout = new FixedLayout
        //    {
        //        NumberOfColumns = 3,
        //        NumberOfRows = 2
        //    }
        //});
    }

    public void LoadBoards()
    {
        var boards = _boardRepository.ListBoards();

        var lastUsedBoardId = _preferences.Get("LastUsedBoardId", -1);
        Board lastUsedBoard = null;

        foreach (var board in boards)
        {
            Boards.Add(board);

            if (lastUsedBoardId == board.Id)
            {
                lastUsedBoard = board;
            }
        }

        if (lastUsedBoard is not null)
        {
            Dispatcher.GetForCurrentThread().Dispatch(() =>
            {
                BoardSelected(lastUsedBoard);
            });
        }
    }

    private async void BoardSelected(Board board)
    {
        await Shell.Current.GoToAsync("fixedboard", new Dictionary<string, object> { { "Board", board } });
    }
}
