using System.Collections.Generic;

namespace CharSheet3.Models;

/// <summary>
/// For calculating spell slots.
/// </summary>
public enum CasterType
{
    None, // No magic caster
    Subclass, // Subclass caster (Eldritch Knight, Arcane Trickster, etc.)
    Half, // Half caster (Paladin, Ranger, Artificer)
    Full, // Full caster (Wizard, Cleric, Druid, Sorcerer)
    Warlock, // Warlock (unique spell slot progression)
    // No, we're not going to add a way of doing spell slot progression that doesn't fit into the above categories.
}

/// <summary>
/// I want to use this at some point.
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

/// <summary>
/// Record-only class type for selecting in the character sheet and going in the builder.
/// Choices become part of the character object.
/// </summary>
public class Class5e
{
    public string Name { get; set; } = "New Class";
    public string CopyrightInfo { get; set; } = "Insert Authorship Here";
    public int HitDie { get; set; } = 8; // Default to D8
    public CasterType CasterType { get; set; } = CasterType.None; // Default to no magic caster
    public List<ClassFeature5e> Features { get; set; } = [];
    public List<Subclass5e> Subclasses { get; set; } = [];
}

public class Subclass5e
{
    public string Name { get; set; } = "New Subclass";
    public string CopyrightInfo { get; set; } = "Insert Authorship Here";
    public bool AddsCasting { get; set; } = false; // Defines if the PC becomes a subclass caster or not.
    public List<ClassFeature5e> Features { get; set; } = [];
}

public class ClassFeature5e
{
    public string Name { get; set; } = "New Feature";
    public string CopyrightInfo { get; set; } = "Insert Authorship Here";
    public string Description { get; set; } = "Feature description goes here.";
    public int Level { get; set; } = 1;
}

