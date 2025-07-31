using System;
using System.Collections.Generic;

using CharSheet3.Models;

namespace CharSheet3.Utilities;

public static class Calculators
{
    public enum Proficiency
    {
        None,
        Proficient,
        Expertise
    }

    public static int CalculateModifier(int input)
    {
        return (int)Math.Floor(((double)input - 10) / 2); // Standard D&D 5e modifier calculation
    }

    public static int CalculateProficiencyBonus(int scoreBonus = 0, int level = 1, Proficiency proficiency = Proficiency.None)
    {
        int profBonus = 2 + (level - 1) / 4; // Proficiency bonus increases every 4 levels
        return proficiency switch
        {
            Proficiency.Proficient => profBonus + scoreBonus, // Add score bonus for proficiency
            Proficiency.Expertise => (profBonus * 2) + scoreBonus, // Double the proficiency bonus for expertise
            _ => scoreBonus, // No proficiency, just return the score bonus
        };
    }

    public static readonly uint[][] MulticlassSpellcastingTable =
    [
        // PHB24, p. 45
        // I could do math based on level, but this just copies the table from the PHB.
        // Bit easier to read hopefully.
        //         1st  2nd  3rd  4th  5th  6th  7th  8th  9th
        /* 0th*/ [  0u,  0u,  0u,  0u,  0u,  0u,  0u,  0u,  0u], // No spell slots at level 0
        /* 1st*/ [  2u,  0u,  0u,  0u,  0u,  0u,  0u,  0u,  0u],
        /* 2nd*/ [  3u,  0u,  0u,  0u,  0u,  0u,  0u,  0u,  0u],
        /* 3rd*/ [  4u,  2u,  0u,  0u,  0u,  0u,  0u,  0u,  0u],
        /* 4th*/ [  4u,  3u,  0u,  0u,  0u,  0u,  0u,  0u,  0u],
        /* 5th*/ [  4u,  3u,  2u,  0u,  0u,  0u,  0u,  0u,  0u],
        /* 6th*/ [  4u,  3u,  3u,  0u,  0u,  0u,  0u,  0u,  0u],
        /* 7th*/ [  4u,  3u,  3u,  1u,  0u,  0u,  0u,  0u,  0u],
        /* 8th*/ [  4u,  3u,  3u,  2u,  0u,  0u,  0u,  0u,  0u],
        /* 9th*/ [  4u,  3u,  3u,  3u,  1u,  0u,  0u,  0u,  0u],
        /*10th*/ [  4u,  3u,  3u,  3u,  2u,  0u,  0u,  0u,  0u],
        /*11th*/ [  4u,  3u,  3u,  3u,  2u,  1u,  0u,  0u,  0u],
        /*12th*/ [  4u,  3u,  3u,  3u,  2u,  1u,  0u,  0u,  0u],
        /*13th*/ [  4u,  3u,  3u,  3u,  2u,  1u,  1u,  0u,  0u],
        /*14th*/ [  4u,  3u,  3u,  3u,  2u,  1u,  1u,  0u,  0u],
        /*15th*/ [  4u,  3u,  3u,  3u,  2u,  1u,  1u,  1u,  0u],
        /*16th*/ [  4u,  3u,  3u,  3u,  2u,  1u,  1u,  1u,  0u],
        /*17th*/ [  4u,  3u,  3u,  3u,  2u,  1u,  1u,  1u,  1u],
        /*18th*/ [  4u,  3u,  3u,  3u,  3u,  1u,  1u,  1u,  1u],
        /*19th*/ [  4u,  3u,  3u,  3u,  3u,  2u,  1u,  1u,  1u],
        /*20th*/ [  4u,  3u,  3u,  3u,  3u,  2u,  2u,  1u,  1u],
        /*21st*/ [  4u,  3u,  3u,  3u,  3u,  2u,  2u,  2u,  1u],
        /*22nd*/ [  4u,  3u,  3u,  3u,  3u,  2u,  2u,  2u,  2u],
        // I could keep going, but it'd be guesswork.
    ];

    public static uint[] CalculateSpellSlots(List<Tuple<CasterType, uint>> characterClasses)
    {
        // Calculate spellcasting level based on character classes.
        uint derivedLevel = 0;
        uint warlockLv = 0;
        foreach (var characterClass in characterClasses)
        {
            switch (characterClass.Item1)
            {
                case CasterType.Full:
                    derivedLevel += characterClass.Item2;
                    break;
                case CasterType.Half:
                    // Should truncate towards zero.
                    derivedLevel += characterClass.Item2 / 2;
                    break;
                case CasterType.Subclass:
                    // Should truncate towards zero.
                    derivedLevel += characterClass.Item2 / 3;
                    break;
                case CasterType.Warlock:
                    // Track warlock level(s) separately, as they have a unique spell slot progression.
                    warlockLv += characterClass.Item2;
                    break;
                default:
                    break;
            }
        }

        if (derivedLevel > MulticlassSpellcastingTable.Length)
        {
            derivedLevel = (uint)MulticlassSpellcastingTable.Length - 1; // Cap it to the maximum defined level in the table (-1 because we index at 0).
        }

        // Set to relevant row of big table. (pls don't be a reference pls don't be a reference pls don't be a reference)
        uint[] output = MulticlassSpellcastingTable[derivedLevel];

        // If we have a warlock, we need to add the warlock spell slots to the output.
        // It's almost regular enough that we can do math, but there's just as many unique cases that it just isn't worth it.
        switch (warlockLv)
        {
            case 0:
                break; // No warlock, no changes.
            case 1:
                output[0] += 1; // 1st level warlock gets 1 1st slot
                break;
            case 2:
                output[0] += 2; // 1st level warlock gets 2 1st slots
                break;
            // Becomes a bit more regular for a bit.
            case 3:
            case 4:
                output[1] += 2;
                break;
            case 5:
            case 6:
                output[2] += 2;
                break;
            case 7:
            case 8:
                output[3] += 2;
                break;
            case 9:
            case 10:
                output[4] += 2;
                break;
            // Warlock stops getting stronger spell slots and starts getting more of them.
            case uint n when n >= 11 && n <= 16:
                output[4] += 3; // dont forget we index at 0, so this is 5th level slots
                break;
            default:
                // 17th level warlock gets 4 5th level slots and it doesn't change past that point.
                output[4] += 4;
                break;
        }

        return output;
    }
}
