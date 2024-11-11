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
