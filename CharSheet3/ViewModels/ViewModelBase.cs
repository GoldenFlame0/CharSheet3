using System.Text.RegularExpressions;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CharSheet3.ViewModels;

public class ViewModelBase : ObservableObject
{
    public string GetHumanClassName()
    {
        // https://stackoverflow.com/a/272809
        return Regex.Replace(GetType().Name.Replace("ViewModel", ""), "([a-z])([A-Z])", "$1 $2");
    }
}
