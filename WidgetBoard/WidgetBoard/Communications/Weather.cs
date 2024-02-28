namespace WidgetBoard.Communications;

public class Weather
{
    public string Icon { get; set; }
    public string IconUrl => $"https://openweathermap.org/img/wn/{Icon}@2x.png";
    public string Main { get; set; }
}
