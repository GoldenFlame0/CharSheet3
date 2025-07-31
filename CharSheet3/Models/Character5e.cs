using System;
using System.Collections.Generic;

using static CharSheet3.Utilities.Calculators;

namespace CharSheet3.Models;

/// <summary>
/// A model class representing a D&D 5e character.
/// Should mostly be used for data and some small nicities, manipulation is done in the ViewModels.
/// </summary>
public class Character5e
{
    public string CharacterName { get; set; } = "New Character";
    public int Level { get; set; } = 1;
    public List<KeyValuePair<string, int>> Classes { get; set; } = []; // Class name and level pairs

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

    #region File Import/Export

    // TODO: convert to streams.
    // (This is intended to be the model layer,
    // we shouldn't assume anything about the use-case.)

    /// <summary>
    /// Does what it says on the tin.
    /// </summary>
    public static bool ExportToXml(Character5e character, string filePath)
    {
        try
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Character5e));
            using var writer = new System.IO.StreamWriter(filePath);
            serializer.Serialize(writer, character);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exporting to XML: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Does what it says on the tin.
    /// </summary>
    public static Character5e? ImportFromXml(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
        {
            Console.WriteLine("File path is invalid or file does not exist.");
            return null;
        }

        try
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Character5e));
            using var reader = new System.IO.StreamReader(filePath);
            return (Character5e?)serializer.Deserialize(reader);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error importing from XML: {ex.Message}");
            return null;
        }
    }
    #endregion
}
