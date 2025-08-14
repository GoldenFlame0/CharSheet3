using System;
using System.Collections.Generic;

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
    public List<KeyValuePair<string, uint>> Classes { get; set; } = []; // Class name and level pairs

    #region Ability Scores
    public int StrengthScore { get; set; } = 10;
    public int DexterityScore { get; set; } = 10;
    public int ConstitutionScore { get; set; } = 10;
    public int IntelligenceScore { get; set; } = 10;
    public int WisdomScore { get; set; } = 10;
    public int CharismaScore { get; set; } = 10;
    #endregion

    #region Skills
    // str
    public Proficiency StrengthSaves { get; set; } = Proficiency.None;
    public Proficiency Athletics { get; set; } = Proficiency.None;
    // dex
    public Proficiency DexteritySaves { get; set; } = Proficiency.None;
    public Proficiency Acrobatics { get; set; } = Proficiency.None;
    public Proficiency Stealth { get; set; } = Proficiency.None;
    public Proficiency SleightOfHand { get; set; } = Proficiency.None;
    // con
    public Proficiency ConstitutionSaves { get; set; } = Proficiency.None;
    // int
    public Proficiency IntelligenceSaves { get; set; } = Proficiency.None;
    public Proficiency Arcana { get; set; } = Proficiency.None;
    public Proficiency History { get; set; } = Proficiency.None;
    public Proficiency Investigation { get; set; } = Proficiency.None;
    public Proficiency Nature { get; set; } = Proficiency.None;
    public Proficiency Religion { get; set; } = Proficiency.None;
    // wis
    public Proficiency WisdomSaves { get; set; } = Proficiency.None;
    public Proficiency AnimalHandling { get; set; } = Proficiency.None;
    public Proficiency Insight { get; set; } = Proficiency.None;
    public Proficiency Medicine { get; set; } = Proficiency.None;
    public Proficiency Perception { get; set; } = Proficiency.None;
    public Proficiency Survival { get; set; } = Proficiency.None;
    // cha
    public Proficiency CharismaSaves { get; set; } = Proficiency.None;
    public Proficiency Deception { get; set; } = Proficiency.None;
    public Proficiency Intimidation { get; set; } = Proficiency.None;
    public Proficiency Performance { get; set; } = Proficiency.None;
    public Proficiency Persuasion { get; set; } = Proficiency.None;

    #endregion
}
