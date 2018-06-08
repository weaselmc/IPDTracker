using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using IPDTracker.Models;
using IPDTracker.Services;

namespace IPDTracker.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        //public IDataStore<Item> ItemDataStore => 
        //    DependencyService.Get<IDataStore<Item>>() ?? new DbItemStore();
        public IDataStore<BillingEntry> LocalDataStore =>
           DependencyService.Get<IDataStore<BillingEntry>>() ?? new LocalDataStore();

        //public AzureDataStore AzureDataStore = AzureDataStore;

        bool isBusy = false;        

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        
        #region INotifyPropertyChanged

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
