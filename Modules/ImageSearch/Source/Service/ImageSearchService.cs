/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using Assessment.Common;
using Assessment.DataModel;
using Assessment.ImageSearch.Utility;
using Assessment.Interfaces.ImageSearchAPI;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;

namespace Assessment.ImageSearch.Service
{
    /// <summary>
    /// ImageSearch Service class
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class ImageSearchService : IDisposable
    {
        IFetchImages _fetchLogic;
        IUnityContainer _container;
        IDirectoryHelper _directory;
        string _configFilePath;
        string _destinationPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSearchService"/> class.
        /// </summary>
        /// <param name="container">The unity container.</param>
        public ImageSearchService(IUnityContainer container)
        {
            _container = container;
            _fetchLogic = _container.Resolve<IFetchImages>();
            _directory = _container.Resolve<IDirectoryHelper>();
            string dateTimeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            _configFilePath = Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location) + Constants.ConfigExtension;
            _destinationPath = Path.Combine(ConfigurationHelper.GetSetting(Constants.DestinationPath, _configFilePath), dateTimeStamp);
        }

        /// <summary>
        /// Gets all images.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns>List of <see cref="ImageSearchModel"/></returns>
        public List<ImageSearchModel> GetAllImages(string searchText)
        {
            List<ImageSearchModel> images = new List<ImageSearchModel>();
            try
            {
                if (!_directory.Exists(_destinationPath))
                {
                    _directory.CreateDirectory(_destinationPath);
                }

                var modeOfSearch = ConfigurationHelper.GetSetting(Constants.SearchMode, _configFilePath);
                var mode = modeOfSearch.ToEnum<SearchMode>();
                _fetchLogic.Load(searchText, _destinationPath, mode);
                images = PopulateImages();
            }
            catch (Exception)
            {
                //Todo: Developmet log entry
            }
            return images;
        }

        /// <summary>
        /// Populates the images.
        /// </summary>
        /// <returns>List of <see cref="ImageSearchModel"/></returns>
        public List<ImageSearchModel> PopulateImages()
        {
            List<ImageSearchModel> images = new List<ImageSearchModel>();
            try
            {
                string[] filesindirectory = _directory.GetFiles(_destinationPath, Constants.JpgExtension);
                foreach (string url in filesindirectory)
                {
                    images.Add(_container.Resolve<ImageSearchModel>(new ParameterOverride("imageSourcePath", url)));
                }
            }
            catch (Exception)
            {
                //Todo: Developmet log entry
            }
            return images;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
            _container = null;
        }
    }
}
