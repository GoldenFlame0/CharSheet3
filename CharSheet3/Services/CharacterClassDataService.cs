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
    private readonly ConfigurationService _ConfigurationService;

    public const string SRDFiveTwoOneCopyrightInfo = "This work includes material from the System Reference Document 5.2.1 (“SRD 5.2.1”) by Wizards of the Coast LLC, available at https://www.dndbeyond.com/srd. The SRD 5.2.1 is licensed under the Creative Commons Attribution 4.0 International License, available at https://creativecommons.org/licenses/by/4.0/legalcode.";

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
            HitDie = 12,
            Features = [
                new ClassFeature5e
                {
                    Name = "Rage",
                    Description = "You can imbue yourself with a primal power called Rage, a force that grants you extraordinary might and resilience. You can enter it as a Bonus Action if you aren’t wearing Heavy armor. You can enter your Rage the number of times shown for your Barbarian level in the Rages column of the Barbarian Features table. You regain one expended use when you finish a Short Rest, and you regain all expended uses when you finish a Long Rest. While active, your Rage grants the following benefits: You have resistance to Piercing, Slashing, and Bludgeoning damage. Whenever you make an attack using a Strength - with either a weapon or an Unarmed Strike - and deal damage to the target, you gain a bonus to that damage that increases as you gain levels as a Barbarian, as shown in the Rage Damage column of the Barbarian Features Table.",
                    Level = 1
                },
                new ClassFeature5e
                {
                    Name = "Unarmored Defense",
                    Description = "While you are not wearing any armor, your Armor Class equals 10 + your Dexterity modifier + your Constitution modifier.",
                    Level = 1
                },
                new ClassFeature5e
                {
                    Name = "Reckless Attack",
                    Description = "When you make your first attack on your turn, you can choose to gain advantage on all melee weapon attack rolls using Strength during that turn, but attack rolls against you have advantage until your next turn.",
                    Level = 2
                },
            ],
        }
    ];
    private List<Class5e> homebrewClasses = [];

    private static readonly List<Subclass5e> srdSubclasses =
    [
        new Subclass5e
        {
            Name = "Path of the Berserker",
            Description = "A subclass of Barbarian that focuses on unrestrained fury and relentless attacks.",
            IsSRD = true,
            CopyrightInfo = SRDFiveTwoOneCopyrightInfo,
            ParentClass = "Barbarian",
            Features = [
                new ClassFeature5e
                {
                    Name = "Frenzy",
                    Description = "Starting at 3rd level, you can go into a Frenzy when you Rage. If you do so, for the duration of your Rage, you can make a single melee weapon attack as a Bonus Action on each of your turns after this one.",
                    Level = 3
                },
                new ClassFeature5e
                {
                    Name = "Mindless Rage",
                    Description = "Beginning at 6th level, you cannot be charmed or frightened while raging.",
                    Level = 6
                },
            ],
        }
    ];
    private List <Subclass5e> homebrewSubclasses = [];

    private static bool AddToPlayerOptionCollection<T>
    (
        List<T> collection,
        T? option
    ) where T : IPlayerOption
    {
        if (option == null || string.IsNullOrEmpty(option.Name))
        {
            Console.WriteLine("Invalid player option provided. Cannot add to collection.");
            return false;
        }
        if (option.IsSRD)
        {
            Console.WriteLine("SRD object. Skipping...");
            return false;
        }
        if (collection.Any(o => o.Name.Equals(option.Name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"Player option {option.Name} already exists. Skipping.");
            return false;
        }
        collection.Add(option);
        return true;
    }

    private static int MassAddToPlayerOptionCollection<T>
    (
        List<T> collection,
        List<T>? options
    ) where T : IPlayerOption
    {
        if (options == null || options.Count == 0)
        {
            Console.WriteLine("No valid player options provided. Cannot add to collection.");
            return 0;
        }
        int addcount = 0;
        foreach (var option in options)
        {
            if (AddToPlayerOptionCollection(collection, option))
            {
                addcount++;
            }
        }
        return addcount;
    }

    /// <summary>
    /// List of all classes, including both SRD and homebrew classes.
    /// Data saved by the user is stored in the homebrew classes list.
    /// </summary>
    public List<Class5e> GetClassList()
    {
        return [.. srdClasses, .. homebrewClasses];
    }

    public bool AddToClassList(Class5e classData)
    {
        return AddToPlayerOptionCollection(homebrewClasses, classData);
    }

    public int AddToClassList(List<Class5e> classDataList)
    {
        return MassAddToPlayerOptionCollection(homebrewClasses, classDataList);
    }

    public List<Subclass5e> GetSubclassList(string? parentClass)
    {
        List<Subclass5e> combined = [.. srdSubclasses, .. homebrewSubclasses];
        if (string.IsNullOrEmpty(parentClass))
        {
            Console.WriteLine("Parent class is not specified. Returning all subclasses.");
            return combined;
        }
        // Return subclasses that match the parent class (case-insensitive)
        return [.. combined.Where(sub => sub.ParentClass != null && sub.ParentClass.Equals(parentClass, StringComparison.OrdinalIgnoreCase))];
    }

    public void AddToSubclassList(Subclass5e subclassData)
    {
        AddToPlayerOptionCollection(homebrewSubclasses, subclassData);
    }

    public void AddToSubclassList(List<Subclass5e> subclassDataList)
    {
        MassAddToPlayerOptionCollection(homebrewSubclasses, subclassDataList);
    }

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

        string parentClassFolder = Path.Combine(classDataPath, "classes");
        if (!Directory.Exists(parentClassFolder))
        {
            Console.WriteLine($"The directory {parentClassFolder} does not exist.");
            Directory.CreateDirectory(parentClassFolder);
        }
        string subclassFolder = Path.Combine(classDataPath, "subclasses");
        if (!Directory.Exists(subclassFolder))
        {
            Console.WriteLine($"The directory {subclassFolder} does not exist.");
            Directory.CreateDirectory(subclassFolder);
        }

        foreach (var file in Directory.GetFiles(subclassFolder, "*.xml"))
        {
            Class5e? classData = ExportImport.ImportFromXmlFileSafe<Class5e>(file);
            if (classData != null)
            {
                AddToClassList(classData);
            }
            else
            {
                Console.WriteLine($"Failed to load class data from {file}.");
            }
        }
        foreach (var file in Directory.GetFiles(parentClassFolder, "*.xml"))
        {
            Subclass5e? classData = ExportImport.ImportFromXmlFileSafe<Subclass5e>(file);
            if (classData != null)
            {
                AddToSubclassList(classData);
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
        if (string.IsNullOrEmpty(classDataPath))
        {
            Console.WriteLine("Character class data path is not set.");
            return;
        }
        if (!Directory.Exists(classDataPath))
        {
            Console.WriteLine($"Character class data path {classDataPath} does not exist.");
            return;
        }
        foreach (var classData in homebrewClasses)
        {
            string filePath = Path.Combine(classDataPath, "classes", $"{classData.Name}.xml");
            if (File.Exists(filePath))
            {
                File.Delete(filePath); // Remove old file if it exists
            }
            if (!ExportImport.ExportToXmlFile(classData, filePath))
            {
                Console.WriteLine($"Failed to save class data for {classData.Name} to {filePath}.");
            }
        }
        foreach (var subclassData in homebrewSubclasses)
        {
            string filePath = Path.Combine(classDataPath, "subclasses", $"{subclassData.Name}.xml");
            if (File.Exists(filePath))
            {
                File.Delete(filePath); // Remove old file if it exists
            }
            if (!ExportImport.ExportToXmlFile(subclassData, filePath))
            {
                Console.WriteLine($"Failed to save subclass data for {subclassData.Name} to {filePath}.");
            }
        }
    }
}
