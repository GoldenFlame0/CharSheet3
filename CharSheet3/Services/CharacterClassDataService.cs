using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CharSheet3.Structures;
using CharSheet3.Utilities;

namespace CharSheet3.Services;
public class CharacterClassDataService
{
    public CharacterClassDataService(ConfigurationService configurationService)
    {
        _ConfigurationService = configurationService;
        LoadClassData();
    }

    public const string SRDFiveTwoOneCopyrightInfo = "\r\nThis work includes material from the System Reference Document 5.2.1 (“SRD 5.2.1”) by Wizards of the\r\nCoast LLC, available at https://www.dndbeyond.com/srd. The SRD 5.2.1 is licensed under the Creative\r\nCommons Attribution 4.0 International License, available at https://creativecommons.org/licenses/by/4.0/legalcode.\r\n";

    /// <summary>
    /// Stock classes from the SRD (System Reference Document).
    /// TODO: move this from code to a set of files.
    /// TODO: redo all of the AI generated descriptions to be less soulless.
    /// </summary>
    private static readonly List<Class5e> srdClasses =
    [
        new Class5e
        {
            Name = "Barbarian",
            Description = "Great warriors, powered by primal forces, who are able to tap into those forces to enter a battle state known as Rage.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
        new Class5e
        {
            Name = "Bard",
            Description = "A versatile class that uses music and magic to inspire allies, manipulate foes, and create magical effects.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
        new Class5e
        {
            Name = "Cleric",
            Description = "Divine spellcasters who draw power from a deity to heal, protect, and destroy, wielding both magic and weapons.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
        new Class5e
        {
            Name = "Druid",
            Description = "Spellcasters who draw on the power of nature, able to cast spells and transform into animals.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
        new Class5e
        {
            Name = "Fighter",
            Description = "Masters of martial combat, skilled with a variety of weapons and armor, adaptable to many fighting styles.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
        new Class5e
        {
            Name = "Monk",
            Description = "Martial artists who harness the power of ki to perform extraordinary physical and mystical feats.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
        new Class5e
        {
            Name = "Paladin",
            Description = "Holy warriors bound by oath, combining martial prowess with divine magic to protect and heal.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
        new Class5e
        {
            Name = "Ranger",
            Description = "Warriors of the wilderness, skilled in tracking, hunting, and casting nature-based spells.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
        new Class5e
        {
            Name = "Rogue",
            Description = "Stealthy and dexterous adventurers, experts in infiltration, deception, and precision attacks.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
        new Class5e
        {
            Name = "Sorcerer",
            Description = "Spellcasters who wield innate magical power, able to shape and manipulate spells with raw talent.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
        new Class5e
        {
            Name = "Warlock",
            Description = "Casters who gain magical abilities through pacts with powerful patrons, blending eldritch magic and martial skill.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
        new Class5e
        {
            Name = "Wizard",
            Description = "Scholars of arcane magic, able to cast a wide variety of spells through study and preparation.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
        },
    ];
    private List<Class5e> homebrewClasses = [];

    /// <summary>
    /// List of all classes, including both SRD and homebrew classes.
    /// Data saved by the user is stored in the homebrew classes list.
    /// </summary>
    public List<Class5e> ClassList
    {
        get => [.. srdClasses.Concat(homebrewClasses)];
        set
        {
            List<Class5e> toStore = [];
            foreach (var classData in value)
            {
                if (classData == null || string.IsNullOrEmpty(classData.Name))
                {
                    Console.WriteLine("Invalid class data encountered. Skipping.");
                    continue;
                }
                if (classData.IsSRD)
                {
                    Console.WriteLine($"Class {classData.Name} already exists in the SRD classes. Skipping.");
                    continue;
                }
                toStore.Add(classData);
            }
            homebrewClasses = toStore;
        }
    }

    private readonly ConfigurationService _ConfigurationService;

    public void LoadClassData()
    {
        string classDataPath = _ConfigurationService.CharacterClassDataPath;

        // YMMV if these should throw or not.
        if (string.IsNullOrEmpty(classDataPath))
        {
            Console.WriteLine("Character class data path is not set.");
            return;
        }
        if (!Directory.Exists(classDataPath))
        {
            // TODO: probably create the directory if it doesn't exist.
            Console.WriteLine($"The directory {classDataPath} does not exist.");
            return;
        }

        foreach (var file in Directory.GetFiles(classDataPath, "*.xml"))
        {
            Class5e? classData = ExportImport.ImportFromXmlFileSafe<Class5e>(file);
            if (classData != null)
            {
                ClassList.Add(classData);
            }
            else
            {
                Console.WriteLine($"Failed to load class data from {file}.");
            }
        }
    }

    public void SaveClassData()
    {
        string classDataPath = _ConfigurationService.CharacterClassDataPath;
        if (string.IsNullOrEmpty(classDataPath) || !Directory.Exists(classDataPath))
        {
            Console.WriteLine("Character class data path is not set or does not exist.");
            return;
        }
        foreach (var classData in ClassList)
        {
            string filePath = Path.Combine(classDataPath, $"{classData.Name}.xml");
            if (!ExportImport.ExportToXmlFile(classData, filePath))
            {
                Console.WriteLine($"Failed to save class data for {classData.Name} to {filePath}.");
            }
        }
    }
}
