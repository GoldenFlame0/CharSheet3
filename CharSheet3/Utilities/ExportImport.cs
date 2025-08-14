using System;

using CharSheet3.Structures;

namespace CharSheet3.Utilities;

public static class ExportImport
{
    #region File Import/Export

    // TODO: Add stream version.

    /// <summary>
    /// Does what it says on the tin.
    /// TODO: Add non-overwrite option.
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
    /// Safe version that checks for file existence and handles exceptions.
    /// TODO: I bet $5 there's some security issue here.
    /// </summary>
    public static T? ImportFromXmlFileSafe<T>(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
        {
            Console.WriteLine("File path is invalid or file does not exist.");
            return default;
        }

        try
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
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
