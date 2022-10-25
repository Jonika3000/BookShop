using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EkzamenEF.Models
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Book> books { get; set; }  
        public DbSet<PublishingHouse> publishingHouses { get; set; } 
        public DbSet<Author> authors { get; set; }
        public DbSet<Account> accounts { get; set; }    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-57UKIQP;Initial Catalog=EkzamenEntity;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}
