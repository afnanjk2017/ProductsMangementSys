using Microsoft.EntityFrameworkCore;
using Products.Models;
namespace Products.Data 
{
    public class ApplicationDbContext : DbContext   
    {

       
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
            {
            }

        // Define your DbSets (tables) here
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }




    }
}
