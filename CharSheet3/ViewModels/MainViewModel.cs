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
        RegisterServices();
        SelectedPage = Pages.FirstOrDefault();
    }
    private ConfigurationService _ConfigurationService;
    private NavigationService _NavigationService;
    private CharacterClassDataService _CharacterClassDataService;

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
        CurrentPage?.OnDeactivate(); // Deactivate the old page if it exists
        CurrentPage = instance as ViewModelBase;
    }

    [RelayCommand]
    public void ToggleSidebar()
    {
        IsSidebarOpen = !IsSidebarOpen;
    }

    /// <summary>
    /// Create all the services.
    /// This is where they live - viewmodels live in the NavigationService.
    /// </summary>
    public void RegisterServices()
    {
        _ConfigurationService = new()
        {
            // Make sure I'm not going nuts.
            TestString = "This is a test string from the MainViewModel."
        };

        _CharacterClassDataService = new(_ConfigurationService);

        // Poor nav service needs to know about everything else, since it holds the viewmodels.
        _NavigationService = new(_CharacterClassDataService, _ConfigurationService);
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
