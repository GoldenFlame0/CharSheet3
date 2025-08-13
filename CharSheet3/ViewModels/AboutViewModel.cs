using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CharSheet3.Services;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CharSheet3.ViewModels;
public partial class AboutViewModel : ViewModelBase
{
    public AboutViewModel(ConfigurationService configurationAndPlatformService)
    {
        _ConfigurationAndPlatformService = configurationAndPlatformService;
        OperatingSystemAndDistributionName = ConfigurationService.GetOperatingSystemAndDistributionName();
        TestString = _ConfigurationAndPlatformService.TestString;
    }

    ConfigurationService _ConfigurationAndPlatformService;

    [ObservableProperty]
    private string operatingSystemAndDistributionName = string.Empty;



    [ObservableProperty]
    private string testString = string.Empty;
}
