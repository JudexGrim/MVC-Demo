using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class Client : IViewModel
    {

        [Required(AllowEmptyStrings =false, ErrorMessage ="This Field Is Required")]
        [Range(0, int.MaxValue)]
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field Is Required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field Is Required")]
        [RegularExpression("^(Seller|Buyer|Both)", ErrorMessage ="Client Can only be Seller, Buyer or Both")]
        public string Type { get; set; }
    }
}
