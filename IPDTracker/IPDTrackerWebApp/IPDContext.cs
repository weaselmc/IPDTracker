using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IPDTrackerWebApp.Model;
using Microsoft.EntityFrameworkCore;

namespace IPDTrackerWebApp
{
    public class IPDContext : DbContext
    {
        public DbSet<BillingEntry> BillingEntries { get; set; }

        public IPDContext(DbContextOptions<IPDContext> options) : base(options)
        {

        }        
    }
}
