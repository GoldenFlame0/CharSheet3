using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharSheet3.Services;
/// <summary>
/// This service handles two things: User config and platform-specific getters.
/// </summary>
public class ConfigurationAndPlatformService
{
    public string GetOperatingSystemAndDistributionName()
    {
        PlatformID platformID = Environment.OSVersion.Platform;
        switch (platformID)
        {
            case PlatformID.Win32NT:
                return GetWindowsReleaseName();
            case PlatformID.Unix:
                return GetLinuxDistroName();
            case PlatformID.MacOSX:
                return "macOS";
            default:
                return "Unknown OS";
        }
    }

    public static string GetWindowsReleaseName()
    {
        // I have no idea how Visual Studio's AI figured this out, but let's see.
        return Environment.OSVersion.Version.ToString();
    }

    public static string GetLinuxDistroName()
    {
        // Google AI wrote the bulk of this.
        string osReleasePath = "/etc/os-release";
        if (File.Exists(osReleasePath))
        {
            try
            {
                string[] lines = File.ReadAllLines(osReleasePath);
                Dictionary<string, string> distroInfo = lines
                    .Where(line => line.Contains('='))
                    .ToDictionary(
                        line => line.Split('=')[0],
                        line => line.Split('=')[1].Trim('"')
                    );

                if (distroInfo.TryGetValue("NAME", out string? name))
                {
                    Console.WriteLine($"Distribution Name: {name}");
                }
                if (distroInfo.TryGetValue("VERSION_ID", out string? versionId))
                {
                    Console.WriteLine($"Version ID: {versionId}");
                }
                if (distroInfo.TryGetValue("PRETTY_NAME", out string? prettyName))
                {
                    Console.WriteLine($"Pretty Name: {prettyName}");
                }

                return $"{name} {versionId} ({prettyName})";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading /etc/os-release: {ex.Message}");
            }
        }
        return "Linux, but can't find /etc/os-release.";
    }
}
