using System.Collections.ObjectModel;
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

    public AppShellViewModel() 
    {
        Boards.Add(new Board
        {
            Name = "My first board",
            Layout = new FixedLayout
            {
                NumberOfColumns = 3,
                NumberOfRows = 2
            }
        });
    }

    private async void BoardSelected(Board board)
    {
        await Shell.Current.GoToAsync("fixedboard", new Dictionary<string, object> { { "Board", board } });
    }
}
