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

    /// <summary>
    /// Navigates to the view associated with the specified view model type.
    /// If you directly know the type of the view model at compile time, use <see cref="GoTo{T}()"/> instead.
    /// </summary>
    /// <typeparam name="TBaseType">The base type of the view model.</typeparam>
    /// <param name="viewModelType">The view model type.</param>
    /// <returns>The created view model cast to the <typeparamref name="TBaseType"/>.</returns>
    public TBaseType GoTo<TBaseType>(Type viewModelType) where TBaseType : class, TViewModelBase
    {
        if (Current?.GetType() == viewModelType)
        {
            return (TBaseType)Current;
        }

        var destination = CreateViewModel<TBaseType>(viewModelType);
        Push(destination);
        return destination;
    }

    /// <summary>
    /// Navigates to the view associated with the specified view model type.
    /// </summary>
    /// <typeparam name="T">The type of the view model.</typeparam>
    /// <returns>The created view model.</returns>
    public T GoTo<T>() where T : class, TViewModelBase
    {
        if (Current?.GetType() == typeof(T))
        {
            return (T)Current;
        }

        var destination = CreateViewModel<T>();
        Push(destination);
        return destination;
    }

    /// <summary>
    /// Use <see cref="GoTo{T}()"/> to navigate to a new view based upon a view model type.
    /// This method is only used to create a view model instance of the specified type.
    /// This can be useful for components where you don't need to navigate to a new view.
    /// </summary>
    /// <typeparam name="T">The type to return (the type or base type of the view model).</typeparam>
    /// <returns>The created view model.</returns>
    public T CreateViewModel<T>() where T : class, TViewModelBase
    {
        return CreateViewModel<T>(typeof(T));
    }

    /// <summary>
    /// Use <see cref="GoTo{TBaseType}(Type)"/> to navigate to a new view based upon a view model type.
    /// If you directly know the type of the view model at compile time, use <see cref="CreateViewModel{T}()"/> instead.
    /// This method is only used to create a view model instance of the specified type.
    /// This can be useful for components where you don't need to navigate to a new view.
    /// </summary>
    /// <typeparam name="TBaseType">The base type of the view model.</typeparam>
    /// <param name="viewModelType">The type of the view model.</param>
    /// <returns>The created view model cast to the <typeparamref name="TBaseType"/>.</returns>
    public TBaseType CreateViewModel<TBaseType>(Type viewModelType) where TBaseType : class, TViewModelBase
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

        return viewModel as TBaseType
            ?? throw new InvalidOperationException($"Unable to create {viewModelType.Name}.  Ensure that it derives from {typeof(TBaseType).FullName}.");
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