/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Assessment.Common
{
    /// <summary>
    /// Utility to get and set values from COnfiguration file
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// Gets the setting value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="configFileName">Name of the configuration file.</param>
        /// <returns>Value of the specified setting key.</returns>
        public static string GetSetting(string key, string configFileName)
        {
            var appSettings = GetConfig(configFileName).AppSettings.Settings;
            string result = appSettings[key].Value;
            return result;
        }

        /// <summary>
        /// Adds or Updates the setting value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="configFileName">Name of the configuration file.</param>
        /// <exception cref="ArgumentNullException">Invalid input</exception>
        public static void AddUpdateSetting(string key, string value, string configFileName)
        {
            if(key.Equals(string.Empty))
            {
                throw new ArgumentNullException(key,"Invalid input");
            }
            Configuration configFile = GetConfig(configFileName);
            var appSettings = configFile.AppSettings.Settings;
            if (appSettings[key] == null)
            {
                appSettings.Add(key, value);
            }
            else
            {
                appSettings[key].Value = value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="configFilePath">The configuration file path.</param>
        /// <returns> <see cref="Configuration"/> Object </returns>
        private static Configuration GetConfig(string configFilePath)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = configFilePath
            };
            Configuration config = ConfigurationManager
                .OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return config;
        }
    }
}
