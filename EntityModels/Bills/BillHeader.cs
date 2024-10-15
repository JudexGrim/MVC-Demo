using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModels.Bills
{
    public class BillHeader : IEntityModel
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string Type { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime BillDate { get; set; }
        public IEnumerable<BillDetail> Details { get; set; }
    }
}
