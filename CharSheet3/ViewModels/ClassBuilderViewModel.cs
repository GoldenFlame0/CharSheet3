using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Avalonia.Controls;

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

    #region enums
    #endregion

    CharacterClassDataService _CharacterClassDataService;

    public ObservableCollection<Class5e> ClassesList { get; set; }

    public ObservableCollection<Subclass5e> SubclassesList { get; set; }

    [ObservableProperty]
    private Class5e? selectedClass = null;

    partial void OnSelectedClassChanged(Class5e? value)
    {
        SelectedClassFeatures = new(value?.Features ?? []);

        SubclassesList = new(_CharacterClassDataService.GetSubclassList(value?.Name ?? string.Empty));
        SelectedSubclass = SubclassesList.FirstOrDefault();
    }

    ObservableCollection<ClassFeature5e> SelectedClassFeatures { get; set; } = [];

    [ObservableProperty]
    private Subclass5e? selectedSubclass = null;

    partial void OnSelectedSubclassChanged(Subclass5e? value)
    {
        if (value == null)
        {
            SelectedSubclassFeatures = [];
            return;
        }
        SelectedSubclassFeatures = new(value.Features);
    }

    ObservableCollection<ClassFeature5e> SelectedSubclassFeatures { get; set; } = [];

    [RelayCommand]
    public void AddClass()
    {
        ClassesList.Add(new Class5e
        {
            Name = "New Class"
        });
    }

    [RelayCommand]
    public void AddSubclass()
    {
        SubclassesList.Add(new Subclass5e
        {
            Name = "New Subclass",
            ParentClass = SelectedClass != null ? SelectedClass.Name : string.Empty
        });
    }
}
