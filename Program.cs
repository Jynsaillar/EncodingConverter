using System;
using EncodingMethods;
using ArgumentParsing;

namespace EncodingConverter
{
    class Program
    {
        static void Main(string[] args)
        {

            System.Text.Encoding.RegisterProvider(DirectoryEncodingConverter.EncodingProvider); // Registers encoding provider to enable all encodings in .NET Core.
            ConvertFileEncodings(args);
        }

        static void ConvertFileEncodings(string[] args)
        {
            try
            {
                var filteredArgs = ArgumentParser.FilteredArgs(args);
                var sourceEncoding = System.Text.Encoding.GetEncoding(filteredArgs.SourceEncoding);
                var destEncoding = System.Text.Encoding.GetEncoding(filteredArgs.DestEncoding);
                var convertedFilesCount = DirectoryEncodingConverter.ConvertAllFileEncodingsFiltered(filteredArgs.TargetDirectory, sourceEncoding, destEncoding, filteredArgs.ExtensionFilters);

                Console.WriteLine($"Converted {convertedFilesCount} files.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Couldn't figure out the provided encoding. Are you sure it is a valid encoding or codepage?\n{e.Message}");
            }
        }
    }
}
