using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;

using CharSheet3.ViewModels;
using CharSheet3.Views;

namespace CharSheet3;

public partial class App : Application
{
    public MainViewModel _mainViewModel;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        // I feel like this line might need to be earlier.
        RegisterViewmodelsAndServices();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // NB: desktop.MainWindow is an Avalonia thing, but the MainWindow class is in our project.
            desktop.MainWindow = new MainWindow
            {
                DataContext = _mainViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = _mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void RegisterViewmodelsAndServices()
    {
        _mainViewModel = new();
    }
}
