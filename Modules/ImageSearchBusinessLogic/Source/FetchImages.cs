/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using Assessment.Common;
using Assessment.ImageSearchBusinessLogic.Utility;
using Assessment.Interfaces.ImageSearchAPI;
using Assessment.Interfaces.Logging;
using Microsoft.Practices.Unity;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assessment.ImageSearchBusinessLogic
{
    public class FetchImages : IFetchImages
    {
        IUnityContainer _container;
        IDirectoryHelper _directory;
        IFileHelper _file;
        IDevelopmentLogger _devLog;

        public FetchImages(IUnityContainer container)
        {
            _container = container.CreateChildContainer();
            _directory = _container.Resolve<IDirectoryHelper>();
            _file = _container.Resolve<IFileHelper>();
            _devLog = _container.Resolve<IDevelopmentLogger>();
        }

        public void Load(string searchText, string destPath, SearchMode mode = SearchMode.All)
        {
            try
            {
                string searchString = string.Format("https://www.flickr.com/services/feeds/photos_public.gne?tags={0}&tagmode={1}", searchText, mode);
                using (HttpClient httpClient = new HttpClient())
                {
                    var data = httpClient.GetByteArrayAsync(searchString);
                    var fileStream = _file.Create(Path.Combine(destPath, Constants.HttpResponseFile));
                    fileStream.Write(data.Result, 0, data.Result.Length);
                    fileStream.Close();
                }

                XDocument xdoc = XDocument.Load(Path.Combine(destPath, Constants.HttpResponseFile));
                DownloadImages(xdoc, destPath);
            }
            catch (Exception exception)
            {
                _devLog.Error(exception);
                throw exception;
            }
        }

        private int DownloadImages(XDocument xmlSource, string destinationPath)
        {
            int imgCount = 0;
            foreach (XElement item in xmlSource.Root.DescendantNodes().OfType<XElement>())
            {
                if (item.Name.LocalName == "link" && item.Attributes("rel").First().Value.Equals("enclosure"))
                {
                    imgCount++;
                    string dwnld = item.Attribute("href").Value;
                    if (!dwnld.Contains(Constants.ImageExtension))
                    {
                        continue;
                    }
                    using (HttpClient hclient = new HttpClient())
                    {
                        Task<Stream> stream = hclient.GetStreamAsync(dwnld);
                        System.Drawing.Image searchImage = System.Drawing.Image.FromStream(stream.Result);
                        var dirInfo = _directory.CreateDirectory(destinationPath); 
                        searchImage.Save(Path.Combine(dirInfo.FullName, Constants.ImageFileName + imgCount + Constants.ImageExtension));
                        searchImage.Dispose();
                    }
                }
            }
            return imgCount;
        }
    }
}
