using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBoard.Models;

namespace WidgetBoard.Data;

public class LiteDBBoardRepository : IBoardRepository
{
    private readonly LiteDatabase _database;
    private readonly ILiteCollection<Board> _boardCollection;
    private readonly ILiteCollection<BoardWidget> _boardWidgetCollection;

    public void CreateBoard(Board board)
    {
        _boardCollection.Insert(board);
    }

    public void CreateBoardWidget(BoardWidget boardWidget)
    {
        throw new NotImplementedException();
    }

    public void DeleteBoard(Board board)
    {
        _boardCollection.Delete(board.Id);
    }

    public IReadOnlyList<Board> ListBoards()
    {
        return _boardCollection.Query().OrderBy(b => b.Name).ToList();
    }

    public Board LoadBoard(int boardId)
    {
        var board = _boardCollection.FindById(boardId);
        var boardWidgets = _boardWidgetCollection.Find(w => w.BoardId == boardId).ToList();

        board.BoardWidgets = boardWidgets;

        return board;
    }

    public void UpdateBoard(Board board)
    {
        _boardCollection.Update(board);
    }

    public LiteDBBoardRepository(IFileSystem fileSystem)
    {
        var dbPath = Path.Combine(fileSystem.AppDataDirectory, "widgetboard_litedb.db");
        _database = new LiteDatabase(dbPath);

        _boardCollection = _database.GetCollection<Board>("Boards");
        _boardWidgetCollection = _database.GetCollection<BoardWidget>("BoardWidgets");

        _boardCollection.EnsureIndex(b => b.Id, true);
        _boardCollection.EnsureIndex(b => b.Name, false);
    }
}
