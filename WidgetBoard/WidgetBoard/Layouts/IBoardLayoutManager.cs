namespace WidgetBoard.Layouts;

/// <summary>
/// Defines how the BoardLayout will interact with a layout manager implementation.
/// </summary>
public interface IBoardLayoutManager
{
    object BindingContext { get; set; }
    BoardLayout Board { get; set; }
    void SetPosition(BindableObject bindableObject, int position);
}
