using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Bills
{
    public class BillDetail : IViewModel
    {
        public int ID { get; set; }
        public int HeaderID { get; set; }
        public int ItemID { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
    }
}
