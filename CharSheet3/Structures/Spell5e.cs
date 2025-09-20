using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CharSheet3.Structures;
public class Spell5e : ObservableObject, IPlayerOption
{
    public string Name { get; set; } = "New Spell";
    public FundamentalEnums5e.Edition Edition { get; set; } = FundamentalEnums5e.Edition.TwentyTwentyFour;
    public string Description { get; set; } = "Spell description goes here.";
    public string Source { get; set; } = "Unknown Source";
    public bool IsSRD { get; set; } = false;
    public string CopyrightInfo { get; set; } = "Insert Authorship Here";
    /// <summary>
    /// TODO this isn't needed. Remove it from IPlayerOption?
    /// </summary>
    public List<ClassFeature5e> Features
    {
        get;
        set;
    } = [];
    int Range { get; set; } = 0; // In feet. 0 = Self
    string Target { get; set; } = "None"; // E.g. "One creature", "30-foot cone", "60-foot line", etc.
    bool VocalComponent { get; set; } = false;
    bool SomaticComponent { get; set; } = false;
    bool MaterialComponent { get; set; } = false;
    string MaterialComponentDescription { get; set; } = "None"; // E.g. "a tiny ball of bat guano and sulfur"
    bool RequiresConcentration { get; set; } = false;
    string CastingTime { get; set; } = "1 Action"; // E.g. "1 Action", "1 Bonus Action", "1 Minute", etc.
    int Level { get; set; } = 0; // 0 = Cantrip
    public string Duration { get; set; } = "Instantaneous"; // E.g. "Instantaneous", "Up to 1 minute", etc.
    FundamentalEnums5e.SpellSchool School { get; set; } = FundamentalEnums5e.SpellSchool.Conjuration;
    // These two help determine where the spell is avaliable to.
    // AvaliableToClasses is preferred, but AvaliableToMagicOrigins is a fallback for custom classes/subclasses.
    List<string> AvailableToClasses { get; set; } = [];
    List<FundamentalEnums5e.MagicOrigin> AvaliableToMagicOrigins { get; set; } = [];
}
