namespace WidgetBoard;

public class Scheduler
{
    /// <summary>
    /// Schedules an action of work to be performed after a specific priod of time.
    /// </summary>
    /// <param name="timeSpan"></param>
    /// <param name="action"></param>
    public static void ScheduleAction(TimeSpan timeSpan, Action action)
    {
        Task.Run(async () =>
        {
            await Task.Delay(timeSpan);
            action.Invoke();
        });
    }
}
