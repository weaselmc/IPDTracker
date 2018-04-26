using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

using IPDTracker.Views;
using IPDTracker.Models;
using System.Runtime.CompilerServices;
using Plugin.Iconize;
using Plugin.Iconize.Fonts;

namespace IPDTracker.ViewModels
{
    class RootPageMasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<RootPageMenuItem> MenuItems { get; set; }

        public RootPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<RootPageMenuItem>(new[]
            {
                    new RootPageMenuItem { Id = 0, Title = "Items",
                        Icon="fas-address-book", TargetType =typeof(ItemsPage) },
                    new RootPageMenuItem { Id = 1, Title = "Billing Entries",
                        Icon="fas-calendar", TargetType=typeof(BillingPage) },
                    new RootPageMenuItem { Id = 2, Title = "About",
                        Icon ="fas-question-circle", TargetType=typeof(AboutPage) }
                });
            //int i = 3;
            //foreach (Icon icon in FontAwesomeCollection.SolidIcons)
            //{
            //    MenuItems.Add(new RootPageMenuItem { Id = i, Title = icon.Key, Icon = icon.Key, TargetType = typeof(ItemsPage) });
            //    i++;
            //}
            //foreach (Icon icon in FontAwesomeCollection.RegularIcons)
            //{
            //    MenuItems.Add(new RootPageMenuItem { Id = i, Title = icon.Key, Icon = icon.Key, TargetType = typeof(ItemsPage) });
            //    i++;
            //}
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
