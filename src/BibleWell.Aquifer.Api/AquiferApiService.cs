using System.Net.Http.Json;
using System.Text.Json;
using BibleWell.PushNotifications;

namespace BibleWell.Aquifer.Api;

public sealed class AquiferApiService(HttpClient _aquiferClient) : IReadOnlyAquiferService
{
    public async Task<ResourceContent?> GetResourceContentAsync(int contentId)
    {
        // temporary hack to get HTML resource data for testing from the public API
        if (contentId == 366960)
        {
            const string json = /* lang=json */
                """{"id":366960,"referenceId":295440,"name":"Prayers for Vengeance","localizedName":"Prayers for Vengeance","content":["<p>The psalmists sometimes asked the Lord to execute vengeance against their adversaries. It was not unusual for a psalmist to pray for the violent destruction of their enemies as a manifestation of God’s justice. How can this kind of prayer be okay?</p><p>These prayers for the destruction of the wicked arose out of concern for justice and righteousness and out of confidence in God. Divine justice is defined in <span data-bnType=\"bibleReference\" data-verses=\"[[1019001006,1019001006]]\">Psalm 1:6</span>: The Lord loves the righteous and destroys the wicked. The wicked are subversive, corrupt, and thoroughly committed to evil; they live in opposition to God and to everything that God does. The wicked shake the foundations of ethics, of society, and of God’s kingdom. The psalmists argued that evil is inconsistent with God’s nature and that the removal of evil is the only way for his kingdom to thrive. However, the poets of Israel did not simply invoke God’s judgment on anyone with whom they could not get along. Instead, the psalmists were guided by God’s standards of justice and righteousness, to which God holds all humans accountable.</p><p>The psalmists were intimately acquainted with grief. They had suffered and been oppressed and marginalized by bullies, leaders, and kings from inside and outside of Israel. Their prayers were full of faith and hope, asking how long the Lord would tolerate their suffering and confessing that the Lord alone could rescue them from evil. They expressed deep longing for his redemption. By the principle of retribution, they asked the Lord to inflict upon the wicked the suffering that they had endured (<span data-bnType=\"bibleReference\" data-verses=\"[[1019005010,1019005010]]\">Ps 5:10</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019006010,1019006010]]\">6:10</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019007009,1019007009]]\">7:9</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019009019,1019009020]]\">9:19-20</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019028004,1019028004]]\">28:4</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019056007,1019056007]]\">56:7</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019104035,1019104035]]\">104:35</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019137007,1019137009]]\">137:7-9</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019139019,1019139019]]\">139:19</span>). Through these prayers for justice and vindication, the godly may rest in peace as they await God’s rescue.</p><p>Do we truly see evil as evil, or do we perceive it merely as an inconvenience? Prayers for the end of evil are appropriate as long as we recognize God as arbiter, judge, and executor. The prayer for the coming of God’s Kingdom implies the removal of evil. But now the cruelty inflicted on the wicked has been transformed through the cruel crucifixion of Jesus Christ. This act of God informs how we pray for those who oppose us. Jesus will indeed judge and bring an ultimate end to evil (see <span data-bnType=\"bibleReference\" data-verses=\"[[1067019011,1067019021]]\">Rev 19:11-21</span>), but while Christians await that final judgment, they are to love as Christ loved (<span data-bnType=\"bibleReference\" data-verses=\"[[1044013034,1044013034]]\">John 13:34</span>), pray for their enemies, and forgive them (<span data-bnType=\"bibleReference\" data-verses=\"[[1041005038,1041005048]]\">Matt 5:38-48</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1052003013,1052003013]]\">Col 3:13</span>).</p><p><u>Passages for Further Study</u></p><p><span data-bnType=\"bibleReference\" data-verses=\"[[1014024022,1014024022]]\">2 Chr 24:22</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1016004005,1016004005]]\">Neh 4:5</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019003007,1019003007]]\">Pss 3:7</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019009019,1019009020]]\">9:19-20</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019010015,1019010015]]\">10:15</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019012003,1019012003]]\">12:3</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019041010,1019041010]]\">41:10</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019055015,1019055015]]\">55:15</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019069022,1019069028]]\">69:22-28</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019079006,1019079006]]\">79:6</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019109006,1019109020]]\">109:6-20</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019110005,1019110006]]\">110:5-6</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1019137001,1019137009]]\">137:1-9</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1023061002,1023061002]]\">Isa 61:2</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1024011020,1024011023]]\">Jer 11:20-23</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1024018019,1024018023]]\">18:19-23</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1024051035,1024051035]]\">51:35</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1025001022,1025001022]]\">Lam 1:22</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1025003064,1025003066]]\">3:64-66</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1045001020,1045001020]]\">Acts 1:20</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1046011009,1046011010]]\">Rom 11:9-10</span>; <span data-bnType=\"bibleReference\" data-verses=\"[[1067006010,1067006010]]\">Rev 6:10</span></p>"],"grouping":{"type":"StudyNotes","name":"Study Notes - Themes (Tyndale)","mediaType":"Text","licenseInfo":{"title":"Tyndale Open Study Notes","copyright":{"dates":"2019","holder":{"name":"Tyndale House Publishers","url":"https://tyndaleopenresources.com/"}},"licenses":[{"eng":{"name":"CC BY-SA 4.0 license","url":"https://creativecommons.org/licenses/by-sa/4.0/legalcode.en"}}],"showAdaptationNoticeForEnglish":false,"showAdaptationNoticeForNonEnglish":false}},"language":{"id":1,"code":"eng","displayName":"English","scriptDirection":"LTR"}}""";
            var response = JsonSerializer.Deserialize<ResourceContentResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new ResourceContent(response!.Id, response.Name, string.Join(Environment.NewLine, response.Content));
        }

        try
        {
            // TODO the internal API doesn't expose HTML, only Tiptap JSON
            var response = await _aquiferClient.GetFromJsonAsync<ResourceContentResponse>($"resources/{contentId}/content?contentTextType=html").ConfigureAwait(false);
            if (response != null)
            {
                return new ResourceContent(response.Id, response.Name, Content: string.Join(Environment.NewLine, response.Content));
            }

            return null;
        }
        catch (Exception)
        {
            // TODO implement better exception handling/logging
            return null;
        }
    }

    private class ResourceContentResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required IReadOnlyList<string> Content { get; set; }
    }

    // --- push notifications ---
    public async Task<DeviceInstallation?> RegisterDeviceAsync(DeviceInstallation deviceInstallation)
    {
        var response = await _aquiferClient.PostAsJsonAsync("push-notifications/device-installation", deviceInstallation).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            return new DeviceInstallation
            {
                InstallationId = deviceInstallation.InstallationId,
                Platform = deviceInstallation.Platform,
                PushChannel = deviceInstallation.PushChannel,
                Tags = deviceInstallation.Tags,
            };
        }
        
        return null;
    }

    public async Task DeRegisterDeviceAsync(string deviceId)
    {
        await _aquiferClient.DeleteAsync($"push-notifications/device-installation/{deviceId}").ConfigureAwait(false);
    }
    
    // --- demo code for requesting notifications ---
    public async Task<bool> RequestActionAAsync()
    {
        var result = await _aquiferClient.PostAsJsonAsync("push-notifications/requests", new
        {
            Text = "Action A notification requested.",
            Action = "action_a",
            Tags = (List<object>) [],
            Silent = true
        });

        return result.IsSuccessStatusCode;
    }
    
    public async Task<bool> RequestActionBAsync()
    {
        var result = await _aquiferClient.PostAsJsonAsync("push-notifications/requests", new
        {
            Text = "Action B notification requested.",
            Action = "action_b",
            Tags = (List<object>) [],
            Silent = true
        });

        return result.IsSuccessStatusCode;
    }
}