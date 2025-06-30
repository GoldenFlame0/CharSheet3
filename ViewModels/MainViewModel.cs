using CharSheet3.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection;
using System.Security;
using static CharSheet3.Utilities.Calculators;

namespace CharSheet3.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    Character5e characterData;

    public MainViewModel()
    {
        characterData = new();
        // Initialize properties from characterData

        Level = characterData.Level;

        StrengthScore = characterData.StrengthScore;
        DexterityScore = characterData.DexterityScore;
        ConstitutionScore = characterData.ConstitutionScore;
        IntelligenceScore = characterData.IntelligenceScore;
        WisdomScore = characterData.WisdomScore;
        CharismaScore = characterData.CharismaScore;
    }

    #region Character Name

    [ObservableProperty]
    private string characterName = "New Character";
    partial void OnCharacterNameChanged(string value)
    {
        characterData.CharacterName = value;
    }

    [ObservableProperty]
    private int level = 1;
    partial void OnLevelChanged(int value)
    {
        characterData.Level = value;
    }

    #endregion

    #region Ability Scores and Modifiers
    [ObservableProperty]
    private int strengthScore;
    partial void OnStrengthScoreChanged(int value)
    {
        characterData.StrengthScore = value;
        StrengthModifier = CalculateModifier(value);
        StrengthSavesModifier = CalculateProficiencyBonus(StrengthModifier, characterData.Level, characterData.StrengthSaves);
        AthleticsModifier = CalculateProficiencyBonus(StrengthModifier, characterData.Level, characterData.Athletics);
    }

    [ObservableProperty]
    private int strengthModifier;

    [ObservableProperty]
    private int dexterityScore;
    partial void OnDexterityScoreChanged(int value)
    {
        characterData.DexterityScore = value;
        DexterityModifier = CalculateModifier(value);
        DexteritySavesModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, characterData.DexteritySaves);
        AcrobaticsModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, characterData.Acrobatics);
        StealthModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, characterData.Stealth);
        SleightOfHandModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, characterData.SleightOfHand);
    }

    [ObservableProperty]
    private int dexterityModifier;

    [ObservableProperty]
    private int constitutionScore;
    partial void OnConstitutionScoreChanged(int value)
    {
        characterData.ConstitutionScore = value;
        ConstitutionModifier = CalculateModifier(value);
        ConstitutionSavesModifier = CalculateProficiencyBonus(ConstitutionModifier, characterData.Level, characterData.ConstitutionSaves);
    }

    [ObservableProperty]
    private int constitutionModifier;

    [ObservableProperty]
    private int intelligenceScore;
    partial void OnIntelligenceScoreChanged(int value)
    {
        characterData.IntelligenceScore = value;
        IntelligenceModifier = CalculateModifier(value);
        IntelligenceSavesModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.IntelligenceSaves);
        ArcanaModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.Arcana);
        HistoryModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.History);
        InvestigationModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.Investigation);
        NatureModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.Nature);
        ReligionModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.Religion);
    }

    [ObservableProperty]
    private int intelligenceModifier;

    [ObservableProperty]
    private int wisdomScore;
    partial void OnWisdomScoreChanged(int value)
    {
        characterData.WisdomScore = value;
        WisdomModifier = CalculateModifier(value);
        WisdomSavesModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.WisdomSaves);
        AnimalHandlingModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.AnimalHandling);
        InsightModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.Insight);
        MedicineModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.Medicine);
        PerceptionModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.Perception);
        SurvivalModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.Survival);
    }

    [ObservableProperty]
    private int wisdomModifier;

    [ObservableProperty]
    private int charismaScore;
    partial void OnCharismaScoreChanged(int value)
    {
        characterData.CharismaScore = value;
        CharismaModifier = CalculateModifier(value);
        CharismaSavesModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, characterData.CharismaSaves);
        DeceptionModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, characterData.Deception);
        IntimidationModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, characterData.Intimidation);
        PerformanceModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, characterData.Performance);
        PersuasionModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, characterData.Persuasion);
    }

    [ObservableProperty]
    private int charismaModifier;
    #endregion

    #region Skills and Saving Throws

    // --- Strength ---
    [ObservableProperty]
    private Proficiency strengthSaves;
    partial void OnStrengthSavesChanged(Proficiency value)
    {
        characterData.StrengthSaves = value;
        StrengthSavesModifier = CalculateProficiencyBonus(StrengthModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int strengthSavesModifier;

    [ObservableProperty]
    private Proficiency athletics;
    partial void OnAthleticsChanged(Proficiency value)
    {
        characterData.Athletics = value;
        AthleticsModifier = CalculateProficiencyBonus(StrengthModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int athleticsModifier;

    // --- Dexterity ---
    [ObservableProperty]
    private Proficiency dexteritySaves;
    partial void OnDexteritySavesChanged(Proficiency value)
    {
        characterData.DexteritySaves = value;
        DexteritySavesModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int dexteritySavesModifier;

    [ObservableProperty]
    private Proficiency acrobatics;
    partial void OnAcrobaticsChanged(Proficiency value)
    {
        characterData.Acrobatics = value;
        AcrobaticsModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int acrobaticsModifier;

    [ObservableProperty]
    private Proficiency stealth;
    partial void OnStealthChanged(Proficiency value)
    {
        characterData.Stealth = value;
        StealthModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int stealthModifier;

    [ObservableProperty]
    private Proficiency sleightOfHand;
    partial void OnSleightOfHandChanged(Proficiency value)
    {
        characterData.SleightOfHand = value;
        SleightOfHandModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int sleightOfHandModifier;

    // --- Constitution ---
    [ObservableProperty]
    private Proficiency constitutionSaves;
    partial void OnConstitutionSavesChanged(Proficiency value)
    {
        characterData.ConstitutionSaves = value;
        ConstitutionSavesModifier = CalculateProficiencyBonus(ConstitutionModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int constitutionSavesModifier;

    // --- Intelligence ---
    [ObservableProperty]
    private Proficiency intelligenceSaves;
    partial void OnIntelligenceSavesChanged(Proficiency value)
    {
        characterData.IntelligenceSaves = value;
        IntelligenceSavesModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int intelligenceSavesModifier;

    [ObservableProperty]
    private Proficiency arcana;
    partial void OnArcanaChanged(Proficiency value)
    {
        characterData.Arcana = value;
        ArcanaModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int arcanaModifier;

    [ObservableProperty]
    private Proficiency history;
    partial void OnHistoryChanged(Proficiency value)
    {
        characterData.History = value;
        HistoryModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int historyModifier;

    [ObservableProperty]
    private Proficiency investigation;
    partial void OnInvestigationChanged(Proficiency value)
    {
        characterData.Investigation = value;
        InvestigationModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int investigationModifier;

    [ObservableProperty]
    private Proficiency nature;
    partial void OnNatureChanged(Proficiency value)
    {
        characterData.Nature = value;
        NatureModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int natureModifier;

    [ObservableProperty]
    private Proficiency religion;
    partial void OnReligionChanged(Proficiency value)
    {
        characterData.Religion = value;
        ReligionModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int religionModifier;

    // --- Wisdom ---
    [ObservableProperty]
    private Proficiency wisdomSaves;
    partial void OnWisdomSavesChanged(Proficiency value)
    {
        characterData.WisdomSaves = value;
        WisdomSavesModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int wisdomSavesModifier;

    [ObservableProperty]
    private Proficiency animalHandling;
    partial void OnAnimalHandlingChanged(Proficiency value)
    {
        characterData.AnimalHandling = value;
        AnimalHandlingModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int animalHandlingModifier;

    [ObservableProperty]
    private Proficiency insight;
    partial void OnInsightChanged(Proficiency value)
    {
        characterData.Insight = value;
        InsightModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int insightModifier;

    [ObservableProperty]
    private Proficiency medicine;
    partial void OnMedicineChanged(Proficiency value)
    {
        characterData.Medicine = value;
        MedicineModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int medicineModifier;

    [ObservableProperty]
    private Proficiency perception;
    partial void OnPerceptionChanged(Proficiency value)
    {
        characterData.Perception = value;
        PerceptionModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int perceptionModifier;

    [ObservableProperty]
    private Proficiency survival;
    partial void OnSurvivalChanged(Proficiency value)
    {
        characterData.Survival = value;
        SurvivalModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int survivalModifier;

    // --- Charisma ---
    [ObservableProperty]
    private Proficiency charismaSaves;
    partial void OnCharismaSavesChanged(Proficiency value)
    {
        characterData.CharismaSaves = value;
        CharismaSavesModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int charismaSavesModifier;

    [ObservableProperty]
    private Proficiency deception;
    partial void OnDeceptionChanged(Proficiency value)
    {
        characterData.Deception = value;
        DeceptionModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int deceptionModifier;

    [ObservableProperty]
    private Proficiency intimidation;
    partial void OnIntimidationChanged(Proficiency value)
    {
        characterData.Intimidation = value;
        IntimidationModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int intimidationModifier;

    [ObservableProperty]
    private Proficiency performance;
    partial void OnPerformanceChanged(Proficiency value)
    {
        characterData.Performance = value;
        PerformanceModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int performanceModifier;

    [ObservableProperty]
    private Proficiency persuasion;
    partial void OnPersuasionChanged(Proficiency value)
    {
        characterData.Persuasion = value;
        PersuasionModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int persuasionModifier;

    #endregion
}
