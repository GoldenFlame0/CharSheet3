namespace CharSheet3.Structures;
public static class FundamentalEnums5e
{
    public enum Edition
    {
        OriginalRelease,
        TwentyTwentyFour
    }

    public enum AbilityScore
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }

    public enum SkillProficiency
    {
        None,
        Proficient,
        Expertise
    }

    public enum WeaponProficiency
    {
        None,
        Proficient,
        Mastery
    }

    public enum ArmourTier
    {
        Clothing,
        Light,
        Medium,
        Heavy
    }

    public enum SpellSchool
    {
        Abjuration,
        Conjuration,
        Divination,
        Enchantment,
        Evocation,
        Illusion,
        Necromancy,
        Transmutation
    }

    /// <summary>
    /// Mostly an import from Pathfinder, but I'm adding it as a fallback when adding class/subclass and spell options.
    /// Especially when one doesn't know about the other.
    /// </summary>
    public enum MagicOrigin
    {
        None,
        Arcane,
        Divine,
        Primal,
        Occult,
        Psionic,
    }
}
