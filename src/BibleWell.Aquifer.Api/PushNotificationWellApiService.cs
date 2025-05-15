using BibleWell.Aquifer.API.Client;
using BibleWell.Aquifer.API.Client.Models;
using BibleWell.PushNotifications;
using Microsoft.Extensions.Logging;

namespace BibleWell.Aquifer.Api;

public sealed class PushNotificationWellApiService(ILogger<PushNotificationWellApiService> _logger, AquiferWellApiClient _aquiferClient) : IPushNotificationWellApiService
{
    private static CreateDeviceInstallationRequest MapToCreateDeviceInstallationRequest(DeviceInstallation source)
    {
        return new CreateDeviceInstallationRequest()
        {
            InstallationId = source.InstallationId,
            Platform = source.Platform,
            PushChannel = source.PushChannel,
            Tags = source.Tags,
        };
    }
    
    public async Task RegisterDeviceAsync(DeviceInstallation deviceInstallation)
    {
        try
        {
            await _aquiferClient
                .PushNotifications
                .DeviceInstallation
                .PostAsync(MapToCreateDeviceInstallationRequest(deviceInstallation))
                .ConfigureAwait(false);
        }
        catch (ErrorResponse ex)
        {
            _logger.LogError(ex, "RegisterDeviceAsync failed.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating or updating device installation on Aquifer Well API.");
            throw;
        }
    }

    public async Task DeregisterDeviceAsync(string deviceId)
    {
        try
        {
            await _aquiferClient.PushNotifications.DeviceInstallation[deviceId].DeleteAsync().ConfigureAwait(false);
        }
        catch (ErrorResponse ex)
        {
            _logger.LogError(ex, "DeRegisterDeviceAsync failed.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error: device installation deletion on Aquifer Well API.");
            throw;
        }
    }
    
    // --- demo code for requesting notifications ---
    public async Task RequestActionAAsync()
    {
        try
        {
            await _aquiferClient.PushNotifications.Requests.PostAsync(
                new NotificationRequest
                {
                    Text = "Action A notification requested.",
                    Action = "action_a",
                    Tags = [],
                    Silent = true,
                });
        }
        catch (ErrorResponse ex)
        {
            _logger.LogError(ex, "RequestActionAAsync failed.");
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "RequestActionAAsync API call failed.");
            throw;
        }
    }
    
    public async Task RequestActionBAsync()
    {
        try
        {
            await _aquiferClient.PushNotifications.Requests.PostAsync(
                new NotificationRequest
                {
                    Text = "Action B notification requested.",
                    Action = "action_b",
                    Tags = [],
                    Silent = true,
                });
        }
        catch (ErrorResponse ex)
        {
            _logger.LogError(ex, "RequestActionBAsync failed.");
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "RequestActionBAsync API call failed.");
            throw;
        }
    }
}