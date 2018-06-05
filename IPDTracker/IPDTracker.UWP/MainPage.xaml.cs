using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Popups;
using IPDTracker.Services;
using System.Linq;
using Windows.Security.Credentials;
using Windows.ApplicationModel.Activation;

namespace IPDTracker.UWP
{
    public sealed partial class MainPage : IPDTracker.App.IAuthenticate
    {
        public MainPage()
        {
            this.InitializeComponent();
            IPDTracker.App.Init(this);
            LoadApplication(new IPDTracker.App());
        }

        private MobileServiceUser user;

        //public async Task<bool> Authenticate()
        //{
        //    string message = string.Empty;
        //    var success = false;

        //    try
        //    {
        //        // Sign in with MS login using a server-managed flow.
        //        if (user == null)
        //        {
        //            user = await AzureDataStore.DefaultStore.CurrentClient
        //                .LoginAsync(MobileServiceAuthenticationProvider
        //                .MicrosoftAccount, IPDTracker.App.AzureSchemaUrl); //"ipdtracker.azurewebsites.net/.auth/login/microsoftaccount/callback??"
        //            if (user != null)
        //            {
        //                success = true;
        //                AzureDataStore.DefaultStore.CurrentClient.CurrentUser = user;
        //                message = string.Format("You are now signed-in as {0}.", user.UserId);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        message = string.Format("Authentication Failed: {0}", ex.Message);
        //    }

        //    // Display the success or failure message.
        //    await new MessageDialog(message, "Sign-in result").ShowAsync();

        //    return success;
        //}
        public async Task<bool> Authenticate()
        {
            string message;
            bool success = false;

            // This sample uses the MS provider.
            var provider = MobileServiceAuthenticationProvider.MicrosoftAccount;

            // Use the PasswordVault to securely store and access credentials.
            PasswordVault vault = new PasswordVault();
            PasswordCredential credential = null;

            try
            {
                // Try to get an existing credential from the vault.
                credential = vault.FindAllByResource(provider.ToString()).FirstOrDefault();
            }
            catch (Exception)
            {
                // When there is no matching resource an error occurs, which we ignore.
            }

            if (credential != null)
            {
                // Create a user from the stored credentials.
                user = new MobileServiceUser(credential.UserName);
                credential.RetrievePassword();
                user.MobileServiceAuthenticationToken = credential.Password;

                // Set the user from the stored credentials.
                AzureDataStore.DefaultStore.CurrentClient.CurrentUser = user;

                // Consider adding a check to determine if the token is 
                // expired, as shown in this post: http://aka.ms/jww5vp.

                success = true;
                message = string.Format("Cached credentials for user - {0}", user.UserId);
            }
            else
            {
                try
                {
                    // Login with the identity provider.
                    user = await AzureDataStore.DefaultStore.CurrentClient
                        .LoginAsync(provider, IPDTracker.App.AzureSchemaUrl);

                    // Create and store the user credentials.
                    credential = new PasswordCredential(provider.ToString(),
                        user.UserId, user.MobileServiceAuthenticationToken);
                    vault.Add(credential);

                    success = true;
                    message = string.Format("You are now logged in - {0}", user.UserId);
                }
                catch (MobileServiceInvalidOperationException)
                {
                    message = "You must log in. Login Required";
                }
            }

            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand("OK"));
            await dialog.ShowAsync();

            return success;
        }
    }
}
