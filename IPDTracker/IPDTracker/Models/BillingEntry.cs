using System;
using System.Collections.Generic;
using System.Text;

namespace IPDTracker.Models
{
    class BillingEntry
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public DateTime BillingDate { get; set; }
        public TimeSpan BillingTime { get; set; }
        public string Notes { get; set; }
    }
}
