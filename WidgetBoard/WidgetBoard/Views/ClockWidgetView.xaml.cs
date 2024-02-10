using WidgetBoard.ViewModels;

namespace WidgetBoard.Views;

/// <summary>
/// The View for the ClockWidget.
/// </summary>
public partial class ClockWidgetView : Label, IWidgetView
{
	public IWidgetViewModel WidgetViewModel { get; set; }

	public ClockWidgetView()
	{
		InitializeComponent();

		WidgetViewModel = new ClockWidgetViewModel();
		BindingContext = WidgetViewModel;
	}
}
