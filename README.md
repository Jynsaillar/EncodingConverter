# EncodingConverter
Small console app to open and resave all files in a directory with extensions matching a user-provided filter. Runs on .NET Core.

## Prerequisites

* [.NET Core 3.0 Preview](https://dotnet.microsoft.com/download/dotnet-core/3.0)
* [NuGet Package System.Text.Encoding.CodePages 4.5.1](https://www.nuget.org/packages/System.Text.Encoding.CodePages/)

## How to

* Clone the repository to a folder of your chosing with `git clone https://github.com/Jynsaillar/EncodingConverter.git`
* In the terminal, change your working directory to the `EncodingConverter` root folder, e.g. `>cd D:\EncodingConverter`
* Install the `CodePages` NuGet package: `dotnet add package System.Text.Encoding.CodePages --version 4.5.1`
* Run the program either in Debug mode or release mode -
### Debug mode
Modify `EncodingConverter\launch.json` and edit `configurations->args` to your liking, e.g. 

```
...
"configurations": [
        {
            ...
            "args": ["C:\\FilesWithWrongEncoding", "windows-1252"],
            ...
        },
        ...
    ]
 ```
 ### Release mode
 Provide the path to the directory containing badly encoded files as first argument and the new encoding as second argument.
