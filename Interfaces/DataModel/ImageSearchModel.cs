/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

namespace Assessment.DataModel
{
    /// <summary>
    /// Data Model for Image Search
    /// </summary>
    public class ImageSearchModel
    {
        /// <summary>
        /// Construtor initializing property
        /// </summary>
        /// <param name="imageSourcePath">Initializer of <cref>ImageSourcePath</cref> </param>
        public ImageSearchModel(string imageSourcePath)
        {
            ImageSourcePath = imageSourcePath;
        }

        /// <summary>
        /// Source path of Image file to be displayed.
        /// </summary>
        public string ImageSourcePath { get; set; }

        /// <summary>
        /// Alt text for the Image to be displayed.
        /// </summary>
        public string AlternateText { get; set; }

    }
}
