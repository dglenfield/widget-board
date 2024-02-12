namespace WidgetBoard.ViewModels;

/// <summary>
/// The ViewModel for the ClockWidget.
/// </summary>
public class ClockWidgetViewModel : BaseViewModel, IWidgetViewModel
{
    private DateOnly _date;
    public DateOnly Date
    {
        get => _date; 
        set => SetProperty(ref _date, value);
    }

    public int Position { get; set; }

    private TimeOnly _time;
    public TimeOnly Time
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
    /// Sets the Date and Time properties for the clock widget.
    /// </summary>
    /// <param name="dateTime"></param>
    public void SetTime(DateTime dateTime)
    {
        Date = DateOnly.FromDateTime(dateTime);
        Time = TimeOnly.FromDateTime(dateTime);
        Scheduler.ScheduleAction(TimeSpan.FromSeconds(1), () => SetTime(DateTime.Now));
    }
}
