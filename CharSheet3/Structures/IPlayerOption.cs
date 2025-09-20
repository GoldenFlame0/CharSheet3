using System.Collections.Generic;

using static CharSheet3.Structures.FundamentalEnums5e;

namespace CharSheet3.Structures;
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
