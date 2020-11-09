/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Assessment.Common
{
    /// <summary>
    /// Base class for all ViewModels
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">The storage.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>TRUE if value is set; FALSE otherwise.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            bool changed = false;
            if (!object.Equals(storage, value))
            {
                storage = value;
                changed = true;
                OnPropertyChanged(propertyName);
            }
            return changed;
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
