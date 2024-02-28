using System.Text.Json;

namespace WidgetBoard.Communications;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly HttpClient _httpClient;

    private const string ApiKey = "9976899780c8693df20c622492aed456";
    private const string ServerUrl = "https://api.openweathermap.org/data/2.5/weather?";

    public WeatherForecastService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Forecast> GetForecast(double latitude, double longitude)
    {
        var response = await _httpClient.GetAsync($"{ServerUrl}lat={latitude}&lon={longitude}&units=metric&exclude=minutely,hourly,daily,alerts&appId={ApiKey}")
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        System.Diagnostics.Debug.WriteLine(stringContent);

        try
        {
            Forecast forecast = JsonSerializer.Deserialize<Forecast>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return forecast;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            throw;
        }
    }
}
