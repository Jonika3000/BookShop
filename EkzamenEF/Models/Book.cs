using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkzamenEF.Models
{
    internal class Book
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }   
        public int idAuthor { get; set; }
        [ForeignKey("idAuthor")]
        public Author author { get; set; } 
        public int idHouse { get; set; }
        [ForeignKey("idHouse")]
        public PublishingHouse publishingHouse { get; set; }
        public int pagesCount { get; set; } 
        public string genre { get; set; }   
        public DateTime date { get; set; }  
        public double price { get;set; }
        public double priceForSale { get; set; }
        public string continuation { get; set; }
        public byte[] photo { get; set; }
        public int count { get; set; }

    }
}
