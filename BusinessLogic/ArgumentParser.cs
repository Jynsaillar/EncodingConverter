using System;
using System.Collections.Generic;

namespace ArgumentParsing
{
    public static class ArgumentParser
    {
        public static (string TargetDirectory, string SourceEncoding, string DestEncoding, List<string> ExtensionFilters) FilteredArgs(string[] args)
        {
            if (args.Length < 6)
            {
                Console.WriteLine("Not enough arguments provided.\nExpected arguments:");
                Console.WriteLine("-d <targetDirectory>, path to a directory whose files should be re-encoded.");
                Console.WriteLine("-se <sourceEncoding>, the current encoding of the file as a string, see https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding?view=netcore-3.0 for a list of valid encodings & codepages.");
                Console.WriteLine("-de <destEncoding>, the new encoding of the file as a string, see https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding?view=netcore-3.0 for a list of valid encodings & codepages.");
                Console.WriteLine("-f <extensionFilters>, one or more extensions, only files with any of these extensions will be re-encoded.");
                Console.WriteLine("* to re-encode all files (Not advised as this will include images, audio etc.!)");
                Console.WriteLine("Example 1: -d \"C:\\FolderWithWrongEncoding\" -se windows-1252 -de utf-8 -f .h .hpp .cpp");
                Console.WriteLine("Example 2: -d \"D:\\ParentFolder\\FolderWithWrongEncoding\" -se utf-8 -de euc-kr -f *");
            }

            string targetDirectory = string.Empty;
            string sourceEncoding = string.Empty;
            string destEncoding = string.Empty;
            List<string> extensionFilters = new List<string>();
            bool flagDirectory = false;
            bool flagSourceEncoding = false;
            bool flagDestEncoding = false;
            bool flagFilters = false;

            foreach (var argument in args)
            {
                if (!flagDirectory && !flagSourceEncoding && !flagDestEncoding && !flagFilters)
                {
                    switch (argument)
                    {
                        case "-d":
                            flagDirectory = true;
                            flagSourceEncoding = false;
                            flagDestEncoding = false;
                            flagFilters = false;
                            break;
                        case "-se":
                            flagSourceEncoding = true;
                            flagDestEncoding = false;
                            flagDirectory = false;
                            flagFilters = false;
                            break;
                        case "-de":
                            flagDestEncoding = true;
                            flagSourceEncoding = false;
                            flagDirectory = false;
                            flagFilters = false;
                            break;
                        case "-f":
                            flagFilters = true;
                            flagDirectory = false;
                            flagSourceEncoding = false;
                            flagDestEncoding = false;
                            break;
                        // Okay Houston, we've had a problem here.
                        default: throw new ArgumentException("The provided argument does not match any of the keywords - did you forget prepending a keyword like \"-d\" or \"-f\" or did you maybe forget the argument itself?");
                    }

                }
                else if (flagDirectory && !flagSourceEncoding && !flagDestEncoding && !flagFilters) // This argument is the targetDirectory.
                {
                    targetDirectory = argument;
                    flagDirectory = false;
                }
                else if (flagSourceEncoding && !flagDestEncoding && !flagDirectory && !flagFilters) // This argument is the sourceEncoding.
                {
                    sourceEncoding = argument;
                    flagSourceEncoding = false;
                }
                else if (flagDestEncoding && !flagSourceEncoding && !flagDirectory && !flagFilters) // This argument is the sourceEncoding.
                {
                    destEncoding = argument;
                    flagDestEncoding = false;
                }
                else if (flagFilters && !flagDirectory && !flagSourceEncoding && !flagDestEncoding) // This and all following arguments are extensionFilters.
                {
                    if (argument == "-d" && targetDirectory == string.Empty)
                    {
                        flagFilters = false;
                        flagDirectory = true;
                    }
                    else if (argument == "-se" && sourceEncoding == string.Empty)
                    {
                        flagFilters = false;
                        flagSourceEncoding = true;
                    }
                    else if (argument == "-de" && destEncoding == string.Empty)
                    {
                        flagFilters = false;
                        flagDestEncoding = true;
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

            return (targetDirectory, sourceEncoding, destEncoding, extensionFilters);
        }
    }
}