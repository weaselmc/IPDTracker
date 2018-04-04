﻿using System;
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
    public partial class BillingEntryDetailPage : ContentPage
    {
        BillingEntryDetailViewModel viewModel;
        public BillingEntryDetailPage(BillingEntryDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public BillingEntryDetailPage()
        {
            InitializeComponent();

            var item = new BillingEntry()
            { Id = Guid.NewGuid() };

            viewModel = new BillingEntryDetailViewModel(item);
            BindingContext = viewModel;
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "UpdateItem", viewModel.Item);
            await Navigation.PopModalAsync();
        }
    }
}