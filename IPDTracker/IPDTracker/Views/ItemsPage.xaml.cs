using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IPDTracker.Models;
using IPDTracker.ViewModels;
using Plugin.Iconize;

namespace IPDTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            BindingContext = viewModel = new ItemsViewModel();

            InitializeComponent();
            
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushModalAsync(
                new IconNavigationPage(
                    new ItemDetailPage(
                        new ItemDetailViewModel(item))));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new IconNavigationPage(
                    new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            foreach(IconToolbarItem item in ToolbarItems)
            {
                var visible = item.IsVisible;
                item.IsVisible = false;
                item.IsVisible = visible;
            }

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}