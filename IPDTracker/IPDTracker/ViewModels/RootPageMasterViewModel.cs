using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

using IPDTracker.Views;
using IPDTracker.Models;
using System.Runtime.CompilerServices;

namespace IPDTracker.ViewModels
{
    class RootPageMasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<RootPageMenuItem> MenuItems { get; set; }

        public RootPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<RootPageMenuItem>(new[]
            {
                    new RootPageMenuItem { Id = 0, Title = "Items", TargetType=typeof(ItemsPage) },
                    new RootPageMenuItem { Id = 1, Title = "Billing Entries", TargetType=typeof(BillingPage) },
                    new RootPageMenuItem { Id = 2, Title = "About", TargetType=typeof(AboutPage) }
                });
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
