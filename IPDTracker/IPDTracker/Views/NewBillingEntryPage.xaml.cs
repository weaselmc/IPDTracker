using IPDTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPDTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewBillingEntryPage : ContentPage
	{
        public BillingEntry Entry { get; set; }
		public NewBillingEntryPage ()
		{
			InitializeComponent ();
            Entry = new BillingEntry();
            Entry.Id = Guid.NewGuid();
            Entry.BillingDate = DateTime.Now;
            BindingContext = this;
		}

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddEntry", Entry);
            await Navigation.PopModalAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}