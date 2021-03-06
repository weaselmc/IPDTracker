﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using IPDTracker.Models;
using Xamarin.Forms;

using IPDTracker.Views;
using IPDTracker.Services;

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
                try
                {
                    await AzureDataStore.DefaultStore.AddItemAsync(_entry);
                }

                catch
                {
                    await LocalDataStore.AddItemAsync(_entry);
                }
            });
            MessagingCenter.Subscribe<BillingEntryDetailPage, BillingEntry>
                (this, "UpdateEntry", async (obj, entry) =>
                {
                    var _entry = Items.Where((BillingEntry arg) => 
                    arg.Id == entry.Id).FirstOrDefault();
                    _entry.ClientName = entry.ClientName;
                    _entry.BillingDate = entry.BillingDate;
                    _entry.BillingTime = entry.BillingTime;
                    _entry.Notes = entry.Notes;
                    //Items.Add(_entry);
                    try
                    {
                        await AzureDataStore.DefaultStore.UpdateItemAsync(_entry);
                    }
                    catch
                    {
                        await LocalDataStore.UpdateItemAsync(_entry);
                    }
                });
            MessagingCenter.Subscribe<BillingEntryDetailPage, BillingEntry>
                (this, "DeleteEntry", async (obj, entry) =>
                {
                    var _entry = Items.Where((BillingEntry arg) => 
                    arg.Id == entry.Id).FirstOrDefault();
                    Items.Remove(_entry);
                    try
                    {
                        await AzureDataStore.DefaultStore.DeleteItemAsync(_entry.Id.ToString());
                    }
                    catch
                    {
                        await LocalDataStore.DeleteItemAsync(_entry.Id.ToString());
                    }
                    
                });
        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            
            await AzureDataStore.DefaultStore.SyncAsync();
            
            try
            {
                Items.Clear();
                IEnumerable<BillingEntry> items;
                try
                {
                    items = await AzureDataStore.DefaultStore.GetItemsAsync(true);
                }
                catch
                {
                    items = await LocalDataStore.GetItemsAsync(true);
                }
                foreach (var item in items)
                {
                    Items.Add(item);
                }
                //Items.Add(new BillingEntry()
                //{
                //    Id = Guid.NewGuid(),
                //    ClientName = "Nelson Muntz",
                //    BillingDate = DateTime.Now,
                //    BillingTime = new TimeSpan(1, 22, 0),
                //    Notes = "Horrible laugh!!!"
                //});
                //Items.Add(new BillingEntry()
                //{
                //    Id = Guid.NewGuid(),
                //    ClientName = "Jimbo Jones",
                //    BillingDate = DateTime.Now,
                //    BillingTime = new TimeSpan(0, 57, 0),
                //    Notes = "Loves Beanies!!!"
                //});
                //Items.Add(new BillingEntry()
                //{
                //    Id = Guid.NewGuid(),
                //    ClientName = "Dolphin Rainbow",
                //    BillingDate = DateTime.Now,
                //    BillingTime = new TimeSpan(2, 42, 0),
                //    Notes = "Stupid Hippie Parents!!!"
                //});
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
