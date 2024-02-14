using System.Windows.Input;
using WidgetBoard.Controls;

namespace WidgetBoard.Layouts;

public class FixedLayoutManager : BindableObject, IBoardLayoutManager
{
    public static readonly BindableProperty NumberOfColumnsProperty = BindableProperty.Create(
        nameof(NumberOfColumns), typeof(int), typeof(FixedLayoutManager), defaultBindingMode: BindingMode.OneWay, propertyChanged: OnNumberOfColumnsChanged);

    public static readonly BindableProperty NumberOfRowsProperty = BindableProperty.Create(
        nameof(NumberOfRows), typeof(int), typeof(FixedLayoutManager), defaultBindingMode: BindingMode.OneWay, propertyChanged: OnNumberOfRowsChanged);

    public static readonly BindableProperty PlaceholderTappedCommandProperty = BindableProperty.Create(
        nameof(PlaceholderTappedCommand), typeof(ICommand), typeof(FixedLayoutManager));

    private BoardLayout _board;
    public BoardLayout Board
    {
        get => _board;
        set
        {
            _board = value;
            InitializeGrid();
        }
    }

    public int NumberOfColumns
    {
        get => (int)GetValue(NumberOfColumnsProperty);
        set => SetValue(NumberOfColumnsProperty, value);
    }

    public int NumberOfRows
    {
        get => (int)GetValue(NumberOfRowsProperty);
        set => SetValue(NumberOfRowsProperty, value);
    }

    public ICommand PlaceholderTappedCommand
    {
        get => (ICommand)GetValue(PlaceholderTappedCommandProperty);
        set => SetValue(PlaceholderTappedCommandProperty, value);
    }

    private bool _isInitialized;

    public void SetPosition(BindableObject bindableObject, int position)
    {
        if (NumberOfColumns == 0)
        {
            return;
        }

        int column = position % NumberOfColumns;
        int row = position / NumberOfColumns;

        Grid.SetColumn(bindableObject, column);
        Grid.SetRow(bindableObject, row);

        var placeholder = Board.Placeholders.Where(p => p.Position == position).FirstOrDefault();
        if (placeholder is not null)
        {
            Board.RemovePlaceholder(placeholder);
        }
    }

    static void OnNumberOfColumnsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var manager = (FixedLayoutManager)bindable;
        manager.InitializeGrid();
    }

    static void OnNumberOfRowsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var manager = (FixedLayoutManager)bindable;
        manager.InitializeGrid();
    }

    private void InitializeGrid()
    {
        if (Board is null || NumberOfColumns == 0 || NumberOfRows == 0 || _isInitialized == true)
        {
            return;
        }

        _isInitialized = true;

        for (int i = 0; i < NumberOfColumns; i++)
        {
            Board.AddColumn(new ColumnDefinition(new GridLength(1, GridUnitType.Star)));
        }

        for (int i = 0; i < NumberOfRows; i++)
        {
            Board.AddRow(new RowDefinition(new GridLength(1, GridUnitType.Star)));
        }

        for (int column = 0; column < NumberOfColumns; column++)
        {
            for (int row = 0; row < NumberOfRows; row++)
            {
                var placeholder = new Placeholder();
                placeholder.Position = row * NumberOfColumns + column;
                
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                placeholder.GestureRecognizers.Add(tapGestureRecognizer);

                Board.AddPlaceholder(placeholder);

                Grid.SetColumn(placeholder, column);
                Grid.SetRow(placeholder, row);
            }
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (sender is Placeholder placeholder)
        {
            if (PlaceholderTappedCommand?.CanExecute(placeholder.Position) == true)
            {
                PlaceholderTappedCommand.Execute(placeholder.Position);
            }
        }
    }
}
