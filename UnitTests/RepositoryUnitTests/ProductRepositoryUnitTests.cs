using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductRepository.Data;
using ProductRepository.Model;
using ProductRepository.ProductRepo;
using ProductRepository.ProductRepo.Interface;

namespace UnitTests.RepositoryUnitTests
{
    [TestFixture]
    public class ProductRepositoryUnitTests
    {
        private readonly ProductRepository.ProductRepo.ProductRepository productRepository;
        private readonly  Mock<ProductContext> _mockContext;


 
        public  ProductRepositoryUnitTests()
        {
            _mockContext = new Mock<ProductContext>();

            productRepository = new ProductRepository.ProductRepo.ProductRepository(_mockContext.Object);
                               

        }

        [Test]
        public async Task WhenGetProductsAsyncIsCalled_ThenItReturnsProductsList()
        {
            var mockProducts = new List<Product>
            {
                new Product {ProductID = 1, ProductName = "Product1", Price = 10, Quantity = 10},
                new Product {ProductID = 2, ProductName = "Product2", Price = 20, Quantity = 20},
                new Product {ProductID = 3, ProductName = "Product3", Price = 30, Quantity = 30}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Product>>();
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(mockProducts.Provider);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(mockProducts.Expression);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(mockProducts.ElementType);
            mockDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => mockProducts.GetEnumerator());

           var _mockContext = new Mock<ProductContext>();
            _mockContext.Setup(c => c.Products).Returns(mockDbSet.Object);

            //_mockContext.Setup(c => c.Products.FromSqlRaw(It.IsAny<string>()))
            //    .Returns(mockDbSet.Object);
            var productRepository = new ProductRepository.ProductRepo.ProductRepository(_mockContext.Object);

            var products = await productRepository.GetProductsAsync();

            Assert.That(products.Count(), Is.EqualTo(3));

           

        }
    }
}