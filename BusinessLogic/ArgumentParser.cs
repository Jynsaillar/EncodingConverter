using System;
using System.Collections.Generic;

namespace ArgumentParsing
{
    public static class ArgumentParser
    {
        public static (string TargetDirectory, string NewEncoding, List<string> ExtensionFilters) FilteredArgs(string[] args)
        {
            if (args.Length < 6)
            {
                Console.WriteLine("Not enough arguments provided.\nExpected arguments:");
                Console.WriteLine("-d <targetDirectory>, path to a directory whose files should be re-encoded.");
                Console.WriteLine("-e <newEncoding>, an encoding or a codepage as a string, see https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding?view=netcore-3.0 for a list of valid encodings & codepages.");
                Console.WriteLine("-f <extensionFilters>, one or more extensions, only files with any of these extensions will be re-encoded.");
                Console.WriteLine("* to re-encode all files (Not advised as this will include images, audio etc.!)");
                Console.WriteLine("Example 1: -d \"C:\\FolderWithWrongEncoding\" -e windows-1252 -f .h .hpp .cpp");
                Console.WriteLine("Example 2: -d \"D:\\ParentFolder\\FolderWithWrongEncoding\" -e euc-kr -f *");
            }

            string targetDirectory = string.Empty;
            string newEncoding = string.Empty;
            List<string> extensionFilters = new List<string>();
            bool flagDirectory = false;
            bool flagEncoding = false;
            bool flagFilters = false;

            foreach (var argument in args)
            {
                if (!flagDirectory && !flagEncoding && !flagFilters)
                {
                    switch (argument)
                    {
                        case "-d":
                            flagDirectory = true;
                            flagEncoding = false;
                            flagFilters = false;
                            break;
                        case "-e":
                            flagEncoding = true;
                            flagDirectory = false;
                            flagFilters = false;
                            break;
                        case "-f":
                            flagFilters = true;
                            flagDirectory = false;
                            flagEncoding = false;
                            break;
                        // Okay Houston, we've had a problem here.
                        default: throw new ArgumentException("The provided argument does not match any of the keywords - did you forget prepending a keyword like \"-d\" or \"-f\" or did you maybe forget the argument itself?");
                    }

                }
                else if (flagDirectory && !flagEncoding && !flagFilters) // This argument is the targetDirectory.
                {
                    targetDirectory = argument;
                    flagDirectory = false;
                }
                else if (flagEncoding && !flagDirectory && !flagFilters) // This argument is the newEncoding.
                {
                    newEncoding = argument;
                    flagEncoding = false;
                }
                else if (flagFilters && !flagDirectory && !flagEncoding) // This and all following arguments are extensionFilters.
                {
                    if (argument == "-d" && targetDirectory == string.Empty)
                    {
                        flagFilters = false;
                        flagDirectory = true;
                    }
                    else if (argument == "-e" && newEncoding == string.Empty)
                    {
                        flagFilters = false;
                        flagEncoding = true;
                    }
                    else
                    {
                        extensionFilters.Add(argument);
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid argument detected - did you arrange the arguments in proper order (keyword first, then the argument)?\nKeep in mind that keywords may not follow each other, e.g. \"-d -f\" will break the program.");
                }
            }

            return (targetDirectory, newEncoding, extensionFilters);
        }
    }
}