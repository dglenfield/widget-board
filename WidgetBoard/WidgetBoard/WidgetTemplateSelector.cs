using WidgetBoard.ViewModels;

namespace WidgetBoard;

public class WidgetTemplateSelector : DataTemplateSelector
{
    private readonly WidgetFactory _widgetFactory;

    public WidgetTemplateSelector(WidgetFactory widgetFactory)
    {
        _widgetFactory = widgetFactory;
    }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is IWidgetViewModel widgetViewModel)
        {
            return new DataTemplate(() => _widgetFactory.CreateWidget(widgetViewModel));
        }

        return null;
    }
}
