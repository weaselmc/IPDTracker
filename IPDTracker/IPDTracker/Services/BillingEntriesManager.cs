using IPDTracker.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPDTracker.Services
{
    public partial class BillingEntriesManager
    {
        static BillingEntriesManager defaultInstance = new BillingEntriesManager();
        public string AppSchema = "net.azurewebsites.ipdtracker";
        public string AzureBackendUrl = @"https://ipdtracker.azurewebsites.net";

        MobileServiceClient msclient;
        IMobileServiceSyncTable<BillingEntry> BillingEntriesTable;

        const string offlineDbPath = @"localstore.db";
        private BillingEntriesManager()
        {

            var store = new MobileServiceSQLiteStore(offlineDbPath);
            store.DefineTable<BillingEntry>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            msclient.SyncContext.InitializeAsync(store);
            BillingEntriesTable = msclient.GetSyncTable<BillingEntry>();
        }

        public static BillingEntriesManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return msclient; }
        }

        public bool IsOfflineEnabled
        {
            get { return BillingEntriesTable is IMobileServiceSyncTable<BillingEntry>; }
        }


    }
}
