using System.Text.RegularExpressions;

using CharSheet3.Services;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CharSheet3.ViewModels;

public class ViewModelBase : ObservableObject
{
    public ConfigurationAndPlatformService _ConfigurationAndPlatformService { get; set; } = new();

    public string GetHumanClassName()
    {
        // https://stackoverflow.com/a/272809
        return Regex.Replace(GetType().Name.Replace("ViewModel", ""), "([a-z])([A-Z])", "$1 $2");
    }
}
