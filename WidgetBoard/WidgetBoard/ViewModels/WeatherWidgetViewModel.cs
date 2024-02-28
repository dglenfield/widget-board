using System.Windows.Input;
using WidgetBoard.Communications;

namespace WidgetBoard.ViewModels;

public class WeatherWidgetViewModel : BaseViewModel, IWidgetViewModel
{
    public const string DisplayName = "Weather";
    public int Position { get; set; }
    public string Type => DisplayName;

    private string _iconUrl;
    public string IconUrl
    {
        get => _iconUrl;
        set => SetProperty(ref _iconUrl, value);
    }

    private State _state;
    public State State
    {
        get => _state;
        set => SetProperty(ref _state, value);
    }

    private double _temperature;
    public double Temperature
    {
        get => _temperature;
        set => SetProperty(ref _temperature, value);
    }

    private string _weather;
    public string Weather
    {
        get => _weather; 
        set => SetProperty(ref _weather, value);
    }

    public ICommand LoadWeatherCommand { get; }

    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherWidgetViewModel(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;

        LoadWeatherCommand = new Command(async () => await LoadWeatherForecast());

        Task.Run(async () => await LoadWeatherForecast());
    }

    private async Task LoadWeatherForecast()
    {
        try
        {
            State = State.Loading;

            var forecast = await _weatherForecastService.GetForecast(20.798363, -156.331924);

            Temperature = forecast.Main.Temperature;
            Weather = forecast.Weather.First().Main;
            IconUrl = forecast.Weather.First().IconUrl;

            System.Diagnostics.Debug.WriteLine(IconUrl);

            State = State.Loaded;
        }
        catch (Exception ex)
        {
            State = State.Error;
        }
    }
}

public enum State
{
    None = 0,
    Loading = 1,
    Loaded = 2,
    Error = 3
}
