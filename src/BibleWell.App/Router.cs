#if DEBUG
using System.Reflection;
#endif
using Avalonia.Controls;
#if DEBUG
using BibleWell.App.ViewModels;
#endif
using CommunityToolkit.Mvvm.DependencyInjection;

namespace BibleWell.App;

/// <summary>
/// Class initially copied from https://github.com/sandreas/Avalonia.SimpleRouter/blob/main/Avalonia.SimpleRouter/HistoryRouter.cs
/// but heavily modified.
/// </summary>
public sealed class Router<TViewModelBase> where TViewModelBase : class
{
    private int _currentIndex = -1;
    private List<TViewModelBase> _history = [];
    private const uint MaxHistorySize = 20;

    public bool CanGoBack => _currentIndex > 0;
    public bool CanGoForward => _history.Count > 0 && _currentIndex < _history.Count - 1;

    public TViewModelBase? Current => _currentIndex < 0 ? null : _history[_currentIndex];

    public event Action<TViewModelBase>? CurrentViewModelChanged;

    public TViewModelBase? Back()
    {
        if (!CanGoBack)
        {
            return null;
        }

        _currentIndex--;
        OnCurrentViewModelChanged(Current!);
        return Current;
    }

    public TViewModelBase? Forward()
    {
        if (!CanGoForward)
        {
            return null;
        }

        _currentIndex++;
        OnCurrentViewModelChanged(Current!);
        return Current;
    }

    public TBaseType GoTo<TBaseType>(Type viewModelType) where TBaseType : class, TViewModelBase
    {
        if (Current?.GetType() == viewModelType)
        {
            return (TBaseType) Current;
        }

        var destination = CreateViewModel<TBaseType>(viewModelType);
        Push(destination);
        return destination;
    }

    public T GoTo<T>() where T : class, TViewModelBase
    {
        if (Current?.GetType() == typeof(T))
        {
            return (T)Current;
        }

        var destination = CreateViewModel<T>(typeof(T));
        Push(destination);
        return destination;
    }

    private static T CreateViewModel<T>(Type viewModelType) where T : class, TViewModelBase
    {
        object? viewModel;
        if (Design.IsDesignMode)
        {
#if DEBUG
            // get the DesignData view model property for this type (if it exists)
            viewModel = typeof(DesignData)
                    .GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(pi => pi.PropertyType == viewModelType)
                    ?.GetValue(null, null)
                ?? throw new InvalidOperationException($"No design-time view model is defined for {viewModelType} in {typeof(DesignData).FullName}.");
#else
            viewModel = Activator.CreateInstance(viewModelType);
#endif
        }
        else
        {
            viewModel = Ioc.Default.GetService(viewModelType);
        }

        return viewModel as T
            ?? throw new InvalidOperationException($"Unable to create {viewModelType.Name}.  Ensure that it derives from {typeof(T).FullName}.");
    }

    private void OnCurrentViewModelChanged(TViewModelBase viewModel)
    {
        CurrentViewModelChanged?.Invoke(viewModel);
    }

    private void Push(TViewModelBase item)
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
            _history.RemoveAt(0);
        }

        _currentIndex = _history.Count - 1;
        OnCurrentViewModelChanged(Current!);
    }
}