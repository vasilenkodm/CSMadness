using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    class UserContext : DbContext
    {
        /*
        public UserContext()
            :  base("DbConnection")
        { }
        */
        public DbSet<Data> DataDS { get; set; }
        public DbSet<Property> PropertyDS { get; set; }
        public DbSet<SubData> SubDataDS { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("test_base");
        }
    } // class UserContext : DbContext
}
