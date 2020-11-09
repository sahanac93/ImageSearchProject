/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using Assessment.Common;
using Assessment.DataModel;
using Assessment.ImageSearch.Service;
using Microsoft.Practices.Unity;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assessment.ImageSearch
{
    /// <summary>
    /// View-Model class for MAinWindow
    /// </summary>
    /// <seealso cref="Assessment.Common.ViewModelBase" />
    /// <seealso cref="System.IDisposable" />
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private IUnityContainer _container;
        private ObservableCollection<ImageSearchModel> imagesPathListField;
        private string searchTextField;
        private RelayCommand loadCommandField;
        private bool inProgressField;
        private bool _can = false; // Flag

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="parentContainer">The parent container.</param>
        public MainWindowViewModel(IUnityContainer parentContainer)
        {
            _container = parentContainer;
            LoadCommand = new RelayCommand(OnLoad, CanLoad);
            InProgress = false;
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText
        {
            get
            {
                return searchTextField;
            }
            set
            {
                SetProperty<string>(ref searchTextField, value);
            }
        }

        /// <summary>
        /// Gets or sets the load command.
        /// </summary>
        /// <value>
        /// The load command.
        /// </value>
        public RelayCommand LoadCommand
        {
            get
            {
                if (loadCommandField == null)
                    loadCommandField = new RelayCommand(OnLoad, CanLoad);
                return loadCommandField;
            }
            set => loadCommandField = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [in progress].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [in progress]; otherwise, <c>false</c>.
        /// </value>
        public bool InProgress
        {
            get => inProgressField;
            set
            {
                SetProperty<bool>(ref inProgressField, value);
            }
        }

        /// <summary>
        /// Gets or sets the images path list.
        /// </summary>
        /// <value>
        /// The images path list.
        /// </value>
        public ObservableCollection<ImageSearchModel> ImagesPathList
        {
            get => imagesPathListField;
            set
            {
                SetProperty<ObservableCollection<ImageSearchModel>>(ref imagesPathListField, value);
            }
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        /// <summary>
        /// Determines whether this instance can load the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance can load the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        private bool CanLoad(object parameter)
        {
            if (SearchText == null)
                return false;
            _can = !SearchText.Equals(string.Empty) && !string.IsNullOrWhiteSpace(SearchText) && Validate(SearchText);
            return _can;
        }

        /// <summary>
        /// Called when [load].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private async void OnLoad(object parameter)
        {
            InProgress = true;
            await Task.Run(() =>
            {
                var svc = new ImageSearchService(_container);
                ImagesPathList = new ObservableCollection<ImageSearchModel>(svc.GetAllImages(SearchText));

            });
            InProgress = false;
        }

        /// <summary>
        /// Validates the specified search text field.
        /// </summary>
        /// <param name="searchTextField">The search text field.</param>
        /// <returns>TRUE if input is valid; FALSE otherwise</returns>
        private bool Validate(string searchTextField)
        {
            //validation logic to not accept any special characters except ','
            var regexItem = new Regex("^[a-zA-Z0-9 ,]*$");
            return regexItem.IsMatch(searchTextField);
        }
    }
}
