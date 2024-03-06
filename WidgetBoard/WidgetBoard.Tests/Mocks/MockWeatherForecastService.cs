using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBoard.Communications;

namespace WidgetBoard.Tests.Mocks;

internal class MockWeatherForecastService : IWeatherForecastService
{
    private readonly Forecast? _forecast;
    private readonly TimeSpan _delay;

    internal static IWeatherForecastService ThatReturns(Forecast? forecast, TimeSpan after) => new MockWeatherForecastService(forecast, after);
    internal static IWeatherForecastService ThatReturnsNoForecast(TimeSpan after) => new MockWeatherForecastService(null, after);

    private MockWeatherForecastService(Forecast? forecast, TimeSpan delay)
    {
        _forecast = forecast;
        _delay = delay;
    }

    public async Task<Forecast> GetForecast(double latitude, double longitude)
    {
        await Task.Delay(_delay);

        return _forecast;
    }
}
