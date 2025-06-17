# Bible Well

The Bible Well is a cross-platform app (with primary emphasis on mobile, especially Android) with the goal of exposing
data in the Aquifer to users to provide Biblical resources to aid in Bible translation.

# Technologies

## .net (C#)

The Bible Well is built using [.net](https://dotnet.microsoft.com/en-us/) (currently .net 9) and C#.

### AvaloniaUI

[AvaloniaUI](https://avaloniaui.net/) provides cross-platform UI to desktop, mobile, and web with a single .net codebase with markdown similar to WPF.  It is a UI framework and only cares about UI tooling (e.g. it doesn't provide a an API save data to the device's storage).  Avalonia renders its own UI elements using [Skia](https://skia.org/) and thus UI appears the same on all platforms.

Within Avalonia we use the MVVM pattern via the [Community MVVM Toolkit](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/).

### MAUI

[.net MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui?view=net-maui-8.0) is the replacement for Xamarin and offers cross-platform APIs as well as UI tooling.  Bible Well only uses cross-platform "under-the-hood" C# APIs via [MAUI Essentials](https://docs.avaloniaui.net/docs/guides/building-cross-platform-applications/dealing-with-platforms#using-mauiessentials), not the UI components, though it is possible for [Avalonia and MAUI UI to co-exist](https://github.com/AvaloniaUI/AvaloniaMauiHybrid) if needed.

## SQLite

Data access is accomplished via [SQLite](https://www.sqlite.org/).

# Installation

Getting started with .net:
* Install the [latest .net SDK](https://dotnet.microsoft.com/en-us/download).
* Ensure appropriate [workloads are installed](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-workload-install).  At a minimum you will need `android`, `ios`, and `wasm-tools` for Avalonia development.  MAUI has additional workloads as well (run `dotnet workload search`).
* Install an IDE.  The following IDEs all support Avalonia extensions: [Visual Studio Community Edition](https://visualstudio.microsoft.com/vs/community/) (VS) (free for open-source development), [VSCode](https://code.visualstudio.com/) (free), and [JetBrains Rider](https://www.jetbrains.com/rider/) (paid).  VS or Rider are the best options and offer design-time previews of the UI but note that VS is only available on Windows.

Getting started with AvaloniaUI:
* Start here: https://docs.avaloniaui.net/docs/get-started/
* Install [Avalonia templates](https://docs.avaloniaui.net/docs/get-started/install#install-avalonia-ui-templates).
* If you use VS then install the [Avalonia for Visual Studio 2022 extension](https://marketplace.visualstudio.com/items?itemName=AvaloniaTeam.AvaloniaVS).
* If you use Rider then install the [visual designer plugin](https://plugins.jetbrains.com/plugin/14839-avaloniarider/).
* Configure the [Android SDK](https://learn.microsoft.com/en-us/previous-versions/xamarin/android/get-started/installation/android-sdk?tabs=windows).
* See also this [video walkthrough](https://www.youtube.com/watch?v=yvzdtYAvhVE) including Rider setup with manual JDK install. 
* On Mac, also install [Xcode](https://developer.apple.com/xcode/) and [Xcode command line tools](https://mac.install.guide/commandlinetools/).
* On Mac, install the iOS platform by opening XCode, then clicking `XCode -> Settings -> Components -> Get iOS [version]`.
* You can also install [Android Studio](https://developer.android.com/studio) if desired but it's not necessary for Android development using Avalonia because VS and Rider both have built-in Android emulation.

After install, consider working through the Avalonia tutorials to become familiar with the framework: https://docs.avaloniaui.net/docs/tutorials/.

# Getting Started

## Dev Tools

Dev tools are available from Avalonia when running the DEBUG target of the desktop application locally.  Hit F12 to see the dev tools window (similar to the Chrome dev tools).

## Database Storage

Sqlite read/writes in this environment can easily be used with or without EF. After careful consideration and testing, using [Dapper](https://www.learndapper.com) will be a more consistent method for interacting with the database while still offering a familiar and less verbose syntax. Due to AOT compilation constraints we are using [DapperAoT](https://aot.dapperlib.dev/) which allows code to be generated at compile time instead of runtime.

### Models and Repositories

There are models for aquifer and the db \- map to and from in the service  
Also in the service, any business logic for querying  
Repo should contain sql for basic crud and creation of table.  
Repo instantiated as singleton in App.axaml.cs and DI in SqliteAquiferService

### Migrations

Using EF for migrations is not advisable as the tooling for migrations is not compatible with mobile without some intricate workarounds that would make deployment and maintenance more difficult than necessary. A better alternative is to handle migrations manually.

The strategy we are using … (TDB)

## SplashScreen

When researching how to do a splash screen, most solutions have you make a window or UserControl to do it. That is, in reality, creating a secondary splash screen and laying that on top of what’s already there. The [Avalonia templates](https://github.com/AvaloniaUI/avalonia-dotnet-templates% 9) (xplat) have the splash screens for iOS and Android baked into the solution already, and that’s why we have that as default now. 

**Android:** [replace the icon with your own, and get rid of the Resources/drawable-\* folders.](https://github.com/AvaloniaUI/Avalonia/discussions/16170) Also, you may need to edit the \`values-v31/styles.xml\` and take out the references that come from those folders.

**iOS**: [can’t easily test locally](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/images/splashscreen?view=net-maui-9.0&tabs=ios%20) until the app is signed, [but here’s a pretty good resource](https://stackoverflow.com/questions/79565533/using-net-8-for-ios-not-maui-add-a-launchscreen-with-logo-image-and-backgrou/79565661#79565661) once we’re ready to do it.

Another option would be to make a [custom Avalonia template](https://learn.microsoft.com/en-us/dotnet/core/tools/custom-templates) with your own icon/etc, but that’s out of scope of this solution.

## Design View

Avalonia supports seeing a live design view UI of the `axaml` files as you edit without building and running the application (this feature requires IDE plugins).  Custom design-time data can also be injected to render only in this view for testing purposes.  In order to support this feature we use the following patterns:

1. Always create a static design-time view model property for the view model type in the `ViewModels.DesignData` class.  This design view model may inject test data as needed for design-time testing.  
2. If required, the design view model may inject fake services that do not perform I/O during construction.  This can be done using FakeItEasy (e.g. `A.Fake<IService>()`) or by manually creating a fake service that implements the required interface if specific behavior is required.  
3. Finally, register the design-time view model in the view’s `axaml` like so:

```
d:DataContext="{x:Static vm:DesignData.DesignFooViewModel}"
```

## Platform Differences

BibleWell targets Android and iOS for mobile but also Windows/MacOS/Linux for development desktop testing purposes.  Avalonia is also capable of targeting the Browser.  Each of these targets may need its own implementation of platform specific behavior.  When this is necessary, a common interface should be created in the `BibleWell` business-layer project with implementations for each platform as needed in the platform specific app project (e.g. `BibleWell.App.Desktop`) (see [the Avalonia docs](https://docs.avaloniaui.net/docs/guides/platforms/platform-specific-code/dotnet#platform-specific-projects)).  Note that we are using MAUI Essentials for many platform specific operations which works for both iOS and Android when running under an Avalonia application (unfortunately Windows and MacCatalyst aren’t yet fully supported).  For MAUI Essentials implementations of these common platform interfaces see the `BibleWell.Platform.Maui` project.

In general the UI we use should be platform agnostic but it is also possible to [specify platform specific XAML](https://docs.avaloniaui.net/docs/guides/platforms/platform-specific-code/xaml).  If this strategy is necessary then it should be used sparingly to inject app-specific global styles, entire controls that differ by platform (e.g. a control that wraps an Android native WebView only for Android usage), etc., instead of used frequently in the code base (e.g. every time a button needs a slightly wider margin on only one platform).

Finally, platform specific device settings and environment configuration are shown on the Dev page in the app for developer convenience.

## Styling

We are using the Avalonia built-in Fluent Theme.  This theme is configured in `App.axaml`, including the default color scheme (an editor is available; see comments in source code).

`/Assets/AppResources.axaml` should be used for storing colors and/or other keys that differ between theme variants (e.g. a dark background color and a light background color) when rolling our own controls.  These colors should also then be injected in the above-mentioned Avalonia fluent theme in the configuration in `App.axaml` with `StaticResource` or `DynamicResource` injection.

Global styles live in `/Assets/AppStyles.axaml`.  These can be applied like CSS classes.  Styling can also be applied locally within a single window, control, panel, etc.  Styles should never specify colors directly; instead they should reference app resources by key (see above) which will apply the correct color based upon the user’s selected theme variant.

Finally, style properties can be set on an individual control.  This takes the place of CSS styling using tailwind which allows similar styling using CSS classes but in this case does not require Avalonia style classes (e.g. setting the margin on a text block control via the `Margin` property instead of applying a tailwind CSS class for comparable HTML).

## Push Notifications \- WIP

- Requires API backend  
- ‘Registration’ should happen automatically on install (might have a UI in PoC to start)  
- Android and Apple have separate implementations  
-  


The plan is to borrow from the Discovery work done on push notifications as a PoC and then convert over to a more permanent implementation (of the google services). iOS requires a bit of developer registration \- more info to come as this is worked through.  
I will start with implementing notifications on Android as the PoC and gather more information on an iOS implementation.

Tags

Templates

Azure Notifications Hub and Push Notifications Services

Permissions and Presentation to User

## Logging

`Microsoft.Extensions.Logging` is used along with dependency injection to provide a logger to any class that needs it (just add `ILogger<T> logger` to the class constructor where `T` is the class name).

Logging is accomplished via Azure ApplicationInsights.  Console logging is also enabled for development purposes (when the app is built using the `DEBUG` target) but is not enabled for the production application.

Currently AppInsights will only log from a mobile device that is connected to the internet.  There is no offline logging.  This will likely change in the future.

## Testing

See [https://github.com/eten-tech/bible-well/blob/main/tests/readme.md](https://github.com/eten-tech/bible-well/blob/main/tests/readme.md).  
