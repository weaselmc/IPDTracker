
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

using SQLite;

namespace IPDTracker.Models
{
    public class BillingEntry : INotifyPropertyChanged
    {

        private string _clientname;
        private DateTime _billingdate;
        private TimeSpan _billingtime;
        private string _notes;

        [PrimaryKey]
        public Guid Id { get; set; }
        public string ClientName
        {
            get { return _clientname; }
            set { SetProperty(ref _clientname, value); }        
        }
        public DateTime BillingDate
        {
            get { return _billingdate; }
            set { SetProperty(ref _billingdate, value); }
        }
        public TimeSpan BillingTime
        {
            get{ return _billingtime; }
            set { SetProperty(ref _billingtime, value); }
        }

        public string BillingTimeToString
        {
            get { return new DateTime(_billingtime.Ticks).ToString("HH:mm"); }
        }

        public string BillingDateToString
        {
            get { return _billingdate.ToString("dd-MMM-yyyy"); }
        }

        public DateTime DateModified { get; set; }

        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
