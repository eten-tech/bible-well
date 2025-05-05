using System.Globalization;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BibleWell.App.Views.Pages;

public sealed partial class DevPageView : UserControl
{
    public DevPageView()
    {
        InitializeComponent();
        
        AttachedToVisualTree += MainView_AttachedToVisualTree;
    }
    private void MainView_AttachedToVisualTree(object? sender, Avalonia.VisualTreeAttachmentEventArgs e)
    {
        CultureComboBox.SelectedIndex = Thread.CurrentThread.CurrentUICulture.Name switch
        {
            "en-US" => 0,
            "es-ES" => 1,
            _ => throw new NotImplementedException($"Unexpected {nameof(Thread.CurrentThread)}.{nameof(Thread.CurrentThread.CurrentUICulture)}: {Thread.CurrentThread.CurrentUICulture.Name}."),
        };
    }

    private void CultureComboBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        var comboBox = sender as ComboBox
                       ?? throw new InvalidOperationException($"Could not cast {nameof(sender)}.");

        var selectedCultureInfo = comboBox.SelectedIndex switch
        {
            0 => CultureInfo.GetCultureInfo("en-US"),
            1 => CultureInfo.GetCultureInfo("es-ES"),
            _ => throw new InvalidOperationException($"Unexpected {nameof(CultureComboBox)}.{nameof(CultureComboBox.SelectedIndex)}: {CultureComboBox.SelectedIndex}."),
        };

        if (Thread.CurrentThread.CurrentUICulture != selectedCultureInfo)
        {
            Thread.CurrentThread.CurrentUICulture = selectedCultureInfo;
            AvaloniaXamlLoader.Load(this);
        }
    }
}