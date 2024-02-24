using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBoard.Models;

namespace WidgetBoard.Data;

public class SqliteBoardRepository : IBoardRepository
{
    private readonly SQLiteConnection _connection;

    public void CreateBoard(Board board)
    {
        _connection.Insert(board);
    }

    public void CreateBoardWidget(BoardWidget boardWidget)
    {
        _connection.Insert(boardWidget);
    }

    public void DeleteBoard(Board board)
    {
        _connection.Delete(board);
    }

    public IReadOnlyList<Board> ListBoards()
    {
        return _connection.Table<Board>().OrderBy(b => b.Name).ToList();
    }

    public Board LoadBoard(int boardId)
    {
        var board = _connection.Find<Board>(boardId);
        if (board is null)
        {
            return null;
        }

        var widgets = _connection.Table<BoardWidget>().Where(w => w.BoardId == boardId).ToList();
        board.BoardWidgets = widgets;

        return board;
    }

    public void UpdateBoard(Board board)
    {
        _connection.Update(board);
    }

    public SqliteBoardRepository(IFileSystem fileSystem)
    {
        System.Diagnostics.Debug.WriteLine(fileSystem.AppDataDirectory);
        var dbPath = Path.Combine(fileSystem.AppDataDirectory, "widgetboard_sqlite.db");

        _connection = new SQLiteConnection(dbPath);
        _connection.CreateTable<Board>();
        _connection.CreateTable<BoardWidget>();
    }
}
