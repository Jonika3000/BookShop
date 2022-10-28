using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkzamenEF.Models
{
    public class Account
    {
        [Key]
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
         public string email { get; set; }
        public bool admin { get; set; }
        public byte[] avatar { get; set; }
    }
}
