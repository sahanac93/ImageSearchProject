/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using System.IO;

namespace Assessment.Common
{
    /// <summary>
    /// File wrapper implementation
    /// </summary>
    /// <seealso cref="Assessment.Common.IFileHelper" />
    public class FileHelper : IFileHelper
    {
        /// <summary>
        /// Creates a File in the specified path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// FileStream object
        /// </returns>
        public FileStream Create(string filePath)
        {
            return File.Create(filePath);
        }

        /// <summary>
        /// Deletes the specified file in given path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void Delete(string filePath)
        {
            File.Delete(filePath);
        }

        /// <summary>
        /// Checks if the specified file exists in the path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>
        /// TRUE if exists; FALSE otherwise
        /// </returns>
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
