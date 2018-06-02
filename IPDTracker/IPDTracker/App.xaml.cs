using System;
using IPDTracker.Services;
using IPDTracker.Views;
using Plugin.Iconize;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace IPDTracker
{
    
    public partial class App : Application
	{

        public static string AzureBackendUrl = @"http://ipdtracker.azurewebsites.net";


        public App ()
		{
			InitializeComponent();
            Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());
            DependencyService.Register<AzureDataStore>();
           
            MainPage = new RootPage();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
