using CharSheet3.Services;
using CharSheet3.Utilities;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CharSheet3.ViewModels;
public partial class AboutViewModel : ViewModelBase
{
    public AboutViewModel(ConfigurationService configurationAndPlatformService)
    {
        _ConfigurationAndPlatformService = configurationAndPlatformService;
        OperatingSystemAndDistributionName = PlatformInfo.GetOperatingSystemAndDistributionName();
        TestString = _ConfigurationAndPlatformService.TestString;
    }

    ConfigurationService _ConfigurationAndPlatformService;

    [ObservableProperty]
    private string operatingSystemAndDistributionName = string.Empty;



    [ObservableProperty]
    private string testString = string.Empty;
}
