namespace WidgetBoard;

/// <summary>
/// The main entry point to the .NET MAUI application.
/// </summary>
public partial class App : Application
{
    public App(AppShell appShell)
    {
        InitializeComponent();

        MainPage = appShell;
    }
}
