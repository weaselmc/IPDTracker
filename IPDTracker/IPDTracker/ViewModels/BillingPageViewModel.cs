using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using IPDTracker.Models;
using Xamarin.Forms;

using IPDTracker.Views;

namespace IPDTracker.ViewModels
{
    class BillingPageViewModel : BaseViewModel
    {
        public ObservableCollection<BillingEntry> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public BillingPageViewModel()
        {
            Title = "Billing Entries";
            Items = new ObservableCollection<BillingEntry>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewBillingEntryPage, BillingEntry>
                (this, "AddEntry", async (obj, entry) =>
            {
                var _entry = entry as BillingEntry;
                Items.Add(_entry);
                //await DataStore.AddItemAsync(_entry);
            });
            MessagingCenter.Subscribe<NewBillingEntryPage, BillingEntry>
                (this, "UpdateEntry", async (obj, entry) =>
                {
                    //var _entry = entry as BillingEntry;
                    //Items.Add(_entry);
                    //await DataStore.AddItemAsync(_entry);
                });
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                //foreach (var item in items)
                //{
                //    Items.Add(item);
                //}
                Items.Add(new BillingEntry()
                {
                    Id = Guid.NewGuid(),
                    ClientName = "Nelson Muntz",
                    BillingDate = DateTime.Now,
                    BillingTime = new TimeSpan(1, 22, 0),
                    Notes = "Horrible laugh!!!"
                });
                Items.Add(new BillingEntry()
                {
                    Id = Guid.NewGuid(),
                    ClientName = "Jimbo Jones",
                    BillingDate = DateTime.Now,
                    BillingTime = new TimeSpan(0, 57, 0),
                    Notes = "Loves Beanies!!!"
                });
                Items.Add(new BillingEntry()
                {
                    Id = Guid.NewGuid(),
                    ClientName = "Dolphin Rainbow",
                    BillingDate = DateTime.Now,
                    BillingTime = new TimeSpan(2, 42, 0),
                    Notes = "Stupid Hippie Parents!!!"
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
