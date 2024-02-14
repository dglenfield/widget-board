namespace WidgetBoard.Controls;

/// <summary>
/// Placeholder control to show where a widget will be placed.
/// </summary>
public class Placeholder : Border
{
    public int Position { get; set; }

    public Placeholder()
    {
        Content = new Label
        {
            Text = "Tap to add widget",
            FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center, 
            VerticalOptions = LayoutOptions.Center
        };
    }
}
