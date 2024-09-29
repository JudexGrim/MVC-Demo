using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class Item : IViewModel
    {
        [Required (AllowEmptyStrings = false, ErrorMessage ="This Field Is Required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please Enter A Valid Integer")]
        public int ID { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field Is Required.")]
        public string Name { get; set; }
        
        [Required (AllowEmptyStrings = false, ErrorMessage ="This Field Is Required.")]
        [Range(0, float.MaxValue, ErrorMessage = "Please Enter A Valid Float")]
        public decimal Price { get; set; }
    }
}
