using System;

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
            Proficiency.Expertise => (profBonus * 2) + scoreBonus, // Double the bonus for expertise
            _ => scoreBonus, // No proficiency, just return the score bonus
        };
    }
}
