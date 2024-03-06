using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBoard.Tests.Mocks;
using WidgetBoard.ViewModels;
using WidgetBoard.Views;

namespace WidgetBoard.Tests.Views;

public class ClockWidgetViewTests
{
    [Fact]
    public void TextIsUpdatedByTimeProperty()
    {
        var time = new DateTime(2022, 1, 1);

        var clockWidget = new ClockWidgetView(null);

        Assert.True(string.IsNullOrWhiteSpace(clockWidget.Text));

        clockWidget.WidgetViewModel = new MockClockWidgetViewModel(time);

        Assert.Equal(time.ToString(), clockWidget.Text);
    }
}
