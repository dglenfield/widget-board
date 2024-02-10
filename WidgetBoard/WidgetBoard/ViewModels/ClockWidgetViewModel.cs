namespace WidgetBoard.ViewModels;

/// <summary>
/// The ViewModel for the ClockWidget.
/// </summary>
public class ClockWidgetViewModel : BaseViewModel, IWidgetViewModel
{
    public int Position { get; set; }

    private DateTime _time;
    public DateTime Time
    {
        get => _time;
        set => SetProperty(ref _time, value);
    }
    public string Type => "Clock";

    public ClockWidgetViewModel()
    {
        SetTime(DateTime.Now);
    }

    /// <summary>
    /// Sets the Time property and repeat every second so that the widget looks like a clock counting.
    /// </summary>
    /// <param name="dateTime"></param>
    public void SetTime(DateTime dateTime)
    {
        Time = dateTime;
        Scheduler.ScheduleAction(TimeSpan.FromSeconds(1), () => SetTime(DateTime.Now));
    }
}
