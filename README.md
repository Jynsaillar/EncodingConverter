# EncodingConverter
This program takes a path to a directory containing files with wrong encodings and a source encoding as well as a new encoding as arguments, opens all files in the 
given directory (recursively) and saves them with the new encoding.
File extensions can be filtered to select only specific files in the directory (e.g. `.cs` or `.h`, `.cpp`, `.hpp`).

## Prerequisites

* [.NET Core 3.0 Preview](https://dotnet.microsoft.com/download/dotnet-core/3.0)
* [NuGet Package System.Text.Encoding.CodePages 4.5.1](https://www.nuget.org/packages/System.Text.Encoding.CodePages/)

## How to

* Clone the repository to a folder of your chosing with `git clone https://github.com/Jynsaillar/EncodingConverter.git`
* In the terminal, change your working directory to the `EncodingConverter` root folder, e.g. `cd D:\EncodingConverter`
* Install the `CodePages` NuGet package: `dotnet add package System.Text.Encoding.CodePages --version 4.5.1`
* Run the program either in Debug mode or release mode -
### Debug mode
Modify `EncodingConverter\launch.json` and edit `configurations->args` to your liking, e.g. 

```
...
"configurations": [
        {
            ...
            "args": ["-d", "C:\\FilesWithWrongEncoding", "-se", "windows-1252", "-de", "utf-8" "-f", ".h", ".hpp", ".cpp"],
            ...
        },
        ...
    ]
 ```
 ### Release mode
 Keyword|Number of Arguments|Purpose
:------------------:|:-------:|-------
-d     | 1 |Path to the directory containing files that need to be re-encoded|
-se     | 1 |Source encoding, either an encoding or a codepage listed on [MSDN](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding?view=netcore-3.0)|
-de     | 1 |Destination/New encoding, either an encoding or a codepage listed on [MSDN](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding?view=netcore-3.0)|
-f     | * |One or more file extensions. Only files with these extensions will be re-encoded. Wildcard `*` matches all files (not recommended as this will also include media files etc., only use this if you know that the folder only contains valid files!)|
