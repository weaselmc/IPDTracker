using System;

using Xamarin.Forms;

using IPDTracker.Models;
using IPDTracker.Views;


namespace IPDTracker.ViewModels
{
    public class BillingEntryDetailViewModel : BaseViewModel
    {
        public BillingEntry Item { get; set; }
        public BillingEntryDetailViewModel(BillingEntry item = null)
        {
            Title = item?.ClientName;
            Item = item;            
        }


    }
}
