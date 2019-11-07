using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Microsoft.Extensions.DependencyInjection;

using Xamarin.Forms;

using MyFirstMobileApp.Shared.Models;
using MyFirstMobileApp.Services;
using MyFirstMobileApp.Models;

namespace MyFirstMobileApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        IRepository<Item> dataStore;
        public IRepository<Item> DataStore
        {
            get
            {
                if (dataStore != null)
                    return dataStore;
                dataStore = DependencyService.Get<IRepository<Item>>();
                return dataStore;
            }
            set => dataStore = value;
        }

        bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        string title = string.Empty;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
