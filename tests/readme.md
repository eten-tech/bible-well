# BibleWell Mobile Tests

This directory contains automated tests which must succeed as part of the CI/CD process.

## Technologies

We currently use [XUnit](https://xunit.net/) for testing in .net.

.net testing best practices (consider these documents as guidance but not necessarily as law):

* [Unit testing](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices).
* [Integration testing](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests).

Note that common test project configuration is in the `Directory.Build.props` file in this directory which
inherits from and overrides the Solution root's `Directory.Build.props` file.

## Running the Tests

Run the following from the solution root directory:

```bash
dotnet test
```

You can also use an IDE to run one or more tests with results visible in the IDE's Test Runner UI.

## Naming

1. Test projects should match the naming of the project under test but with an `.IntegrationTests` or `.UnitTests` suffix.
1. Test classes should be in their own file and class names should end with the `Tests` suffix, _not_ `TestFixture`.
1. Test method names should strive to
   follow [.net test naming conventions](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices#naming-your-tests)
   though shorter names are often acceptable.

## Testing Philosophy

See https://github.com/BiblioNexusStudio/aquifer-server/blob/master/tests/readme.md#testing-philosophy for basic philosophy.

### Avalonia UI Testing Philosophy

#### `BibleWell.App.IntegrationTests`

This project uses Avalonia headless application testing of the Desktop application (we could choose to make test-specific platform interface
implementations but it's convenient to use the Desktop application implementations). Therefore, this project should:

* only test the app's UI and not the underlying business logic.
* primarily use Views for testing and not ViewModels.
* not include many tests because testing the UI is slow and we have to run tests sequentially.
* limit testing to the most important UI functionality happy paths.

Examples:

* Test that theme and theme variant changes work correctly.
* Test that localization works correctly when switching cultures and that correct localized text is displayed.
* Test UI layout is as expected (by comparing a screenshot of the test UI with a reference screenshot).

#### `BibleWell.App.UnitTests`

This project should test the app's UI logic but not the UI presentation itself. Therefore, it should **always** use ViewModels, never Views.

Examples:

* Test that a command correctly populates expected properties on a ViewModel.
* Test router behavior to ensure that back functionality works correctly.
* Test that fake resource data loads correctly into the view model based upon a test resource search scenario.
