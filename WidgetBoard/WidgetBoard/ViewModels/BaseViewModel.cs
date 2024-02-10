using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WidgetBoard.ViewModels;

/// <summary>
/// The base class for all the view models.
/// </summary>
public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName"></param>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Can be called from a property setter, passing in the field and value being set. 
    /// Checks whether the value is different from the backing field to determine whether the property has really changed.
    /// If it has changed, fires the PropertyChanged event using the OnPropertyChanged method.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="backingField"></param>
    /// <param name="value"></param>
    /// <param name="propertyName"></param>
    /// <returns>A Boolean indicating whether the value really did change.</returns>
    protected bool SetProperty<TValue>(ref TValue backingField, TValue value, [CallerMemberName] string propertyName = "")
    {
        if (Comparer<TValue>.Default.Compare(backingField, value) == 0)
        {
            return false;
        }

        backingField = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
