using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CharSheet3.ViewModels;
public partial class AboutViewModel : ViewModelBase
{
    public AboutViewModel()
    {
        Strings.Add("Your Operating System:" + _ConfigurationAndPlatformService.GetOperatingSystemAndDistributionName());
    }

    ObservableCollection<string> Strings = new()
    {
    };
}
