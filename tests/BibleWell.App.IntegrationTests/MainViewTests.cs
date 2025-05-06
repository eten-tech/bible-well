using Avalonia;
using Avalonia.Headless.XUnit;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Styling;
using BibleWell.App.ViewModels;
using BibleWell.App.Views;
using BibleWell.App.Views.Pages;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace BibleWell.App.IntegrationTests;

public sealed class MainViewTests
{
    /// <summary>
    /// The TransitioningContentControl has a default transition delay of 125 ms.
    /// We can potentially skip waiting in real-time in the future if https://github.com/AvaloniaUI/Avalonia/pull/16226 ships to v12.
    /// </summary>
    private const int TransitionDelayMs = 125;

    /// <summary>
    /// This test method is both a test and an example of how to wire up the full application UI with a standard <see cref="MainWindow" />.
    /// You should probably use the <see cref="MainMenu_OnPress_ShouldCorrectlyNavigate" /> test method example, instead, though.
    /// </summary>
    [AvaloniaFact]
    public void HomePage_ChangeTheme_Success()
    {
        var window = new MainWindow
        {
            DataContext = Ioc.Default.GetRequiredService<MainViewModel>(),
        };

        window.Show();

        // Ideally we wouldn't assert on colors but for theme variant changes this is necessary.
        window.ExecuteUiTest(() =>
        {
            Application.Current!.RequestedThemeVariant
                .Should()
                .Be(ThemeVariant.Default, "the default requested theme variant should be Default");
            Application.Current.ActualThemeVariant.Should().Be(ThemeVariant.Light, "the default actual theme variant should be Light");
            var backgroundBrush = window.Background as SolidColorBrush;
            backgroundBrush.Should().NotBeNull("the window Background is expected to be a SolidColorBrush");
            backgroundBrush.Color.Should().Be(Colors.White, "the window background should be White for the Light theme variant");

            var homePageView = window.MainView.CurrentControl.GetLogicalChildren().SingleOrDefault() as HomePageView;
            homePageView.Should().NotBeNull("the default page should be the Home page");

            homePageView.ChangeThemeButton.TestClick();

            AvaloniaTestExtensions.WaitForAllUiEventsToComplete();

            Application.Current.ActualThemeVariant.Should().Be(ThemeVariant.Dark, "the actual theme variant should now be Dark");

            var updatedBackgroundBrush = window.Background as SolidColorBrush;
            updatedBackgroundBrush.Should().NotBeNull("the window Background is expected to be a SolidColorBrush");
            updatedBackgroundBrush.Color.Should().Be(Colors.Black, "the window background should be Black for the Dark theme variant");
        });
    }

    /// <summary>
    /// This test method is both a test and an example of how to test any given View using <see cref="AvaloniaTestExtensions" />.
    /// </summary>
    [AvaloniaFact]
    public async Task MainMenu_OnPress_ShouldCorrectlyNavigate()
    {
        // create a MainView in a test window (does not use MainWindow).
        var mainView = Application.Current!.GetTestView<MainViewModel, MainView>();

        await mainView.ExecuteUiTestAsync(async () =>
        {
            // Ideally this would click on the window instead, but it's not clear how to click a certain list item in the view.
            mainView.MenuItemsListBox.SelectedIndex = 3;

            AvaloniaTestExtensions.WaitForAllUiEventsToComplete();

            // This delay is necessary in order for error screenshots to be taken after full transition.
            await Task.Delay(TransitionDelayMs);

            // The selected page should now be the resources page.
            (mainView.CurrentControl.GetLogicalChildren().SingleOrDefault() is ResourcesPageView).Should()
                .BeTrue("the selected menu item should be the Resources page");
        });
    }
}