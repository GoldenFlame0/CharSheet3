using CommunityToolkit.Mvvm.ComponentModel;

namespace CharSheet3.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
    }

    [ObservableProperty]
    private CharacterViewModel characterViewModel = new();
}
