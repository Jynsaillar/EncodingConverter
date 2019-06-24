using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EncodingMethods
{
    public static class DirectoryInfoExtension
    {
        // Taken from https://stackoverflow.com/questions/3527203/getfiles-with-multiple-extensions
        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dirInfo, params string[] extensions)
        {
            var allowedExtensions = new HashSet<string>(extensions, StringComparer.OrdinalIgnoreCase);

            return dirInfo.EnumerateFiles()
                          .Where(file => allowedExtensions.Contains(file.Extension));
        }
    }

    public static class DirectoryEncodingConverter
    {
        public static System.Text.EncodingProvider EncodingProvider = System.Text.CodePagesEncodingProvider.Instance; // These fixes the missing encodings in .NET Core.
        public static int ConvertAllFileEncodingsFiltered(string directory, Encoding newEncoding)
        {
            if (!Directory.Exists(directory))
            {
                Console.WriteLine($"{directory} is not a valid directory or doesn't exist, returning...");
            }

            var targetDirectoryInfo = new DirectoryInfo(directory);

            int convertedFilesCounter = 0;

            foreach (var file in targetDirectoryInfo.GetFilesByExtensions(".h", ".cpp", ".hpp"))
            {
                var fileContent = File.ReadAllText(file.FullName);
                File.WriteAllText(file.FullName, fileContent, newEncoding);
                convertedFilesCounter++;
            }

            return convertedFilesCounter;
        }
    }
}