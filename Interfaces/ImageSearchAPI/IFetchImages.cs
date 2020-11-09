/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

namespace Assessment.Interfaces.ImageSearchAPI
{
    /// <summary>
    /// Search mode combination for the search words input.
    /// </summary>
    public enum SearchMode
    {
        /// <summary>
        /// All text (separated by commas) must be tagged to the Image.
        /// </summary>
        All,
        /// <summary>
        /// Any of the input text must be tagged to the image.
        /// </summary>
        Any
    }

    /// <summary>
    /// Interface for Fetch Images business logic.
    /// </summary>
    public interface IFetchImages
    {
        /// <summary>
        /// Loads the images tagged with specified search text.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="destPath">The destination path.</param>
        /// <param name="mode">Enum option of <cref SearchMode/> </param>
        void Load(string searchText, string destPath, SearchMode mode = SearchMode.All);
    }
}
