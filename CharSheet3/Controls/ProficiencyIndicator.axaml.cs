using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

using static CharSheet3.Structures.FundamentalEnums5e;

namespace CharSheet3.Controls;

public partial class ProficiencyIndicator : UserControl
{
    public static readonly StyledProperty<Proficiency> ProficiencyLevelProperty =
        AvaloniaProperty.Register<ProficiencyIndicator, Proficiency>(
            nameof(ProficiencyLevel), defaultValue: Proficiency.None);

    public Proficiency ProficiencyLevel
    {
        get => GetValue(ProficiencyLevelProperty);
        set => SetValue(ProficiencyLevelProperty, value);
    }

    public ProficiencyIndicator()
    {
        InitializeComponent();
        UpdateVisual();
        PointerPressed += OnPointerPressed;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        ProficiencyLevel = ProficiencyLevel switch
        {
            Proficiency.None => Proficiency.Proficient,
            Proficiency.Proficient => Proficiency.Expertise,
            _ => Proficiency.None
        };
    }

    private void UpdateVisual()
    {
        // https://stackoverflow.com/a/76275562
        var nameScope = this.FindNameScope();
        if (nameScope == null)
        {
            return;
        }
        TextBlock? textBlock = nameScope.Find<TextBlock>("IndicatorText");
        if (textBlock == null)
        {
            return;
        }
        textBlock.Text = ProficiencyLevel switch
        {
            Proficiency.Proficient => "P",
            Proficiency.Expertise => "E",
            _ => "",
        };
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        UpdateVisual();
    }
}