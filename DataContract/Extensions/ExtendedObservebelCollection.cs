using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DataContract.Extensions;

public class ExtendedObservableCollection<T> : ObservableCollection<T> where T : notnull, INotifyPropertyChanged
{
    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems is not null)
            foreach (var item in e.NewItems)
                ((T)item).PropertyChanged += Caller;
        
        if(e.OldItems is not null)
            foreach (var item in e.OldItems)
                ((T)item).PropertyChanged -= Caller;
        
        base.OnCollectionChanged(e);
    }

    private void Caller(object? sender, PropertyChangedEventArgs e) => ItemPropertyChanged?.Invoke();
    
    public event Action? ItemPropertyChanged;
}