using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CharSheet3.Models;

namespace CharSheet3.Utilities;

public static class ExportImport
{
    #region File Import/Export

    // TODO: Add stream version.

    /// <summary>
    /// Does what it says on the tin.
    /// </summary>
    public static bool ExportToXmlFile<T>(T input, string filePath)
    {
        try
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using var writer = new System.IO.StreamWriter(filePath);
            serializer.Serialize(writer, input);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exporting to XML: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Does what it says on the tin.
    /// </summary>
    public static T? ImportFromXmlFile<T>(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
        {
            Console.WriteLine("File path is invalid or file does not exist.");
            return default;
        }

        try
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Character5e));
            using var reader = new System.IO.StreamReader(filePath);
            return (T?)serializer.Deserialize(reader);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error importing from XML: {ex.Message}");
            return default;
        }
    }
    #endregion
}
