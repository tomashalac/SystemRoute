using System;
using System.IO;

namespace SystemRoute {

    /// <summary>
    /// A class in .net that allows you to manage folders and files, easily and for Linux, Mac and Windows.
    /// That was designed to be fast and not create more objects, that's why only strings are used.
    /// </summary>
    public static class SystemRoute {

        /// <summary>
        /// Normalize the route so that all its functions of this class work properly among all the multiple platforms.
        /// </summary>
        /// <param name="folderPath">A path of a folder, can be relative or absolute</param>
        /// <param name="fileName">The name of the file, example: "file", or "file.txt"</param>
        /// <returns>The normalized route for this platform</returns>
        public static string Build(this string folderPath, string fileName) {
            return SetPathAndName(folderPath.Build().GetAsPath(), fileName);
        }

        /// <summary>
        /// Normalize the route so that all its functions of this class work properly among all the multiple platforms.
        /// </summary>
        /// <param name="fullFileName">The relative or absolute path of a folder or file</param>
        /// <returns>The normalized route for this platform</returns>
        public static string Build(this string fullFileName) {
            return SetFullFileName(fullFileName);
        }

        public static string SetPathAndName(string folderPath, string fileName) {
            return SetFullFileName(Path.Combine(Path.GetDirectoryName(folderPath) + "/", fileName.GetFileName()));
        }

        /// <summary>
        /// To get a folder from a file or folder
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <returns>The path of a file or a folder, it may not end in /</returns>
        public static string GetPath(this string fullFileName) {
            if (string.IsNullOrEmpty(fullFileName)) return "";
            if (fullFileName.IsPath()) {
                return fullFileName;
            }
            return Normalize(Path.GetDirectoryName(fullFileName) + "/");
        }

        /// <summary>
        /// To get a folder from a file or folder
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <returns>The path of a file or folder always ends with /</returns>
        public static string GetAsPath(this string fullFileName) {
            if (fullFileName.IsPath()) {
                return fullFileName;
            }
            //to force the path to a directory and then return it as a directory
            return Normalize(Path.GetDirectoryName(fullFileName + "/") + "/");
        }

        /// <summary>
        /// The route can be relative or absolute, but it has to end with /
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <returns>Is a folder?</returns>
        public static bool IsPath(this string fullFileName) {
            if (string.IsNullOrEmpty(fullFileName)) return false;
            return fullFileName[fullFileName.Length - 1] == '/';
        }

        public static string[] GetPaths(this string fullFileName) {
            if (string.IsNullOrEmpty(fullFileName)) return new string[0];
            return Normalize(Path.GetDirectoryName(fullFileName)).Split('/');
        }

        /// <summary>
        /// Returns the file name and extension of the specified path string.
        /// <seealso cref="Path.GetFileName"/>
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <returns>
        /// The characters after the last directory separator character in path. If the last
        /// character of path is a directory or volume separator character, this method returns
        /// System.String.Empty. If path is null, this method returns null.
        /// </returns>
        public static string GetFileName(this string fullFileName) {
            return Path.GetFileName(fullFileName);
        }


        /// <summary>
        /// <seealso cref="Path.GetFileNameWithoutExtension"/>
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(this string fullFileName) {
            return Path.GetFileNameWithoutExtension(fullFileName.GetFileName());
        }

        /// <summary>
        /// <seealso cref="Path.GetExtension"/>
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <returns></returns>
        public static string GetFileExtension(this string fullFileName) {
            return Path.GetExtension(fullFileName);
        }

        /// <summary>
        /// To know the full path of the file without its extension, Example: "folder/file"
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <returns>The same file path only as without the file extension</returns>
        public static string GetFullFileNameWithoutExtension(this string fullFileName) {
            return Normalize(Path.Combine(fullFileName.GetPath(), Path.GetFileNameWithoutExtension(fullFileName.GetFileName())));
        }

        /// <summary>
        /// <seealso cref="File.Exists"/>
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <returns></returns>
        public static bool Exists(this string fullFileName) {
            return File.Exists(fullFileName);
        }

        public static bool Exists(this string fullFileName, string endFormat) {
            return File.Exists(fullFileName.GetFullFileNameWithoutExtension() + endFormat);
        }

        /// <summary>
        /// <seealso cref="Directory.Exists"/>
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <returns></returns>
        public static bool ExistsAsDirectory(this string fullFileName) {
            return Directory.Exists(fullFileName);
        }

        /// <summary>
        /// <seealso cref="File.Delete"/>
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        public static void Delete(this string fullFileName) {
            File.Delete(fullFileName);
        }

        public static string SetName(this string path, string name) {
            return SetFullFileName(Path.Combine(path.GetPath(), name));
        }

        public static string Rename(this string fullFileName, string NewName) {
            string newFullName = fullFileName.SetName(NewName);
            File.Move(fullFileName, newFullName);
            return newFullName;
        }

        /// <summary>
        /// <seealso cref="File.Copy"/>
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <param name="newFileAbsolutePath"></param>
        /// <returns>The path to the new file</returns>
        public static string Duplicate(this string fullFileName, string newFileAbsolutePath) {
            File.Copy(fullFileName, newFileAbsolutePath);
            return newFileAbsolutePath;
        }

        /// <summary>
        /// To move to a folder back, for both absolute or relative paths, example: from "folder1/folder2" to "folder1/"
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <returns>The new route</returns>
        public static string MoveFolderBack(this string fullFileName) {
            if (string.IsNullOrEmpty(fullFileName)) return "../";

            string[] paths = fullFileName.GetPaths();
            if (paths[paths.Length - 1] == "..") {
                return Normalize(string.Join("/", paths) + "/../");
            } else {
                paths[paths.Length - 1] = "";
                return Normalize(string.Join("/", paths));
            }
        }

        /// <summary>
        /// <seealso cref="Path.IsPathRooted"/>
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <returns></returns>
        public static bool IsAbsolutePath(this string fullFileName) {
            //for suport windows path on linux systems
            return (fullFileName.Length >= 2 && fullFileName[1] == ':') || Path.IsPathRooted(fullFileName);
        }

        /// <summary>
        /// if it is not an absolute path it transforms it into one, if it already contains an absolute path it returns it
        /// </summary>
        /// <param name="fullFileName">A path to a folder or file already normalized by this class.</param>
        /// <param name="absoluteDefault">If the route is relative it must be completed using this absolute route</param>
        /// <returns>The absolute path to the file or folder</returns>
        public static string ForceToAbsolutePath(this string fullFileName, string absoluteDefault) {

            string FolderPath;

            if (IsAbsolutePath(fullFileName)) {
                FolderPath = GetPath(fullFileName);
            } else {
                //if you have a subfolder in the relative path
                if (fullFileName.Contains("/")) {
                    FolderPath = Path.Combine(absoluteDefault, fullFileName.GetPath());
                } else {
                    FolderPath = absoluteDefault;
                }
            }
            return SetPathAndName(FolderPath, fullFileName.GetFileName());
        }

        private static string Normalize(string path) {
            if (path == null) return "";
            return path.Replace(@"\", "/").Replace(@"\\", "/");
        }

        private static string SetFullFileName(string newFullFileName) {
            newFullFileName = Normalize(newFullFileName);

            if (IsAbsolutePath(newFullFileName)) {

                //if I am in windows and the path starts with / I pass it to the windows format
                if (newFullFileName[0] == '/' && (int)Environment.OSVersion.Platform <= 3) {
                    newFullFileName = "C:" + newFullFileName;
                }
                //if I am in linux and the path starts with x: I pass it to the linux format
                if (newFullFileName.Length >= 2 && newFullFileName[1] == ':' && (int)Environment.OSVersion.Platform > 3) {
                    newFullFileName = newFullFileName.Substring(2);
                }

                //if it is path I make sure it has the correct format and is valid
                //I also clean it, for example if I have 'C:/folder//filename', pass it to 'C:/folder/filename'
                newFullFileName = Normalize(Path.GetFullPath(newFullFileName));
            }

            return newFullFileName;
        }

    }

}
