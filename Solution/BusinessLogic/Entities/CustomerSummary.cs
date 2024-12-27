using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class CustomerSummary
    {
        public int FinishedCount { get; set; }
        public int PendingCount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
