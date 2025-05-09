# Localization

English is the "neutral" app language and is populated in the `AppResources.resx` base file.  Other languages are
added as `AppResources.{lang}.resx` files.  The app will automatically load the appropriate language based on the device's
language settings.  If a string is not found in the selected language, it will fall back to English (the neutral language).

We purposefully use only the language code in the `resx` file name (e.g. `AppResources.fr.resx`) and not the full
culture code (e.g. `AppResources.fr-CA.resx`) because this allows the app to be used in any country that speaks
the language because any country-specific language will fall back to the language-only resource if no country-specific
resource is found.  If we need to add a country-specific dialect in the future we can do so (e.g. by adding `en-GB`
for British English spellings).

Note that language `resx` files added here will _automatically_ appear in the language selection UI in the app.