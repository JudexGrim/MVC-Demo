using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class User : IViewModel
    {
        [Required(AllowEmptyStrings =false)]
        [Range(0, int.MaxValue)]
        public int ID { get; set; }
        
        
        public string Username { get; set; }
        
        [Required(AllowEmptyStrings =false)]
        public string Password { get; set; }
        
        public string Email { get; set; }
    }
}
