using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

using static CharSheet3.Structures.FundamentalEnums5e;

namespace CharSheet3.Controls;

public partial class ProficiencyIndicator : UserControl
{
    public static readonly StyledProperty<SkillProficiency> ProficiencyLevelProperty =
        AvaloniaProperty.Register<ProficiencyIndicator, SkillProficiency>(
            nameof(ProficiencyLevel), defaultValue: SkillProficiency.None);

    public SkillProficiency ProficiencyLevel
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
            SkillProficiency.None => SkillProficiency.Proficient,
            SkillProficiency.Proficient => SkillProficiency.Expertise,
            _ => SkillProficiency.None
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
            SkillProficiency.Proficient => "P",
            SkillProficiency.Expertise => "E",
            _ => "",
        };
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        UpdateVisual();
    }
}