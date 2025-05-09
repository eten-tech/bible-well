using System.Reflection;
using BibleWell.Aquifer.API.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Http.HttpClientLibrary.Middleware.Options;

namespace BibleWell.Aquifer.Api.Helpers;

public static class KiotaServiceRegistryExtensions
{
    public static IServiceCollection AddAquiferWellApiClient(this IServiceCollection services, string baseUri, string apiKey)
    {
        services.AddHttpClient<AquiferWellApiClient>()
            .AddTypedClient(httpClient =>
            {
                // See note on SetApiKeyHeaderRequestHandler about why we don't use the ApiKeyAuthenticationProvider.
                var adapter = new HttpClientRequestAdapter(
                    new AnonymousAuthenticationProvider(),
                    httpClient: httpClient)
                {
                    BaseUrl = baseUri,
                };

                return new AquiferWellApiClient(adapter);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var userAgentHandlerOption = new UserAgentHandlerOption
                {
                    Enabled = true,
                    ProductName = Assembly.GetExecutingAssembly().GetName().Name ?? "",
                    ProductVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "",
                };

                var defaultHttpMessageHandler = KiotaClientFactory.GetDefaultHttpMessageHandler();
                var defaultHandlers = KiotaClientFactory.CreateDefaultHandlers([userAgentHandlerOption]);

                return KiotaClientFactory.ChainHandlersCollectionAndGetFirstLink(
                    defaultHttpMessageHandler,
                    [.. defaultHandlers, new SetApiKeyHeaderRequestHandler(apiKey)])!;
            });

        return services;
    }
}

/// <summary>
/// Kiota supports using an API key as a header for authentication, but it doesn't work for http, only for https.
/// This means we can't use it against a local API.
/// Code for using it would look like this:
/// <code>
/// var apiKeyAuthenticationProvider = new ApiKeyAuthenticationProvider(
///     apiKey,
///     "api-key",
///     ApiKeyAuthenticationProvider.KeyLocation.Header);
/// var adapter = new HttpClientRequestAdapter(
///     apiKeyAuthenticationProvider,
///     ...);
/// </code>
/// We could potentially switch back to this strategy in the 2.0 Kiota release if they add http support with a flag.
/// </summary>
/// <param name="_apiKey">The API key to set in the "api-key" request header.</param>
public sealed class SetApiKeyHeaderRequestHandler(string _apiKey) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("api-key", _apiKey);
        return await base.SendAsync(request, cancellationToken);
    }
}