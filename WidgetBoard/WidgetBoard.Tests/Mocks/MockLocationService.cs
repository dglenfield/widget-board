using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBoard.Services;

namespace WidgetBoard.Tests.Mocks;

internal class MockLocationService : ILocationService
{
    private readonly Location? _location;
    private readonly TimeSpan _delay;

    internal static ILocationService ThatReturns(Location? location, TimeSpan after) => new MockLocationService(location, after);
    internal static ILocationService ThatReturnsNoLocation(TimeSpan after) => new MockLocationService(null, after);

    private MockLocationService(Location? mockLocation, TimeSpan delay) 
    {
        _location = mockLocation;
        _delay = delay;
    }

    public async Task<Location> GetLocationAsync()
    {
        await Task.Delay(_delay);

        return _location;
    }
}
