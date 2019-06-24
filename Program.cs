using System;
using EncodingMethods;

namespace EncodingConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("No valid directory paths found. Please provide a valid directory as first argument [0] and the new encoding as second argument [1].");
            }

            System.Text.Encoding.RegisterProvider(DirectoryEncodingConverter.EncodingProvider); // Registers encoding provider to enable all encodings in .NET Core.

            var targetDirectory = args[0];
            var newEncodingString = args[1];
            try
            {
                var newEncoding = System.Text.Encoding.GetEncoding(newEncodingString);
                var convertedFilesCount = DirectoryEncodingConverter.ConvertAllFileEncodingsFiltered(targetDirectory, newEncoding);

                Console.WriteLine($"Converted {convertedFilesCount} files.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Couldn't figure out the provided encoding. Are you sure it is a valid encoding or codepage?\n{e.Message}");
            }

        }
    }
}
