using Assessment.Common;
using Assessment.ImageSearch.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ImageSearch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Directory.Delete(ConfigurationHelper.GetSetting(Constants.DestinationPath,
                Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location) + Constants.ConfigExtension),true);
        }
    }

    
}
