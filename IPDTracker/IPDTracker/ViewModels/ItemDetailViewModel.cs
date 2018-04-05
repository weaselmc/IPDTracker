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
            //create duplicate of item to edit
            Item  = new Item()
            {
                Id = item?.Id,
                Text = item?.Text,
                Description = item?.Description
            };
            
        }


    }
}
