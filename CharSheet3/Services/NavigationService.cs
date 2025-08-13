using System;
using System.Collections.Generic;

using CharSheet3.ViewModels;

namespace CharSheet3.Services;

/// <summary>
/// Handles navigation between different pages in the application, including lifetime of the page.
/// Lives in the MainViewModel.
/// </summary>
public class NavigationService()
{
    private readonly Dictionary<Type, ViewModelBase> _viewModelInstances = [];

    public NavigationService(ConfigurationService configurationAndPlatformService) : this()
    {
        _ConfigurationAndPlatformService = configurationAndPlatformService;
    }

    private readonly ConfigurationService? _ConfigurationAndPlatformService;

    public ViewModelBase? GetViewModel(Type viewModelType)
    {
        if (_viewModelInstances.TryGetValue(viewModelType, out var viewModel))
        {
            return viewModel;
        }
        // If the ViewModel is not found, create a new instance and store it
        viewModel = (ViewModelBase)Activator.CreateInstance(viewModelType, [_ConfigurationAndPlatformService])!;

        // Store the new instance in the dictionary
        _viewModelInstances[viewModelType] = viewModel;
        return viewModel;
    }
}

