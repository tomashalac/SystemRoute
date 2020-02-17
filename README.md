# SystemRoute
A class in .net that allows you to manage folders and files, easily and for Linux, Mac and Windows. That was designed to be fast and not create more objects, that's why only strings are used.

![Build And Test .NET Core Projects](https://github.com/tomashalac/SystemRoute/workflows/Build%20And%20Test%20.NET%20Core%20Projects/badge.svg)

# Example
```c#
var absolute = "/home/test/".Build();

var folderRelative = "folder/file.txt".Build();
//You can handle all routes as if it were Linux and the system handles them according to the platform.
var folderAbsolute = "/home/test/folder/file.txt".Build();
//folderAbsolute on windows="C:/home/test/folder/file.txt"
//folderAbsolute on linux="/home/test/folder/file.txt"

//Relative to absolute
Assert.AreEqual(folderAbsolute.ForceToAbsolutePath(absolute), folderRelative.ForceToAbsolutePath(absolute));

//GetFileNameWithoutExtension
Assert.AreEqual(folderAbsolute.GetFileNameWithoutExtension(), folderRelative.GetFileNameWithoutExtension());

//MoveFolderBack
Assert.AreEqual(folderAbsolute.MoveFolderBack(), absolute);
```

Also you need:
```c#
using SystemRoute;
```

# All functions:
* SetPathAndName
* GetPath
* GetAsPath
* IsPath
* GetPaths
* GetFileName
* GetFileNameWithoutExtension
* GetFileExtension
* GetFullFileNameWithoutExtension
* Exists
* ExistsAsDirectory
* Delete
* SetName
* Rename
* Duplicate
* MoveFolderBack
* IsAbsolutePath
* ForceToAbsolutePath