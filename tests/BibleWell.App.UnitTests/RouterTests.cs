using BibleWell.App.ViewModels.Pages;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BibleWell.App.UnitTests;

public sealed class RouterTests
{
    [Fact]
    public void Router_Navigation_ShouldCorrectlyNavigate()
    {
        // These are the only services needed for the router to work.
        // We don't care about the services being injected into the view models for this test so inject null.
        var services = new ServiceCollection()
            .AddTransient(_ => new HomePageViewModel(null!))
            .AddTransient(_ => new BiblePageViewModel())
            .AddTransient(_ => new GuidePageViewModel())
            .AddTransient(_ => new ResourcesPageViewModel(null!));

        Ioc.Default.ConfigureServices(services.BuildServiceProvider());

        var router = new Router();
        router.Current.Should().BeNull();
        router.CanGoBack.Should().BeFalse();
        router.CanGoForward.Should().BeFalse();

        router.GoTo<HomePageViewModel>();
        router.Current.Should().BeOfType<HomePageViewModel>();
        router.CanGoBack.Should().BeFalse();
        router.CanGoForward.Should().BeFalse();

        router.GoTo<BiblePageViewModel>();
        router.Current.Should().BeOfType<BiblePageViewModel>();
        router.CanGoBack.Should().BeTrue();
        router.CanGoForward.Should().BeFalse();

        router.GoTo<GuidePageViewModel>();
        router.Current.Should().BeOfType<GuidePageViewModel>();
        router.CanGoBack.Should().BeTrue();
        router.CanGoForward.Should().BeFalse();

        router.Back();
        router.Current.Should().BeOfType<BiblePageViewModel>();
        router.CanGoBack.Should().BeTrue();
        router.CanGoForward.Should().BeTrue();

        router.Back();
        router.Current.Should().BeOfType<HomePageViewModel>();
        router.CanGoBack.Should().BeFalse();
        router.CanGoForward.Should().BeTrue();

        router.Forward();
        router.Current.Should().BeOfType<BiblePageViewModel>();
        router.CanGoBack.Should().BeTrue();
        router.CanGoForward.Should().BeTrue();

        router.GoTo<ResourcesPageViewModel>();
        router.Current.Should().BeOfType<ResourcesPageViewModel>();
        router.CanGoBack.Should().BeTrue();
        router.CanGoForward.Should().BeFalse();
    }
}