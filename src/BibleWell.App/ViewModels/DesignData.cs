#if DEBUG
using BibleWell.App.Configuration;
using BibleWell.App.ViewModels.Components;
using BibleWell.App.ViewModels.Pages;
using BibleWell.Aquifer;
using BibleWell.Devices;
using BibleWell.Preferences;
using BibleWell.Storage;
using BibleWell.Utility;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BibleWell.App.ViewModels;

/// <summary>
/// This class is only built in DEBUG mode and is only for providing design-time data.
/// </summary>
public static class DesignData
{
    private const string TiptapJson = /* lang=json */
        """{"tiptap":{"type":"doc","content":[{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"The psalmists sometimes asked the Lord to execute vengeance against their adversaries. It was not unusual for a psalmist to pray for the violent destruction of their enemies as a manifestation of God’s justice. How can this kind of prayer be okay?"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"These prayers for the destruction of the wicked arose out of concern for justice and righteousness and out of confidence in God. Divine justice is defined in "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019001006","endVerse":"1019001006"}]}}],"text":"Psalm 1:6"},{"type":"text","text":": The Lord loves the righteous and destroys the wicked. The wicked are subversive, corrupt, and thoroughly committed to evil; they live in opposition to God and to everything that God does. The wicked shake the foundations of ethics, of society, and of God’s kingdom. The psalmists argued that evil is inconsistent with God’s nature and that the removal of evil is the only way for his kingdom to thrive. However, the poets of Israel did not simply invoke God’s judgment on anyone with whom they could not get along. Instead, the psalmists were guided by God’s standards of justice and righteousness, to which God holds all humans accountable."}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"The psalmists were intimately acquainted with grief. They had suffered and been oppressed and marginalized by bullies, leaders, and kings from inside and outside of Israel. Their prayers were full of faith and hope, asking how long the Lord would tolerate their suffering and confessing that the Lord alone could rescue them from evil. They expressed deep longing for his redemption. By the principle of retribution, they asked the Lord to inflict upon the wicked the suffering that they had endured ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019005010","endVerse":"1019005010"}]}}],"text":"Ps 5:10"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019006010","endVerse":"1019006010"}]}}],"text":"6:10"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019007009","endVerse":"1019007009"}]}}],"text":"7:9"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019009019","endVerse":"1019009020"}]}}],"text":"9:19-20"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019028004","endVerse":"1019028004"}]}}],"text":"28:4"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019056007","endVerse":"1019056007"}]}}],"text":"56:7"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019104035","endVerse":"1019104035"}]}}],"text":"104:35"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019137007","endVerse":"1019137009"}]}}],"text":"137:7-9"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019139019","endVerse":"1019139019"}]}}],"text":"139:19"},{"type":"text","text":"). Through these prayers for justice and vindication, the godly may rest in peace as they await God’s rescue."}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"Do we truly see evil as evil, or do we perceive it merely as an inconvenience? Prayers for the end of evil are appropriate as long as we recognize God as arbiter, judge, and executor. The prayer for the coming of God’s Kingdom implies the removal of evil. But now the cruelty inflicted on the wicked has been transformed through the cruel crucifixion of Jesus Christ. This act of God informs how we pray for those who oppose us. Jesus will indeed judge and bring an ultimate end to evil (see "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1067019011","endVerse":"1067019021"}]}}],"text":"Rev 19:11-21"},{"type":"text","text":"), but while Christians await that final judgment, they are to love as Christ loved ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1044013034","endVerse":"1044013034"}]}}],"text":"John 13:34"},{"type":"text","text":"), pray for their enemies, and forgive them ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1041005038","endVerse":"1041005048"}]}}],"text":"Matt 5:38-48"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1052003013","endVerse":"1052003013"}]}}],"text":"Col 3:13"},{"type":"text","text":")."}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","marks":[{"type":"underline"}],"text":"Passages for Further Study"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1014024022","endVerse":"1014024022"}]}}],"text":"2 Chr 24:22"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1016004005","endVerse":"1016004005"}]}}],"text":"Neh 4:5"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019003007","endVerse":"1019003007"}]}}],"text":"Pss 3:7"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019009019","endVerse":"1019009020"}]}}],"text":"9:19-20"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019010015","endVerse":"1019010015"}]}}],"text":"10:15"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019012003","endVerse":"1019012003"}]}}],"text":"12:3"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019041010","endVerse":"1019041010"}]}}],"text":"41:10"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019055015","endVerse":"1019055015"}]}}],"text":"55:15"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019069022","endVerse":"1019069028"}]}}],"text":"69:22-28"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019079006","endVerse":"1019079006"}]}}],"text":"79:6"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019109006","endVerse":"1019109020"}]}}],"text":"109:6-20"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019110005","endVerse":"1019110006"}]}}],"text":"110:5-6"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1019137001","endVerse":"1019137009"}]}}],"text":"137:1-9"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1023061002","endVerse":"1023061002"}]}}],"text":"Isa 61:2"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1024011020","endVerse":"1024011023"}]}}],"text":"Jer 11:20-23"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1024018019","endVerse":"1024018023"}]}}],"text":"18:19-23"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1024051035","endVerse":"1024051035"}]}}],"text":"51:35"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1025001022","endVerse":"1025001022"}]}}],"text":"Lam 1:22"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1025003064","endVerse":"1025003066"}]}}],"text":"3:64-66"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1045001020","endVerse":"1045001020"}]}}],"text":"Acts 1:20"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1046011009","endVerse":"1046011010"}]}}],"text":"Rom 11:9-10"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":"1067006010","endVerse":"1067006010"}]}}],"text":"Rev 6:10"}]}]}}""";

    private static readonly Router s_router = new();

    // components
    public static TiptapRendererViewModel DesignTiptapRendererViewModel { get; } = new()
    {
        ResourceContentTiptap = JsonUtilities.DefaultDeserialize<TiptapModel<TiptapNode>>(TiptapJson),
    };

    // pages
    public static BiblePageViewModel DesignBiblePageViewModel { get; } = new();

    public static DevPageViewModel DesignDevPageViewModel { get; } = new(
        A.Fake<IApplicationInfoService>(),
        A.Fake<IDeviceService>(),
        A.Fake<IStorageService>(),
        A.Fake<IReadWriteAquiferService>(),
        A.Fake<IOptions<ConfigurationOptions>>(),
        A.Fake<ILogger<DevPageViewModel>>());

    public static GuidePageViewModel DesignGuidePageViewModel { get; } = new();
    public static HomePageViewModel DesignHomePageViewModel { get; } = new(s_router, A.Fake<IUserPreferencesService>());
    public static LanguagesPageViewModel DesignLanguagesPageViewModel { get; } = new(
        s_router,
        A.Fake<IUserPreferencesService>(),
        A.Fake<ICachingAquiferService>());

    public static ParentResourcesPageViewModel DesignParentResourcesPageViewModel { get; } = new(
        s_router,
        A.Fake<ICachingAquiferService>());   
    public static LibraryPageViewModel DesignLibraryPageViewModel { get; } = new();
    public static ResourcesPageViewModel DesignResourcesPageViewModel { get; } = new(A.Fake<ICachingAquiferService>());

    // this must be last because it references the above view models
    public static MainViewModel DesignMainViewModel { get; } = new(s_router, A.Fake<IUserPreferencesService>());
}
#endif