using System.Collections.ObjectModel;

namespace DataContract.Extensions;

public static class ObservableCollectionExtensions
{
    public static void AddRange<T>(this ObservableCollection<T> observableCollection, ObservableCollection<T> addedCollection)
    {
        foreach (var newItem in addedCollection)
            observableCollection.Add(newItem);
    }
}