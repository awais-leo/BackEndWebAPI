using ProductRepository.Model;
using ProductRepository.ProductRepo.Interface;

namespace ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() => await _repository.GetProductsAsync();

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");
            return product;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            if (product.Price <= 0 || product.Quantity < 0)
                throw new ArgumentException("Invalid price or quantity");

            return await _repository.AddProductAsync(product);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await _repository.UpdateProductAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repository.DeleteProductAsync(id);
        }
    }
}
