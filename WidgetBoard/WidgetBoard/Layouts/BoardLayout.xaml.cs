using System.Collections;
using WidgetBoard.Controls;
using WidgetBoard.Views;

namespace WidgetBoard.Layouts;

/// <summary>
/// The parent for widgets.
/// </summary>
public partial class BoardLayout 
{
    public static readonly BindableProperty ItemTemplateSelectorProperty =
        BindableProperty.Create(nameof(ItemTemplateSelector), typeof(DataTemplateSelector), typeof(BoardLayout));

    public static readonly BindableProperty ItemsSourceProperty = 
		BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(BoardLayout));

    private IBoardLayoutManager _boardLayoutManager;
	public IBoardLayoutManager BoardLayoutManager
	{
		get => _boardLayoutManager;
		set
		{
			_boardLayoutManager = value;
			_boardLayoutManager.Board = this;
		}
	}

	public IEnumerable ItemsSource
	{
		get => (IEnumerable)GetValue(ItemsSourceProperty);
		set => SetValue(ItemsSourceProperty, value);
	}	

	public DataTemplateSelector ItemTemplateSelector
	{
		get => (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty);
		set => SetValue(ItemTemplateSelectorProperty, value);
	}

	/// <summary>
	/// Provides all children from the PlaceholderGrid that are of type Placeholder.
	/// </summary>
	public IReadOnlyList<Placeholder> Placeholders => PlaceholderGrid.Children.OfType<Placeholder>().ToList();

    public BoardLayout()
	{
		InitializeComponent();
	}

	/// <summary>
	/// Defines the the board's columns on both the PlaceholderGrid and WidgetGrid.
	/// </summary>
	/// <param name="columnDefinition"></param>
	public void AddColumn(ColumnDefinition columnDefinition)
	{
		PlaceholderGrid.ColumnDefinitions.Add(columnDefinition);
		WidgetGrid.ColumnDefinitions.Add(columnDefinition);
	}

	/// <summary>
	/// Adds a placeholder to the PlaceholderGrid.
	/// </summary>
	/// <param name="placeholder"></param>
	public void AddPlaceholder(Placeholder placeholder) => PlaceholderGrid.Children.Add(placeholder);

    /// <summary>
    /// Defines the the board's rows on both the PlaceholderGrid and WidgetGrid.
    /// </summary>
    /// <param name="rowDefinition"></param>
    public void AddRow(RowDefinition rowDefinition)
	{
		PlaceholderGrid.RowDefinitions.Add(rowDefinition);
		WidgetGrid.RowDefinitions.Add(rowDefinition);
	}

	/// <summary>
	/// Removes a placeholder from the PlaceholderGrid. 
	/// </summary>
	/// <param name="placeholder"></param>
	public void RemovePlaceholder(Placeholder placeholder) => PlaceholderGrid.Children.Remove(placeholder);

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

		_boardLayoutManager.BindingContext = this.BindingContext;
    }

	private void Widgets_ChildAdded(object sender, ElementEventArgs e)
	{
		if (e.Element is IWidgetView widgetView)
		{
			BoardLayoutManager.SetPosition(e.Element, widgetView.Position);
		}
	}
}
