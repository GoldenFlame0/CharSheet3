using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CharSheet3.Models;
using CharSheet3.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CharSheet3.ViewModels;
public partial class ClassBuilderViewModel : ViewModelBase
{
    public ObservableCollection<Class5e> ClassesList { get; set; }

    [ObservableProperty]
    private Class5e selectedClass;

    public ClassBuilderViewModel(ConfigurationAndPlatformService configurationAndPlatformService)
    {
        _ConfigurationAndPlatformService = configurationAndPlatformService;

        ClassesList = new ObservableCollection<Class5e>(
        [
            // Initialize with some default classes if needed
            new Class5e { Name = "Fighter" },
            new Class5e { Name = "Wizard" },
        ]);
        SelectedClass = ClassesList.First();
    }

    ConfigurationAndPlatformService _ConfigurationAndPlatformService;

    [RelayCommand]
    public void AddClass()
    {
        ClassesList.Add(new Class5e
        {
            Name = "New Class"
        });
    }

    [RelayCommand]
    public void SortClasses()
    {
        var sortedList = ClassesList.OrderBy(c => c.Name).ToList();
        ClassesList.Clear();
        foreach (var cls in sortedList)
        {
            ClassesList.Add(cls);
        }
    }
}
