using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CharSheet3.Services;
/// <summary>
/// This service handles two things: User config and platform-specific getters.
/// </summary>
public class ConfigurationService
{
    public ConfigurationService() { }

    public string TestString { get; set; } = "This is a test string.";

    public string CharacterClassDataPath { get; set; } = "\\ClassData\\";
}
