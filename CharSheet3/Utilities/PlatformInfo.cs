using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CharSheet3.Utilities;
public static class PlatformInfo
{
    public static string GetOperatingSystemAndDistributionName()
    {
        return Environment.OSVersion.Platform switch
        {
            PlatformID.Win32NT => GetWindowsReleaseName(),
            PlatformID.Unix => GetLinuxDistroName(),
            PlatformID.MacOSX => "macOS",
            _ => "Unknown OS",
        };
    }

    public static string GetWindowsReleaseName()
    {
        // Fun fact we can't tell if it's Windows 10 or 11 easily.
        // So it's... uhh... windows.
        return "Windows (at least one of them)";
    }

    public static string GetLinuxDistroName()
    {
        // Google AI wrote the bulk of this.
        string osReleasePath = "/etc/os-release";
        if (File.Exists(osReleasePath))
        {
            try
            {
                // todo this way of reading lines feels gross.
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

                return prettyName ?? $"{name} {versionId}" ?? "Linux, but error reading /etc/os-release.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Linux, but error reading /etc/os-release: {ex.Message}");
            }
        }
        return "Linux, but can't find /etc/os-release.";
    }
}
