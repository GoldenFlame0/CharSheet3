using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CharSheet3.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
    }

    [ObservableProperty]
    private CharacterViewModel characterViewModel = new();

    [ObservableProperty]
    private bool isSidearOpen = true;

    [RelayCommand]
    public async Task ToggleSidebar()
    {
        IsSidearOpen = !IsSidearOpen;
    }
}
