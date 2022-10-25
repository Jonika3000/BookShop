using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkzamenEF.Models
{
    internal class PublishingHouse
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public byte[] photo { get; set; }
        public string descriptions { get; set; }
        public List<Book> books { get; set; }
    }
}
