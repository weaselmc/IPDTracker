﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPDTrackerWebApp.Model
{
    public class BillingEntry
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public DateTime BillingDate { get; set; }
        public TimeSpan BillingTime { get; set; }
        public string Notes { get; set; }
        public DateTime LastModified { get; set; }
    }
}
