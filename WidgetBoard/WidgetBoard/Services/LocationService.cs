namespace WidgetBoard.Services;

public class LocationService : ILocationService
{
    private readonly IGeolocation _geolocation;

    public async Task<Location> GetLocationAsync()
    {
        return await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            var status = await CheckAndRequestLocationPermissionAsync();
            if (status != PermissionStatus.Granted)
            {
                return null;
            }

            return await _geolocation.GetLocationAsync();
        });
    }

    public LocationService(IGeolocation geolocation)
    {
        _geolocation = geolocation;
    }

    private async Task<PermissionStatus> CheckAndRequestLocationPermissionAsync()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        
        if (status == PermissionStatus.Granted)
        {
            return status;
        }

        if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
        {
            // Prompt the user to turn on in settings
            // On iOS once a permission has been denied it may not be requested again from the application
            return status;
        }

        if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
        {
            // Prompt the user with additional information as to why the permission is needed
        }

        status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        return status;
    }
}
