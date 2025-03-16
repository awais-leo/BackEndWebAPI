using Microsoft.AspNetCore.Mvc;
using ProductRepository.Model;
using ProductService;
using Swashbuckle.AspNetCore.Annotations;

namespace BackEndWebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService service, ILogger<ProductsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieve all products", Description = "Gets a list of all available products.")]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogInformation("Getting all products");

            return Ok(await _service.GetProductsAsync());
        }

        /// <summary>
        /// Get a product by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product details</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieve a product by ID", Description = "Gets the details of a specific product.")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProduct(int id)
        {
            _logger.LogInformation($"Getting product with id {id}");

            return Ok(await _service.GetProductByIdAsync(id));
        }


        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="product">Product object</param>
        /// <returns>Created product</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new product", Description = "Creates a new product and returns the created product.")]
        [ProducesResponseType(typeof(Product), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
           _logger.LogInformation("Adding a new product");

            int productId = await _service.AddProductAsync(product);
           return CreatedAtAction(nameof(GetProduct), new { id = productId }, product);
            
        }

        /// <summary>
        /// Update an existing product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="product">Updated product data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing product", Description = "Updates an existing product's details.")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            _logger.LogInformation($"Updating product with id {id}");

            if (id != product.ProductID)
            {
                return BadRequest();
            }
            return await _service.UpdateProductAsync(product) ? NoContent() : NotFound();
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a product", Description = "Deletes a product by its ID.")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation($"Deleting product with id {id}");

            return await _service.DeleteProductAsync(id) ? NoContent() : NotFound();
        }
    }
}
