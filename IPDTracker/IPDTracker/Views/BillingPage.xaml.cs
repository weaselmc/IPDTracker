using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IPDTracker.ViewModels;
using IPDTracker.Models;
using Plugin.Iconize;

namespace IPDTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillingPage : ContentPage
    {
        BillingPageViewModel viewModel;
        bool authenticated = false;

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

            await Navigation.PushModalAsync(new IconNavigationPage
                (new BillingEntryDetailPage
                (new BillingEntryDetailViewModel(item))));

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
            if (authenticated == true)
            {
                if (viewModel.Items.Count == 0)
                {
                    viewModel.LoadItemsCommand.Execute(null);
                    loginButton.IsVisible = false;
                }
            }
        }

        async void loginButton_Clicked(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
                authenticated = await App.Authenticator.Authenticate();

            // Set syncItems to true to synchronize the data on startup when offline is enabled.
            if (authenticated == true)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}