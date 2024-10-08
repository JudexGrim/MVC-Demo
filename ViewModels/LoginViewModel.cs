using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage ="This Field is Required.")]
        public string User {  get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required.")]
        public string Password { get; set; }
    }
}
