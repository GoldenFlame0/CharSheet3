using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CharSheet3.Services;
using CharSheet3.Structures;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CharSheet3.ViewModels;
public partial class ClassBuilderViewModel : ViewModelBase
{
    public ClassBuilderViewModel(CharacterClassDataService characterClassDataService)
    {
        _CharacterClassDataService = characterClassDataService;

        ClassesList = new (_CharacterClassDataService.ClassList);
        if (ClassesList.Count != 0)
        {
            SelectedClass = ClassesList.First();
        }
    }

    public override void OnActivate()
    {
        base.OnActivate();
        _CharacterClassDataService.LoadClassData();
        ClassesList = new(_CharacterClassDataService.ClassList);
        if (ClassesList.Count != 0)
        {
            SelectedClass = ClassesList.First();
        }
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        // Save classes to the data service when deactivating
        _CharacterClassDataService.ClassList = [.. ClassesList];
        _CharacterClassDataService.SaveClassData();
    }

    CharacterClassDataService _CharacterClassDataService;

    public ObservableCollection<Class5e> ClassesList { get; set; }

    [ObservableProperty]
    private Class5e? selectedClass = null;

    // todo handle duplicates.

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
