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

        ClassesList = new(_CharacterClassDataService.GetClassList());
        if (ClassesList.Count != 0)
        {
            SelectedClass = ClassesList.First();
        }
    }

    public override void OnActivate()
    {
        base.OnActivate();
        _CharacterClassDataService.LoadClassData();
        ClassesList = new(_CharacterClassDataService.GetClassList());
        if (ClassesList.Count != 0)
        {
            SelectedClass = ClassesList.First();
        }
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        // Save classes to the data service when deactivating
        _CharacterClassDataService.AddToClassList([.. ClassesList]);
        _CharacterClassDataService.SaveClassData();
    }

    CharacterClassDataService _CharacterClassDataService;

    public ObservableCollection<Class5e> ClassesList { get; set; }

    public ObservableCollection<Subclass5e> SubclassesList { get; set; }

    [ObservableProperty]
    private Class5e? selectedClass = null;

    partial void OnSelectedClassChanged(Class5e? value)
    {
        SubclassesList = new (_CharacterClassDataService.GetSubclassList(value?.Name ?? string.Empty));
        SelectedSubclass = SubclassesList.FirstOrDefault();
    }

    [ObservableProperty]
    private Subclass5e? selectedSubclass = null;

    [RelayCommand]
    public void AddClass()
    {
        ClassesList.Add(new Class5e
        {
            Name = "New Class"
        });
    }
}
