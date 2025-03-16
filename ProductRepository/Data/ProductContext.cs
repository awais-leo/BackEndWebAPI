using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductRepository.Model;


namespace ProductRepository.Data
{
    public class ProductContext:DbContext
    {
        
        public ProductContext()
        {
        }
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            
        }
        
        public virtual DbSet<Product> Products { get; set; }
    }
}
