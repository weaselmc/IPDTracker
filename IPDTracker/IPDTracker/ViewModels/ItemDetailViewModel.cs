using System;

using Xamarin.Forms;

using IPDTracker.Models;
using IPDTracker.Views;


namespace IPDTracker.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
            
        }


    }
}
