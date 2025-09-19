using System;
using System.Collections.Generic;

using static CharSheet3.Structures.FundamentalEnums5e;
using static CharSheet3.Utilities.Calculators;

namespace CharSheet3.Structures;

/// <summary>
/// A model class representing a D&D 5e character.
/// </summary>
public class Character5e
{
    public string CharacterName { get; set; } = "New Character";
    public int Level
    {
        get
        {
            uint output = 0;
            foreach (var classPair in Classes)
            {
                output += classPair.Value; // Sum the levels of all classes
            }
            return (int)output;
        }
    }
    // Yes, I know that the TTRPG world is moving away from calling it "Race", but that word is still going to stick for a while.
    // We're using Race here for simplicity - the View layer can display it as "Ancestry", "Species", or "Lineage" as desired.
    // ...Side note, I dislike "Species" as the replacement. - GFZ
    public string Race { get; set; } = string.Empty; // Freeform, query from existing data as part of viewmodel. String also allows end-user expansion.
    public List<KeyValuePair<string, uint>> Classes { get; set; } = []; // Class name and level pairs.
    public string Background { get; set; } = string.Empty;

    // TODO: Proficiencies should query from the character's Classes.
    // TBD: Do we handle here, in the Model layer, or does the ViewModel do that calculation?

    #region Ability Scores
    public int StrengthScore { get; set; } = 10;
    public int DexterityScore { get; set; } = 10;
    public int ConstitutionScore { get; set; } = 10;
    public int IntelligenceScore { get; set; } = 10;
    public int WisdomScore { get; set; } = 10;
    public int CharismaScore { get; set; } = 10;
    #endregion

    #region Statwise Skills
    // TODO: This is nominally tied to Race/Class/Background. How do we handle that?
    // str
    public SkillProficiency StrengthSaves { get; set; } = SkillProficiency.None;
    public SkillProficiency Athletics { get; set; } = SkillProficiency.None;
    // dex
    public SkillProficiency DexteritySaves { get; set; } = SkillProficiency.None;
    public SkillProficiency Acrobatics { get; set; } = SkillProficiency.None;
    public SkillProficiency Stealth { get; set; } = SkillProficiency.None;
    public SkillProficiency SleightOfHand { get; set; } = SkillProficiency.None;
    // con
    public SkillProficiency ConstitutionSaves { get; set; } = SkillProficiency.None;
    // int
    public SkillProficiency IntelligenceSaves { get; set; } = SkillProficiency.None;
    public SkillProficiency Arcana { get; set; } = SkillProficiency.None;
    public SkillProficiency History { get; set; } = SkillProficiency.None;
    public SkillProficiency Investigation { get; set; } = SkillProficiency.None;
    public SkillProficiency Nature { get; set; } = SkillProficiency.None;
    public SkillProficiency Religion { get; set; } = SkillProficiency.None;
    // wis
    public SkillProficiency WisdomSaves { get; set; } = SkillProficiency.None;
    public SkillProficiency AnimalHandling { get; set; } = SkillProficiency.None;
    public SkillProficiency Insight { get; set; } = SkillProficiency.None;
    public SkillProficiency Medicine { get; set; } = SkillProficiency.None;
    public SkillProficiency Perception { get; set; } = SkillProficiency.None;
    public SkillProficiency Survival { get; set; } = SkillProficiency.None;
    // cha
    public SkillProficiency CharismaSaves { get; set; } = SkillProficiency.None;
    public SkillProficiency Deception { get; set; } = SkillProficiency.None;
    public SkillProficiency Intimidation { get; set; } = SkillProficiency.None;
    public SkillProficiency Performance { get; set; } = SkillProficiency.None;
    public SkillProficiency Persuasion { get; set; } = SkillProficiency.None;

    #endregion

    #region Misc Skills
    public bool LightArmorProficiency { get; set; } = false;
    public bool MediumArmorProficiency { get; set; } = false;
    public bool HeavyArmorProficiency { get; set; } = false;

    public ArmourTier GetHighestArmourProficiency()
    {
        if (HeavyArmorProficiency)
        {
            return ArmourTier.Heavy;
        }
        if (MediumArmorProficiency)
        {
            return ArmourTier.Medium;
        }
        if (LightArmorProficiency)
        {
            return ArmourTier.Light;
        }
        return ArmourTier.Clothing;
    }

    public bool ShieldProficiency { get; set; } = false;
    public bool SimpleWeaponProficiency { get; set; } = false;
    public bool MartialWeaponProficiency { get; set; } = false;
    // Stores both one-off proficiencies (Elf, Wizard) and weapon masteries.
    public List<KeyValuePair<string, WeaponProficiency>> OtherWeaponProficienciesAndMasteries { get; set; } = [];
    public List<string> MiscProficiencies { get; set; } = []; // Tools, languages, vehicles, etc.
    // We can subdivide as needed later. Can even keep MiscProficiencies as a way to return the whole lot.
    public List<InventoryRecord> Inventory { get; set; } = [];
    public DnDMoney Money { get; set; } = new DnDMoney();
    #endregion
}

/// <summary>
/// Use for storing on character sheets.
/// Weight etc can be queried from outside sources as needed.
/// </summary>
public struct InventoryRecord
{
    string name;
    int quantity;
}

/// <summary>
/// Represent an amount of D&D currency.
/// </summary>
public struct DnDMoney
{
    int copper;
    int silver;
    int electrum;
    int gold;
    int platinum;

    /// <summary>
    /// Convert lower denominations to higher ones if possible.
    /// Visual Studio pulled this code out of its ass,
    /// save for the skipElectrum code.
    /// </summary>
    /// <param name="skipElectrum">If you don't want to use electrum.</param>
    public void Rationalize(bool skipElectrum = true)
    {
        if (copper >= 10)
        {
            silver += copper / 10;
            copper = copper % 10;
        }
        if (skipElectrum && silver >= 10)
        {
            gold += silver / 10;
            silver = silver % 10;
        }
        else if (silver >= 5)
        {
            electrum += silver / 5;
            silver = silver % 5;
        }
        if (electrum >= 2)
        {
            // This triggers even if you skip electrum,
            // so if you somehow have electrum... now you have less.
            // TODO: make skip electrum also break this down into silver.
            gold += electrum / 2;
            electrum = electrum % 2;
        }
        if (gold >= 10)
        {
            platinum += gold / 10;
            gold = gold % 10;
        }
    }
}
