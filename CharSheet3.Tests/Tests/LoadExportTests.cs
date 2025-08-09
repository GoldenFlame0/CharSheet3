using CharSheet3.Models;
using CharSheet3.Utilities;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace CharSheet3.Tests;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public class LoadExportTests
{
    private static Character5e LoadFromPremadeFile(string fileName)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", fileName);
        return ExportImport.ImportFromXmlFile<Character5e>(filePath) ?? throw new FileNotFoundException($"File {fileName} not found or could not be loaded.");
    }

    [Fact]
    public void GivenTestFile_WhenLoaded_PopulatedCorrectly()
    {
        Character5e loadedCharacter = LoadFromPremadeFile("TestChar.xml");
        Assert.Equal("TestChar", loadedCharacter?.CharacterName);
        Assert.Equal(1, loadedCharacter?.StrengthScore);
        Assert.Equal(2, loadedCharacter?.DexterityScore);
        Assert.Equal(3, loadedCharacter?.ConstitutionScore);
        Assert.Equal(4, loadedCharacter?.IntelligenceScore);
        Assert.Equal(5, loadedCharacter?.WisdomScore);
        Assert.Equal(6, loadedCharacter?.CharismaScore);
    }
}
