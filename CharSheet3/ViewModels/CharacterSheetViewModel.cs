using System;
using System.Linq;
using System.Threading.Tasks;

using CharSheet3.Services;
using CharSheet3.Structures;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using static CharSheet3.Structures.FundamentalEnums5e;
using static CharSheet3.Utilities.Calculators;

namespace CharSheet3.ViewModels;

public partial class CharacterSheetViewModel : ViewModelBase
{
    Character5e characterData;

    public CharacterSheetViewModel
    (
        ConfigurationService configurationService,
        CharacterClassDataService characterClassDataService
    )
    {
        _CharacterClassDataService = characterClassDataService;
        _ConfigurationService = configurationService;

        characterData = new();
        UpdateAllFromModel();
    }

    CharacterClassDataService _CharacterClassDataService;
    ConfigurationService _ConfigurationService;

    #region Properties
    #region Top-Level Character Data

    [ObservableProperty]
    private string characterName = "";
    partial void OnCharacterNameChanged(string value)
    {
        characterData.CharacterName = value;
    }

    [ObservableProperty]
    private int level;
    //partial void OnLevelChanged(int value)
    //{
    //    characterData.Level = value;
    //    UpdateAllFromModel();
    //}

    #endregion

    #region Ability Scores and Modifiers
    [ObservableProperty]
    private int strengthScore;
    partial void OnStrengthScoreChanged(int value)
    {
        characterData.StrengthScore = value;
        UpdateStrengthFromModel();
    }

    [ObservableProperty]
    private int strengthModifier;

    [ObservableProperty]
    private int dexterityScore;
    partial void OnDexterityScoreChanged(int value)
    {
        characterData.DexterityScore = value;
        UpdateDexterityFromModel();
    }

    [ObservableProperty]
    private int dexterityModifier;

    [ObservableProperty]
    private int constitutionScore;
    partial void OnConstitutionScoreChanged(int value)
    {
        characterData.ConstitutionScore = value;
        UpdateConstitutionFromModel();
    }

    [ObservableProperty]
    private int constitutionModifier;

    [ObservableProperty]
    private int intelligenceScore;
    partial void OnIntelligenceScoreChanged(int value)
    {
        characterData.IntelligenceScore = value;
        UpdateIntelligenceFromModel();
    }

    [ObservableProperty]
    private int intelligenceModifier;

    [ObservableProperty]
    private int wisdomScore;
    partial void OnWisdomScoreChanged(int value)
    {
        characterData.WisdomScore = value;
        UpdateWisdomFromModel();
    }

    [ObservableProperty]
    private int wisdomModifier;

    [ObservableProperty]
    private int charismaScore;
    partial void OnCharismaScoreChanged(int value)
    {
        characterData.CharismaScore = value;
        UpdateCharismaFromModel();
    }

    [ObservableProperty]
    private int charismaModifier;
    #endregion

    #region Skills and Saving Throws

    // --- Strength ---
    [ObservableProperty]
    private SkillProficiency strengthSaves;
    partial void OnStrengthSavesChanged(SkillProficiency value)
    {
        characterData.StrengthSaves = value;
        StrengthSavesModifier = CalculateProficiencyBonus(StrengthModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int strengthSavesModifier;

    [ObservableProperty]
    private SkillProficiency athletics;
    partial void OnAthleticsChanged(SkillProficiency value)
    {
        characterData.Athletics = value;
        AthleticsModifier = CalculateProficiencyBonus(StrengthModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int athleticsModifier;

    // --- Dexterity ---
    [ObservableProperty]
    private SkillProficiency dexteritySaves;
    partial void OnDexteritySavesChanged(SkillProficiency value)
    {
        characterData.DexteritySaves = value;
        DexteritySavesModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int dexteritySavesModifier;

    [ObservableProperty]
    private SkillProficiency acrobatics;
    partial void OnAcrobaticsChanged(SkillProficiency value)
    {
        characterData.Acrobatics = value;
        AcrobaticsModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int acrobaticsModifier;

    [ObservableProperty]
    private SkillProficiency stealth;
    partial void OnStealthChanged(SkillProficiency value)
    {
        characterData.Stealth = value;
        StealthModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int stealthModifier;

    [ObservableProperty]
    private SkillProficiency sleightOfHand;
    partial void OnSleightOfHandChanged(SkillProficiency value)
    {
        characterData.SleightOfHand = value;
        SleightOfHandModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int sleightOfHandModifier;

    // --- Constitution ---
    [ObservableProperty]
    private SkillProficiency constitutionSaves;
    partial void OnConstitutionSavesChanged(SkillProficiency value)
    {
        characterData.ConstitutionSaves = value;
        ConstitutionSavesModifier = CalculateProficiencyBonus(ConstitutionModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int constitutionSavesModifier;

    // --- Intelligence ---
    [ObservableProperty]
    private SkillProficiency intelligenceSaves;
    partial void OnIntelligenceSavesChanged(SkillProficiency value)
    {
        characterData.IntelligenceSaves = value;
        IntelligenceSavesModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int intelligenceSavesModifier;

    [ObservableProperty]
    private SkillProficiency arcana;
    partial void OnArcanaChanged(SkillProficiency value)
    {
        characterData.Arcana = value;
        ArcanaModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int arcanaModifier;

    [ObservableProperty]
    private SkillProficiency history;
    partial void OnHistoryChanged(SkillProficiency value)
    {
        characterData.History = value;
        HistoryModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int historyModifier;

    [ObservableProperty]
    private SkillProficiency investigation;
    partial void OnInvestigationChanged(SkillProficiency value)
    {
        characterData.Investigation = value;
        InvestigationModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int investigationModifier;

    [ObservableProperty]
    private SkillProficiency nature;
    partial void OnNatureChanged(SkillProficiency value)
    {
        characterData.Nature = value;
        NatureModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int natureModifier;

    [ObservableProperty]
    private SkillProficiency religion;
    partial void OnReligionChanged(SkillProficiency value)
    {
        characterData.Religion = value;
        ReligionModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int religionModifier;

    // --- Wisdom ---
    [ObservableProperty]
    private SkillProficiency wisdomSaves;
    partial void OnWisdomSavesChanged(SkillProficiency value)
    {
        characterData.WisdomSaves = value;
        WisdomSavesModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int wisdomSavesModifier;

    [ObservableProperty]
    private SkillProficiency animalHandling;
    partial void OnAnimalHandlingChanged(SkillProficiency value)
    {
        characterData.AnimalHandling = value;
        AnimalHandlingModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int animalHandlingModifier;

    [ObservableProperty]
    private SkillProficiency insight;
    partial void OnInsightChanged(SkillProficiency value)
    {
        characterData.Insight = value;
        InsightModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int insightModifier;

    [ObservableProperty]
    private SkillProficiency medicine;
    partial void OnMedicineChanged(SkillProficiency value)
    {
        characterData.Medicine = value;
        MedicineModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int medicineModifier;

    [ObservableProperty]
    private SkillProficiency perception;
    partial void OnPerceptionChanged(SkillProficiency value)
    {
        characterData.Perception = value;
        PerceptionModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int perceptionModifier;

    [ObservableProperty]
    private SkillProficiency survival;
    partial void OnSurvivalChanged(SkillProficiency value)
    {
        characterData.Survival = value;
        SurvivalModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int survivalModifier;

    // --- Charisma ---
    [ObservableProperty]
    private SkillProficiency charismaSaves;
    partial void OnCharismaSavesChanged(SkillProficiency value)
    {
        characterData.CharismaSaves = value;
        CharismaSavesModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int charismaSavesModifier;

    [ObservableProperty]
    private SkillProficiency deception;
    partial void OnDeceptionChanged(SkillProficiency value)
    {
        characterData.Deception = value;
        DeceptionModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int deceptionModifier;

    [ObservableProperty]
    private SkillProficiency intimidation;
    partial void OnIntimidationChanged(SkillProficiency value)
    {
        characterData.Intimidation = value;
        IntimidationModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int intimidationModifier;

    [ObservableProperty]
    private SkillProficiency performance;
    partial void OnPerformanceChanged(SkillProficiency value)
    {
        characterData.Performance = value;
        PerformanceModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int performanceModifier;

    [ObservableProperty]
    private SkillProficiency persuasion;
    partial void OnPersuasionChanged(SkillProficiency value)
    {
        characterData.Persuasion = value;
        PersuasionModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, value);
    }
    [ObservableProperty]
    private int persuasionModifier;

    #endregion
    #endregion

    [RelayCommand]
    public async Task SaveCharacterData()
    {
        var location = await this.OpenSaveDialogAsync(title: "Save Character Data", suggestedName: characterData.CharacterName, defaultExtension: "xml");

        Utilities.ExportImport.ExportToXmlFile(characterData, location);
    }

    [RelayCommand]
    public async Task LoadCharacterData()
    {
        var location = await this.OpenFileDialogAsync(title: "Load Character Data", selectMany: false);
        if (location == null || !location.Any())
        {
            // Handle the case where no file was selected
            Console.WriteLine("No file selected for loading character data.");
            return;
        }
        Character5e? loadedCharacter = Utilities.ExportImport.ImportFromXmlFileSafe<Character5e?>(location.First());
        if (loadedCharacter != null)
        {
            characterData = loadedCharacter;
            UpdateAllFromModel();
        }
        else
        {
            // Handle the case where loading fails, e.g., show an error message
            Console.WriteLine("Failed to load character data from XML.");
        }
    }

    [RelayCommand]
    public void NewCharacter()
    {
        characterData = new Character5e();
        // Reset all properties to default values
        UpdateAllFromModel();
    }

    #region Updaters

    public void UpdateAllFromModel()
    {
        CharacterName = characterData.CharacterName;

        Level = characterData.Level;
        UpdateStrengthFromModel();
        UpdateDexterityFromModel();
        UpdateConstitutionFromModel();
        UpdateIntelligenceFromModel();
        UpdateWisdomFromModel();
        UpdateCharismaFromModel();
    }

    public void UpdateStrengthFromModel()
    {
        StrengthScore = characterData.StrengthScore;
        StrengthModifier = CalculateModifier(StrengthScore);
        StrengthSaves = characterData.StrengthSaves;
        StrengthSavesModifier = CalculateProficiencyBonus(StrengthModifier, characterData.Level, characterData.StrengthSaves);
        Athletics = characterData.Athletics;
        AthleticsModifier = CalculateProficiencyBonus(StrengthModifier, characterData.Level, characterData.Athletics);
    }

    public void UpdateDexterityFromModel()
    {
        DexterityScore = characterData.DexterityScore;
        DexterityModifier = CalculateModifier(DexterityScore);
        DexteritySaves = characterData.DexteritySaves;
        DexteritySavesModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, characterData.DexteritySaves);
        Acrobatics = characterData.Acrobatics;
        AcrobaticsModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, characterData.Acrobatics);
        Stealth = characterData.Stealth;
        StealthModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, characterData.Stealth);
        SleightOfHand = characterData.SleightOfHand;
        SleightOfHandModifier = CalculateProficiencyBonus(DexterityModifier, characterData.Level, characterData.SleightOfHand);
    }

    public void UpdateConstitutionFromModel()
    {
        ConstitutionScore = characterData.ConstitutionScore;
        ConstitutionModifier = CalculateModifier(ConstitutionScore);
        ConstitutionSaves = characterData.ConstitutionSaves;
        ConstitutionSavesModifier = CalculateProficiencyBonus(ConstitutionModifier, characterData.Level, characterData.ConstitutionSaves);
    }

    public void UpdateIntelligenceFromModel()
    {
        IntelligenceScore = characterData.IntelligenceScore;
        IntelligenceModifier = CalculateModifier(IntelligenceScore);
        IntelligenceSaves = characterData.IntelligenceSaves;
        IntelligenceSavesModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.IntelligenceSaves);
        Arcana = characterData.Arcana;
        ArcanaModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.Arcana);
        History = characterData.History;
        HistoryModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.History);
        Investigation = characterData.Investigation;
        InvestigationModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.Investigation);
        Nature = characterData.Nature;
        NatureModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.Nature);
        Religion = characterData.Religion;
        ReligionModifier = CalculateProficiencyBonus(IntelligenceModifier, characterData.Level, characterData.Religion);
    }

    public void UpdateWisdomFromModel()
    {
        WisdomScore = characterData.WisdomScore;
        WisdomModifier = CalculateModifier(WisdomScore);
        WisdomSaves = characterData.WisdomSaves;
        WisdomSavesModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.WisdomSaves);
        AnimalHandling = characterData.AnimalHandling;
        AnimalHandlingModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.AnimalHandling);
        Insight = characterData.Insight;
        InsightModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.Insight);
        Medicine = characterData.Medicine;
        MedicineModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.Medicine);
        Perception = characterData.Perception;
        PerceptionModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.Perception);
        Survival = characterData.Survival;
        SurvivalModifier = CalculateProficiencyBonus(WisdomModifier, characterData.Level, characterData.Survival);
    }

    public void UpdateCharismaFromModel()
    {
        CharismaScore = characterData.CharismaScore;
        CharismaModifier = CalculateModifier(CharismaScore);
        CharismaSaves = characterData.CharismaSaves;
        CharismaSavesModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, characterData.CharismaSaves);
        Deception = characterData.Deception;
        DeceptionModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, characterData.Deception);
        Intimidation = characterData.Intimidation;
        IntimidationModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, characterData.Intimidation);
        Performance = characterData.Performance;
        PerformanceModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, characterData.Performance);
        Persuasion = characterData.Persuasion;
        PersuasionModifier = CalculateProficiencyBonus(CharismaModifier, characterData.Level, characterData.Persuasion);
    }

    #endregion
}
