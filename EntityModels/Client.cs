using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModels
{
    public class Client : IEntityModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
}
