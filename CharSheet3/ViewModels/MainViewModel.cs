using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

using CharSheet3.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CharSheet3.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        _ConfigurationAndPlatformService = new();
        _NavigationService = new(_ConfigurationAndPlatformService);
        SelectedPage = Pages.FirstOrDefault();
    }
    // TODO: DI stuff.
    NavigationService _NavigationService { get; }

    public ObservableCollection<ListItemTemplate> Pages { get; } =
    [
        new ListItemTemplate(typeof(CharacterSheetViewModel), "Character Sheet", "PersonRegular"),
        new ListItemTemplate(typeof(ClassBuilderViewModel), "Class Builder", "TextBulletListAddRegular"),
        new ListItemTemplate(typeof(SettingsViewModel), "Settings", "SettingsRegular"),
        new ListItemTemplate(typeof(AboutViewModel), "About", "InfoRegular"),
    ];

    [ObservableProperty]
    private bool isSidebarOpen = false;

    [ObservableProperty]
    private ViewModelBase? currentPage;

    [ObservableProperty]
    private ListItemTemplate? selectedPage;

    partial void OnSelectedPageChanged(ListItemTemplate? oldValue, ListItemTemplate? newValue)
    {
        if (newValue is null)
        {
            return;
        }
        var instance = _NavigationService.GetViewModel(newValue.ListItemType);
        if (instance is null)
        {
            return;
        }
        CurrentPage = instance as ViewModelBase;
    }

    [RelayCommand]
    public async Task ToggleSidebar()
    {
        IsSidebarOpen = !IsSidebarOpen;
    }
}

// https://youtu.be/UDbKVheMBY8?si=8IoywR7pgq_v1lTA
public class ListItemTemplate
{
    public ListItemTemplate(Type type, string label, string iconKey)
    {
        ListItemType = type;
        Label = label;
        Application.Current!.TryFindResource(iconKey, out var res);
        Icon = res as StreamGeometry ?? throw new InvalidOperationException($"Icon with key '{iconKey}' not found.");
    }

    public Type ListItemType { get; }
    public string Label { get; }
    public StreamGeometry Icon { get; }
}