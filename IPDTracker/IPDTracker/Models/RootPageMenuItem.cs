using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPDTracker.Models
{

    public class RootPageMenuItem
    {
        public RootPageMenuItem()
        {
            //TargetType = typeof(RootPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public Type TargetType { get; set; }
    }
}