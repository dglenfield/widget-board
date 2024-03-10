using WidgetBoard.ViewModels;

namespace WidgetBoard.Views;

public partial class SketchWidgetView : GraphicsView, IWidgetView, IDrawable
{
	private DrawingPath _currentPath;
	private readonly IList<DrawingPath> _paths = new List<DrawingPath>();

	public IWidgetViewModel WidgetViewModel
	{
		get => BindingContext as IWidgetViewModel;
		set => BindingContext = value;
	}

	public SketchWidgetView()
	{
		InitializeComponent();

		this.Drawable = this;
	}

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        foreach (var path in _paths)
		{
			canvas.StrokeColor = path.Color;
			canvas.StrokeSize = path.Thickness;
			canvas.StrokeLineCap = LineCap.Round;
			canvas.DrawPath(path.Path);
		}
    }

    private void GraphicsView_DragInteraction(object sender, TouchEventArgs e)
    {
        _currentPath.Add(e.Touches.First());

		Invalidate();
    }

    private void GraphicsView_EndInteraction(object sender, TouchEventArgs e)
    {
        _currentPath.Add(e.Touches.First());

        Invalidate();
    }

    private void GraphicsView_StartInteraction(object sender, TouchEventArgs e) 
	{   
		AppTheme currentTheme = Application.Current.RequestedTheme;
		if (currentTheme == AppTheme.Dark)
		{
            _currentPath = new DrawingPath(Colors.White, 2);
        }
		else
		{
            _currentPath = new DrawingPath(Colors.Black, 2);
        }

        _currentPath.Add(e.Touches.First());
        _paths.Add(_currentPath);

        Invalidate();
	}
}
