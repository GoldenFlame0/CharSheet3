using CommunityToolkit.Mvvm.ComponentModel;

namespace CharSheet3.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private CharacterViewModel characterViewModel = new();

    public MainViewModel()
    {
    }
}
