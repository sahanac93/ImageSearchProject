/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using System.IO;

namespace Assessment.Common
{
    /// <summary>
    /// Interface of File wrapper
    /// </summary>
    public interface IFileHelper 
    {
        /// <summary>
        /// Creates a File in the specified path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>FileStream object</returns>
        FileStream Create(string filePath);

        /// <summary>
        /// Checks if the specified file exists in the path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>TRUE if exists; FALSE otherwise</returns>
        bool Exists(string filePath);

        /// <summary>
        /// Deletes the specified file in given path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        void Delete(string filePath);
    }
}
