using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IPDTracker.Models;
using Plugin.Iconize;

namespace IPDTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            //var page = (Page)Activator.CreateInstance(typeof(ItemsPage)); //BillingPage
            //page.Title = "Items";
            //Detail = new IconNavigationPage(page); 
            //MasterBehavior = MasterBehavior.Popover;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as RootPageMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new IconNavigationPage(new NavigationPage(page));
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}