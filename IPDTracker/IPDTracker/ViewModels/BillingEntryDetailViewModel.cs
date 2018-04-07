using System;

using Xamarin.Forms;

using IPDTracker.Models;
using IPDTracker.Views;


namespace IPDTracker.ViewModels
{
    public class BillingEntryDetailViewModel : BaseViewModel
    {
        public BillingEntry Entry { get; set; }
        public BillingEntryDetailViewModel(BillingEntry item = null)
        {
            Title = item?.ClientName;
            Entry = new BillingEntry();
            Entry.ClientName = item?.ClientName;
            Entry.Notes = item?.Notes;
            if (item != null) { 
            Entry.Id = item.Id;
            Entry.BillingDate = item.BillingDate;
            Entry.BillingTime = item.BillingTime; }
        }


    }
}
