using System;
using System.Collections.Generic;
using System.IO;

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

    public List<Class5e> ClassList { get; set; } = [];

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
