using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using CharSheet3.Utilities;

namespace CharSheet3.Controls;

public partial class ProficiencyIndicator : UserControl
{
    public static readonly StyledProperty<Calculators.Proficiency> ProficiencyLevelProperty =
        AvaloniaProperty.Register<ProficiencyIndicator, Calculators.Proficiency>(
            nameof(ProficiencyLevel), defaultValue: Calculators.Proficiency.None);

    public Calculators.Proficiency ProficiencyLevel
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
            Calculators.Proficiency.None => Calculators.Proficiency.Proficient,
            Calculators.Proficiency.Proficient => Calculators.Proficiency.Expertise,
            _ => Calculators.Proficiency.None
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
            Calculators.Proficiency.Proficient => "P",
            Calculators.Proficiency.Expertise => "E",
            _ => "",
        };
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        UpdateVisual();
    }
}