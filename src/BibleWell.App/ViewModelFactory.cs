#if DEBUG
using System.Reflection;
#endif
using Avalonia.Controls;
#if DEBUG
using BibleWell.App.ViewModels;
#endif
using CommunityToolkit.Mvvm.DependencyInjection;

namespace BibleWell.App;

public static class ViewModelFactory
{
    /// <summary>
    /// Use <see cref="Router.GoTo{T}()"/> to navigate to a new view based upon a view model type.
    /// This method is only used to create a view model instance of the specified type.
    /// This can be useful for components where you don't need to navigate to a new view.
    /// </summary>
    /// <typeparam name="T">The type to return (the type or base type of the view model).</typeparam>
    /// <returns>The created view model.</returns>
    public static T Create<T>() where T : ViewModelBase
    {
        return Create<T>(typeof(T));
    }

    /// <summary>
    /// Use <see cref="Router.GoTo{TBaseType}(Type)"/> to navigate to a new view based upon a view model type.
    /// If you directly know the type of the view model at compile time, use <see cref="Create{T}()"/> instead.
    /// This method is only used to create a view model instance of the specified type.
    /// This can be useful for components where you don't need to navigate to a new view.
    /// </summary>
    /// <typeparam name="TBaseType">The base type of the view model.</typeparam>
    /// <param name="viewModelType">The type of the view model.</param>
    /// <returns>The created view model cast to the <typeparamref name="TBaseType"/>.</returns>
    public static TBaseType Create<TBaseType>(Type viewModelType) where TBaseType : ViewModelBase
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
}