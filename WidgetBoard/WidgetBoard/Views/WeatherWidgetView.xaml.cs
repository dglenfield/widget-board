using WidgetBoard.ViewModels;

namespace WidgetBoard.Views;

public partial class WeatherWidgetView : ContentView, IWidgetView
{
    public IWidgetViewModel WidgetViewModel
    {
        get => BindingContext as IWidgetViewModel;
        set => BindingContext = value;
    }

    public WeatherWidgetView()
	{
		InitializeComponent();
	}
}
