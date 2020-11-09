/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using Assessment.ImageSearch.Utility;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Assessment.ImageSearch.Service
{
    /// <summary>
    /// Bootstrap class to load parent Unity Container.
    /// </summary>
    internal static class Bootstrap
    {
        static IUnityContainer _parentContainer = new UnityContainer();

        /// <summary>
        /// Gets the unity container.
        /// </summary>
        /// <returns>container object</returns>
        internal static IUnityContainer GetUnityContainer()
        {
            //Load the Container registrations from App config
            try
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = Path.GetFileName(Assembly.GetEntryAssembly().Location) + Constants.ConfigExtension
                };
                Configuration config = ConfigurationManager
                    .OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                ConfigurationSection unitySection = config.GetSection(Constants.UnityConfigSection);
                var unityConfigSection = (UnityConfigurationSection)unitySection;
                _parentContainer.LoadConfiguration(unityConfigSection);
            }
            catch (Exception)
            {
                //Todo: Development log entry
            }
            return _parentContainer;
        }
    }
}
