using System;
using System.IO;
using System.Text;
using SQLite;

namespace IPDTracker.Services
{
    static class DataStore
    {
        static string dbname = "ipd.db.sqlite";

#if __ANDROID__
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
#else
#if __IOS__
        public static string path = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library");
#else
        //UWP
        public static string path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
#endif
#endif
        public static string DbPath = Path.Combine(path, dbname);
        public static SQLiteAsyncConnection DbConn = new SQLiteAsyncConnection(DbPath);
    }
}
