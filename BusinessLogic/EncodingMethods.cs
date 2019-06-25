using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EncodingMethods
{
    public static class DirectoryInfoExtension
    {
        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dirInfo, params string[] extensions)
        {
            var allowedExtensions = new HashSet<string>(extensions, StringComparer.OrdinalIgnoreCase);
            if (allowedExtensions.Contains("*"))
            {
                return dirInfo.EnumerateFiles(string.Empty, SearchOption.AllDirectories);
            }

            return dirInfo.EnumerateFiles(string.Empty, SearchOption.AllDirectories)
                          .Where(file => allowedExtensions.Contains(file.Extension));
        }
    }

    public static class DirectoryEncodingConverter
    {
        public static System.Text.EncodingProvider EncodingProvider = System.Text.CodePagesEncodingProvider.Instance; // These fixes the missing encodings in .NET Core.
        public static int ConvertAllFileEncodingsFiltered(string directory, Encoding newEncoding, List<string> extensionFilters)
        {
            if (!Directory.Exists(directory))
            {
                Console.WriteLine($"{directory} is not a valid directory or doesn't exist, returning...");
            }

            var targetDirectoryInfo = new DirectoryInfo(directory);

            int convertedFilesCounter = 0;

            foreach (var file in targetDirectoryInfo.GetFilesByExtensions(extensionFilters.ToArray()))
            {
                var fileContent = File.ReadAllText(file.FullName);
                File.WriteAllText(file.FullName, fileContent, newEncoding);
                convertedFilesCounter++;
            }

            return convertedFilesCounter;
        }
    }
}