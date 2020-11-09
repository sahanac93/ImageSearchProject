/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using System.IO;

namespace Assessment.Common
{
    /// <summary>
    /// Directory wrapper implementation
    /// </summary>
    /// <seealso cref="Assessment.Common.IDirectoryHelper" />
    public class DirectoryHelper : IDirectoryHelper
    {
        /// <summary>
        /// Clears the directory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <exception cref="InvalidDataException">Rooted path is expected for ClearDirectory; 
        /// Path \"{directoryPath}\" is invalid.</exception>
        public void ClearDirectory(string directoryPath)
        {
            if (!Path.IsPathRooted(directoryPath))
            {
                throw new InvalidDataException($"Rooted path is expected for ClearDirectory; Path \"{directoryPath}\" is invalid.");
            }

            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>DirectoryInfo object</returns>
        public DirectoryInfo CreateDirectory(string directoryPath)
        {
            //Base Directory class does the IsExisits check before creation   
            return Directory.CreateDirectory(directoryPath);
        }

        /// <summary>
        /// Checks if the specified directory path exists.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns> TRUE if exists; false otherwise </returns>
        public bool Exists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// Gets all the files.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns> List of files path </returns>
        public string[] GetFiles(string directoryPath)
        {
            return Directory.GetFiles(directoryPath);
        }

        /// <summary>
        /// Gets all the files which matches search pattern.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns> List of files path </returns>
        public string[] GetFiles(string directoryPath, string searchPattern)
        {
            return Directory.GetFiles(directoryPath, searchPattern);
        }

    }
}
