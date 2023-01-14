using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Model> Models { get; set; } = null!;
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB; Database = garage; Trusted_Connection = True;");
        }
    }
}
