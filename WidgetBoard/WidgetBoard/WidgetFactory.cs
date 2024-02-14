using WidgetBoard.ViewModels;
using WidgetBoard.Views;

namespace WidgetBoard;

public class WidgetFactory
{
    public IList<string> AvailableWidgets => _widgetNameRegistrations.Keys.ToList();

    private static IDictionary<Type, Type> _widgetRegistrations = new Dictionary<Type, Type>();
    private static IDictionary<string, Type> _widgetNameRegistrations = new Dictionary<string, Type>();

    private readonly IServiceProvider _serviceProvider;

    public WidgetFactory(IServiceProvider serviceProvider) 
    {
        _serviceProvider = serviceProvider;
    }

    public IWidgetView CreateWidget(IWidgetViewModel widgetViewModel)
    {
        if (_widgetRegistrations.TryGetValue(widgetViewModel.GetType(), out var widgetViewType))
        {
            var widgetView = (IWidgetView)_serviceProvider.GetRequiredService(widgetViewType);
            widgetView.WidgetViewModel = widgetViewModel;
            return widgetView;
        }

        return null;
    }

    public IWidgetViewModel CreateWidgetViewModel(string displayName)
    {
        if (_widgetNameRegistrations.TryGetValue(displayName, out var widgetViewModelType))
        {
            return (IWidgetViewModel)_serviceProvider.GetRequiredService(widgetViewModelType);
        }

        return null;
    }

    public static void RegisterWidget<TWidgetView, TWidgetViewModel>(string displayName)
        where TWidgetView : IWidgetView
        where TWidgetViewModel : IWidgetViewModel
    {
        _widgetRegistrations.Add(typeof(TWidgetViewModel), typeof(TWidgetView));
        _widgetNameRegistrations.Add(displayName, typeof(TWidgetViewModel));
    }
}
