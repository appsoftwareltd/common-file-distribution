#!/usr/bin/env dotnet-script

#r "nuget: Newtonsoft.Json, 12.0.2"

using System;
using System.IO;
using Newtonsoft.Json;

class Program
{
    public static void Run(string[] args)
    {
        try
        {
            string configurationFilePath = args[0];

            string configurationFileText = File.ReadAllText(configurationFilePath);

            var configurationFile = JsonConvert.DeserializeObject<ConfigurationFile>(configurationFileText);

            foreach (var distribution in configurationFile.Distributions)
            {
                if (Directory.Exists(distribution.SourceDirectory))
                {
                    Console.WriteLine($"Source directory is {distribution.SourceDirectory}");

                    foreach (var sourceFilePath in Directory.GetFiles(distribution.SourceDirectory))
                    {
                        foreach (var destinationDirectory in distribution.DestinationDirectories)
                        {
                            if (Directory.Exists(destinationDirectory))
                            {
                                string sourceFileName = Path.GetFileName(sourceFilePath);

                                string destinationFilePath = Path.Combine(destinationDirectory, sourceFileName);

                                Console.WriteLine($"Copying {sourceFileName} to {destinationDirectory}");

                                File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
                            }
                            else
                            {
                                Console.WriteLine($"Could not find destination directory {destinationDirectory}");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Could not find source directory {distribution.SourceDirectory}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error {ex.Message}");
        }

        #if DEBUG

        Console.WriteLine("Press any key to exit");

        Console.ReadKey();

        #endif
    }
}

public class ConfigurationFile
{
    public IList<Distribution> Distributions { get; set; }
}

public class Distribution
{
    public string SourceDirectory { get; set; }

    public IList<string> DestinationDirectories { get; set; }
}

Program.Run(Args.ToArray());

