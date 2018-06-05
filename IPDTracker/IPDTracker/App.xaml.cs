using System;
using System.Threading.Tasks;
using IPDTracker.Services;
using IPDTracker.Views;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Iconize;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace IPDTracker
{
    
    public partial class App : Application
	{
        public static string AzureSchemaUrl = "ipdtracker.azurewebsites.net";
        public static string AzureBackendUrl = @"https://" + AzureSchemaUrl;
        
        public App ()
		{
			InitializeComponent();
            Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());
            DependencyService.Register<AzureDataStore>();
           
            MainPage = new RootPage();
        }

        public interface IAuthenticate
        {
            Task<bool> Authenticate();
        }

        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
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
