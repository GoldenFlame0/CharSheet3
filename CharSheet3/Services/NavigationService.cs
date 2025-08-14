using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using CharSheet3.ViewModels;

namespace CharSheet3.Services;

/// <summary>
/// Handles navigation between different pages in the application, including lifetime of the page.
/// Lives in the MainViewModel.
/// </summary>
public class NavigationService()
{
    public NavigationService
    (
        CharacterClassDataService characterClassDataService,
        ConfigurationService configurationService
    ) : this()
    {
        _CharacterClassDataService = characterClassDataService;
        _ConfigurationService = configurationService;
        BuildViewModels();
    }

    private CharacterClassDataService _CharacterClassDataService;
    private ConfigurationService _ConfigurationService;

    private readonly Dictionary<Type, ViewModelBase> _viewModelInstances = [];

    private void BuildViewModels()
    {
        _viewModelInstances.Add(typeof(CharacterSheetViewModel), new CharacterSheetViewModel(_ConfigurationService, _CharacterClassDataService));
        _viewModelInstances.Add(typeof(ClassBuilderViewModel), new ClassBuilderViewModel(_CharacterClassDataService));
    }

    public ViewModelBase? GetViewModel(Type viewModelType)
    {
        if (_viewModelInstances.TryGetValue(viewModelType, out var viewModel))
        {
            viewModel.OnActivate(); // Activate the ViewModel if it exists
            return viewModel;
        }
        // Stuff below shouldn't be needed.

        // If the ViewModel is not found, create a new instance and store it
        viewModel = (ViewModelBase)Activator.CreateInstance(viewModelType, [_ConfigurationService])!;

        // Store the new instance in the dictionary
        _viewModelInstances[viewModelType] = viewModel;
        viewModel.OnActivate();
        return viewModel;
    }
}

