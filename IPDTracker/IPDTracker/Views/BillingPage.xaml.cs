using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IPDTracker.ViewModels;
using IPDTracker.Models;

namespace IPDTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillingPage : ContentPage
    {
        BillingPageViewModel viewModel;
        public BillingPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BillingPageViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as BillingEntry;
            if (item == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewBillingEntryPage()));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}