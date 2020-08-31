using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;

namespace Packt.Shared
{
    public class Northwind : DbContext
    {
        // these properties map to the tables to the database
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Northwind.db");
            optionsBuilder.UseLazyLoadingProxies().UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Example of using Fluent API instead attributes            
            // to limit the length of category name to 15
            modelBuilder.Entity<Category>()
                .Property(category =>category.CategoryName)
                .IsRequired()   // Not Null
                .HasMaxLength(15);
            
            // as sqlite can't work with decimal type propertly we have to convert it to double
            modelBuilder.Entity<Product>().Property(e=>e.Cost).HasConversion<double>();

            // global filter to remove discontinued products
            modelBuilder.Entity<Product>().HasQueryFilter(p=>!p.Discontinued);
        }
    }
}