using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBoard.ViewModels;

namespace WidgetBoard.Tests.Mocks;

public class MockClockWidgetViewModel : IWidgetViewModel
{
    public int Position { get; set; }
    public string Type => "Mock";

    public DateTime Time { get; }

    public MockClockWidgetViewModel(DateTime time)
    {
        Time = time;
    }

    public Task InitializeAsync() => Task.CompletedTask;
}
