namespace WidgetBoard;

public class DrawingPath
{
    public Color Color { get; }
    public PathF Path { get; }
    public float Thickness { get; }

    public DrawingPath(Color color, float thickness)
    {
        Color = color;
        Thickness = thickness;
        Path = new PathF();
    }

    public void Add(PointF point) => Path.LineTo(point);
}
