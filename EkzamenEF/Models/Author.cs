using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EkzamenEF.Models
{
    public class Author
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public byte[] avatar { get; set; }
        public List<Book> books { get; set; }
    }
}
