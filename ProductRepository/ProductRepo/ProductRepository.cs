using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductRepository.Data;
using ProductRepository.Model;
using ProductRepository.ProductRepo.Interface;


namespace ProductRepository.ProductRepo
{
   public class ProductRepository: IProductRepository
    {
        private readonly ProductContext _context;
        public ProductRepository(ProductContext context)
        {
            _context = context;
        }
        public async Task<int> AddProductAsync(Product product)
        {
           return  await _context.Database.ExecuteSqlRawAsync(
             "EXEC AddProduct @ProductName, @Price, @Quantity",
             new SqlParameter("@ProductName", product.ProductName),
             new SqlParameter("@Price", product.Price),
             new SqlParameter("@Quantity", product.Quantity)
            );
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
           var result = await _context.Database.ExecuteSqlRawAsync(
            "EXEC DeleteProduct @ProductID",
            new SqlParameter("@ProductID", id)
            );
            return (result>0) ? true :  false;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var products = await _context.Products
        .FromSqlRaw("EXEC GetProductById @ProductID={0}", id)
        .ToListAsync(); // Fetch all results first

            return  products.FirstOrDefault(); // Apply LINQ on the in-memory list

        }
        public async  Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.FromSqlRaw("EXEC GetProducts").ToListAsync();
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            var result = await _context.Database.ExecuteSqlRawAsync(
             "EXEC UpdateProduct @ProductID, @ProductName, @Price, @Quantity",
             new SqlParameter("@ProductID", product.ProductID),
             new SqlParameter("@ProductName", product.ProductName),
             new SqlParameter("@Price", product.Price),
             new SqlParameter("@Quantity", product.Quantity)
         );

           return (result > 0) ? true : false;
        }
    }
}
