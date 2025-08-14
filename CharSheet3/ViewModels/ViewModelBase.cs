using System.Text.RegularExpressions;

using CharSheet3.Services;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CharSheet3.ViewModels;

public class ViewModelBase : ObservableObject
{
    public virtual void OnActivate() { }

    public virtual void OnDeactivate() { }
}
