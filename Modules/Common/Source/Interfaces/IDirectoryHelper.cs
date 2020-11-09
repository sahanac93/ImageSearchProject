/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using System.IO;

namespace Assessment.Common
{
    /// <summary>
    /// Interface of Directory wrapper
    /// </summary>
    public interface IDirectoryHelper
    {
        /// <summary>
        /// Clears the directory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        void ClearDirectory(string directoryPath);

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns></returns>
        DirectoryInfo CreateDirectory(string directoryPath);

        /// <summary>
        /// Gets all the files.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>List of files path</returns>
        string[] GetFiles(string directoryPath);

        /// <summary>
        /// Gets all the files which matches search pattern.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns>List of files path</returns>
        string[] GetFiles(string directoryPath, string searchPattern);

        /// <summary>
        /// Checks if the specified directory path exists.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <returns>TRUE if exists; false otherwise</returns>
        bool Exists(string directoryPath);
    }
}
