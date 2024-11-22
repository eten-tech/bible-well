using Avalonia.Controls;
using Avalonia.Controls.Templates;
using BibleWell.App.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace BibleWell.App;

public sealed class ViewLocator : IDataTemplate
{
    private readonly Dictionary<Type, Func<Control?>> _locator = [];

    public Control Build(object? data)
    {
        if (data is null)
        {
            return new TextBlock { Text = "Error: No ViewModel provided." };
        }

        return _locator.GetValueOrDefault(data.GetType())?.Invoke()
            ?? new TextBlock { Text = $"Error: ViewModel \"{data.GetType().Name}\" not registered." };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }

    public void RegisterViewFactory<TViewModel>(Func<Control?> factory)
        where TViewModel : ViewModelBase
    {
        _locator[typeof(TViewModel)] = factory;
    }

    public void RegisterViewFactory<TViewModel, TView>()
        where TViewModel : ViewModelBase
        where TView : Control
    {
        RegisterViewFactory<TViewModel>(Design.IsDesignMode
            ? Activator.CreateInstance<TView>
            : Ioc.Default.GetService<TView>);
    }
}