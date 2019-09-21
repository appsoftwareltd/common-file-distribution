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
                Console.ForegroundColor = ConsoleColor.White;

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

                                ConsoleWriteErrorLine($"Could not find destination directory {destinationDirectory}. No files copied for this distribution.");
                            }
                        }
                    }
                    else
                    {
                        directoriesVerifiedExisting = false;

                        ConsoleWriteErrorLine($"Could not find source directory {distribution.SourceDirectory}. No files copied for this distribution.");
                    }

                    if (directoriesVerifiedExisting)
                    {
                        ConsoleWriteInfoLine($"Source directory is {distribution.SourceDirectory}");

                        foreach (var sourceFilePath in Directory.GetFiles(distribution.SourceDirectory))
                        {
                            string sourceFileName = Path.GetFileName(sourceFilePath);

                            // Only continue with distribution if FileNameMaskRegex or matches pattern

                            if (string.IsNullOrWhiteSpace(distribution.FileNameMaskRegex) || Regex.IsMatch(sourceFileName, distribution.FileNameMaskRegex))
                            {
                                ConsoleWriteInfoLine($"File name mask regex is {distribution.FileNameMaskRegex}");
                            }

                            if (string.IsNullOrWhiteSpace(distribution.FileNameMaskRegex) || Regex.IsMatch(sourceFileName, distribution.FileNameMaskRegex))
                            { 
                                foreach (var destinationDirectory in distribution.DestinationDirectories)
                                {
                                    string destinationFilePath = Path.Combine(destinationDirectory, sourceFileName);

                                    ConsoleWriteInfoLine($"Copying {sourceFileName} to {destinationDirectory}");

                                    File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
                                }
                            }
                            else
                            {
                                ConsoleWriteWarningLine($"Skipping {sourceFileName} as did not match File name mask regex");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleWriteErrorLine($"Error {ex.Message}");
            }

            #if DEBUG

            ConsoleWriteInfoLine("Press any key to exit");

            Console.ReadKey();

            #endif
        }

        public static void ConsoleWriteInfoLine(string str)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(str);
        }

        public static void ConsoleWriteWarningLine(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(str);

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ConsoleWriteErrorLine(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(str);

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
