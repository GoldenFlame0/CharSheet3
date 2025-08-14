using System.Linq;
using System.Threading.Tasks;

using CharSheet3.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CharSheet3.ViewModels;
public partial class SettingsViewModel : ViewModelBase
{
    public SettingsViewModel(ConfigurationService configurationService)
    {
        // Initialize the properties with values from the settings service
        _ConfigurationService = configurationService;
        // todo should probably do this dynamically
        ClassDataPath = _ConfigurationService.CharacterClassDataPath;
    }

    ConfigurationService _ConfigurationService;

    [ObservableProperty]
    private string classDataPath = string.Empty;
    partial void OnClassDataPathChanged(string value)
    {
        if (string.IsNullOrEmpty(value) || !System.IO.Directory.Exists(value))
        {
            IsClassDataPathValid = false;
        }
        else
        {
            IsClassDataPathValid = true;
        }
        _ConfigurationService.CharacterClassDataPath = value;
    }

    [ObservableProperty]
    private bool isClassDataPathValid = true;

    [RelayCommand]
    public async Task BrowseClassData()
    {
        var output = await this.OpenFolderDialogAsync("Select Class Data Directory");
        ClassDataPath = output?.FirstOrDefault() ?? string.Empty;
    }
}
