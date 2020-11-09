/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using Assessment.ImageSearch;
using Assessment.ImageSearch.Service;
using Microsoft.Practices.Unity;
using System.Windows;

namespace ImageSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUnityContainer _parentContainer;
        public MainWindow()
        {
            InitializeComponent();
            SearchBar.Focus();
            _parentContainer = Bootstrap.GetUnityContainer();
            DataContext = new MainWindowViewModel(_parentContainer);
        }
    }
}
