using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CharSheet3.Services;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CharSheet3.ViewModels;
public partial class SettingsViewModel : ViewModelBase
{
    public SettingsViewModel(ConfigurationAndPlatformService configurationAndPlatformService)
    {
        // Initialize the properties with values from the settings service
        _ConfigurationAndPlatformService = configurationAndPlatformService;
        operatingSystemAndDistributionName = ConfigurationAndPlatformService.GetOperatingSystemAndDistributionName();
    }

    ConfigurationAndPlatformService _ConfigurationAndPlatformService;

    [ObservableProperty]
    private string operatingSystemAndDistributionName = string.Empty;
}
