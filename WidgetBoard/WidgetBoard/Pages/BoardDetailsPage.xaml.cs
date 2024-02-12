using WidgetBoard.ViewModels;

namespace WidgetBoard.Pages;

/// <summary>
/// This page allows creating and editing boards.
/// </summary>
public partial class BoardDetailsPage : ContentPage
{
	public BoardDetailsPage(BoardDetailsPageViewModel boardDetailsPageViewModel)
	{
		InitializeComponent();

		BindingContext = boardDetailsPageViewModel;
	}
}
