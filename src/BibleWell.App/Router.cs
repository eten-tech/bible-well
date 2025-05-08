using BibleWell.App.ViewModels;

namespace BibleWell.App;

/// <summary>
/// Class initially copied from https://github.com/sandreas/Avalonia.SimpleRouter/blob/main/Avalonia.SimpleRouter/HistoryRouter.cs
/// but heavily modified.
/// </summary>
public sealed class Router
{
    private const uint MaxHistorySize = 20;
    private int _currentIndex = -1;
    private List<ViewModelBase> _history = [];

    public bool CanGoBack => _currentIndex > 0;
    public bool CanGoForward => _history.Count > 0 && _currentIndex < _history.Count - 1;

    public ViewModelBase? Current => _currentIndex < 0 ? null : _history[_currentIndex];

    public event Action<ViewModelBase>? CurrentViewModelChanged;

    public void EraseHistory()
    {
        _currentIndex = -1;
        _history.Clear();
    }

    public ViewModelBase? Back()
    {
        if (!CanGoBack)
        {
            return null;
        }

        _currentIndex--;
        OnCurrentViewModelChanged(Current!);
        return Current;
    }

    public ViewModelBase? Forward()
    {
        if (!CanGoForward)
        {
            return null;
        }

        _currentIndex++;
        OnCurrentViewModelChanged(Current!);
        return Current;
    }

    /// <summary>
    /// Navigates to the view associated with the specified view model type.
    /// If you directly know the type of the view model at compile time, use <see cref="GoTo{T}()" /> instead.
    /// </summary>
    /// <typeparam name="TBaseType">The base type of the view model.</typeparam>
    /// <param name="viewModelType">The view model type.</param>
    /// <returns>The created view model cast to the <typeparamref name="TBaseType" />.</returns>
    public TBaseType GoTo<TBaseType>(Type viewModelType) where TBaseType : ViewModelBase
    {
        if (Current?.GetType() == viewModelType)
        {
            return (TBaseType)Current;
        }

        var destination = ViewModelFactory.Create<TBaseType>(viewModelType);
        Push(destination);
        return destination;
    }

    /// <summary>
    /// Navigates to the view associated with the specified view model type.
    /// </summary>
    /// <typeparam name="T">The type of the view model.</typeparam>
    /// <returns>The created view model.</returns>
    public T GoTo<T>() where T : ViewModelBase
    {
        if (Current?.GetType() == typeof(T))
        {
            return (T)Current;
        }

        var destination = ViewModelFactory.Create<T>();
        Push(destination);
        return destination;
    }

    private void OnCurrentViewModelChanged(ViewModelBase viewModel)
    {
        CurrentViewModelChanged?.Invoke(viewModel);
    }

    private void Push(ViewModelBase item)
    {
        // After navigating back the current index may not be the most forward position.
        // Delete all "forward" items in the history when this happens.
        if (CanGoForward)
        {
            _history = [.. _history.Take(_currentIndex + 1)];
        }

        // add the item and recalculate the index
        _history.Add(item);

        // history exceeded the max size
        if (_history.Count > MaxHistorySize)
        {
            _history.RemoveAt(index: 0);
        }

        _currentIndex = _history.Count - 1;
        OnCurrentViewModelChanged(Current!);
    }
}