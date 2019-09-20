using System;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AppSoftware.FileDistribution
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string configurationFilePath = args[0];

                string configurationFileText = File.ReadAllText(configurationFilePath);

                var configurationFile = JsonConvert.DeserializeObject<ConfigurationFile>(configurationFileText);

                foreach (var distribution in configurationFile.Distributions)
                {
                    bool directoriesVerifiedExisting = true;

                    if (Directory.Exists(distribution.SourceDirectory))
                    {
                        foreach (var destinationDirectory in distribution.DestinationDirectories)
                        {
                            if(!Directory.Exists(destinationDirectory))
                            {
                                directoriesVerifiedExisting = false;

                                Console.WriteLine($"Could not find destination directory {destinationDirectory}. No files copied for this distribution.");
                            }
                        }
                    }
                    else
                    {
                        directoriesVerifiedExisting = false;

                        Console.WriteLine($"Could not find source directory {distribution.SourceDirectory}. No files copied for this distribution.");
                    }

                    if (directoriesVerifiedExisting)
                    { 
                        Console.WriteLine($"Source directory is {distribution.SourceDirectory}");

                        foreach (var sourceFilePath in Directory.GetFiles(distribution.SourceDirectory))
                        {
                            string sourceFileName = Path.GetFileName(sourceFilePath);

                            // Only continue with distribution if FileNameMaskRegex or matches pattern

                            if (string.IsNullOrWhiteSpace(distribution.FileNameMaskRegex) || Regex.IsMatch(sourceFileName, distribution.FileNameMaskRegex))
                            {
                                Console.WriteLine($"File name mask regex is {distribution.FileNameMaskRegex}");
                            }

                            if (string.IsNullOrWhiteSpace(distribution.FileNameMaskRegex) || Regex.IsMatch(sourceFileName, distribution.FileNameMaskRegex))
                            { 
                                foreach (var destinationDirectory in distribution.DestinationDirectories)
                                {
                                    string destinationFilePath = Path.Combine(destinationDirectory, sourceFileName);

                                    Console.WriteLine($"Copying {sourceFileName} to {destinationDirectory}");

                                    File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Skipping {sourceFileName} as did not match File name mask regex");
                            }
                        }
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
}
