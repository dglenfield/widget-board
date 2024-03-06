using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBoard.Tests.Mocks;
using WidgetBoard.ViewModels;

namespace WidgetBoard.Tests.ViewModels;

public class WeatherWidgetViewModelTests
{
    [Fact]
    //public async Task NullLocationResultsInPermissionErrorState()
    public void NullLocationResultsInPermissionErrorState()
    {
        var viewModel = new WeatherWidgetViewModel(
            MockWeatherForecastService.ThatReturnsNoForecast(after: TimeSpan.FromSeconds(5)), 
            MockLocationService.ThatReturnsNoLocation(after: TimeSpan.FromSeconds(2)));

        //await viewModel.InitializeAsync();

        Assert.Equal(State.PermissionError, viewModel.State);
        Assert.Null(viewModel.Weather);
    }

    [Fact]
    public void NullForecastResultsInErrorState()
    {
        var viewModel = new WeatherWidgetViewModel(
            MockWeatherForecastService.ThatReturnsNoForecast(after: TimeSpan.FromSeconds(5)),
            MockLocationService.ThatReturns(new Location(0.0, 0.0), after: TimeSpan.FromSeconds(2)));

        //await viewModel.InitializeAsync();

        Assert.Equal(State.Error, viewModel.State);
        Assert.Null(viewModel.Weather);
    }

    [Fact]
    public void ValidForecastResultsInSuccessfulLoad()
    {
        var weatherForecastService = MockWeatherForecastService.ThatReturns(
            new Communications.Forecast
            {
                Main = new Communications.Main
                {
                    Temperature = 18.0
                },
                Weather = new List<Communications.Weather>
                {
                    new Communications.Weather
                    {
                        Icon = "abc.png",
                        Main = "Sunshine"
                    }
                }
            }, after: TimeSpan.FromSeconds(5));

        var locationService = MockLocationService.ThatReturns(new Location(0.0, 0.0), after: TimeSpan.FromSeconds(2));

        var viewModel = new WeatherWidgetViewModel(weatherForecastService, locationService);

        //await viewModel.InitializeAsync();

        Assert.Equal(State.Loaded, viewModel.State);
        Assert.Equal("Sunshine", viewModel.Weather);
    }
}
