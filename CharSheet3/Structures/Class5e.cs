using System.Collections.Generic;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CharSheet3.Structures;

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

public enum Edition
{
    OriginalRelease,
    TwentyTwentyFour
}

public enum ClassGroup
{
    None,
    Expert,
    Mage,
    Priest,
    Warrior
}

public interface IPlayerOption
{
    /// <summary>
    /// The name of the option.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Which release of 5th edition this option is for (2014 or 2024).
    /// </summary>
    public Edition Edition { get; set; }
    /// <summary>
    /// A short description of the option. Mostly fluff.
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// The source book, pdf, whatever this option comes from.
    /// </summary>
    public string Source { get; set; }
    /// <summary>
    /// Is this option part of the System Reference Document (SRD) or is it "homebrew"?
    /// Important since SRD options are delivered with the application.
    /// </summary>
    public bool IsSRD { get; set; }
    /// <summary>
    /// Copyright information for the option.
    /// </summary>
    public string CopyrightInfo { get; set; }
    /// <summary>
    /// A set of features that this option provides.
    /// </summary>
    public List<ClassFeature5e> Features { get; set; }
}

/// <summary>
/// Record-only class type for selecting in the character sheet and going in the builder.
/// Choices become part of the character object.
/// </summary>
public class Class5e : ObservableObject, IPlayerOption
{
    public string Name { get; set; } = "New Option";
    public Edition Edition { get; set; } = Edition.TwentyTwentyFour;
    public string Description { get; set; } = "Option description goes here.";
    public string Source { get; set; } = "Source book goes here."; // The source book this option comes from.
    public bool IsSRD { get; set; } = false; // True if this is a standard SRD option, false if it's homebrew.
    public string CopyrightInfo { get; set; } = "Insert Authorship Here";
    public int HitDie { get; set; } = 8; // Default to D8
    public ClassGroup ClassGroup { get; set; } = ClassGroup.None;
    public CasterType CasterType { get; set; } = CasterType.None;
    public List<ClassFeature5e> Features { get; set; } = [];
}

public class Subclass5e : ObservableObject, IPlayerOption
{
    public string Name { get; set; } = "New Option";
    public Edition Edition { get; set; } = Edition.TwentyTwentyFour;
    public string Description { get; set; } = "Option description goes here.";
    public string Source { get; set; } = "Source book goes here."; // The source book this option comes from.
    public bool IsSRD { get; set; } = false; // True if this is a standard SRD option, false if it's homebrew.
    public string CopyrightInfo { get; set; } = "Insert Authorship Here";
    public string ParentClass { get; set; } = "New Class"; // The class this subclass belongs to.
    public bool AddsCasting { get; set; } = false; // Defines if the PC becomes a subclass caster or not.
    public List<ClassFeature5e> Features { get; set; } = [];
}

public class ClassFeature5e : ObservableObject
{
    public string Name { get; set; } = "New Feature";
    /// <summary>
    /// Description of the feature. Each line is a separate string in the list.
    /// </summary>
    public string Description { get; set; } = "Feature description goes here.";
    public int Level { get; set; } = 1;
}
