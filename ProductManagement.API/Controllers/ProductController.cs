using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Services;
using ProductManagement.Database.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("(products)")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ILogger _logger;

    public ProductController(IProductService productService, ILogger logger)
    {
        _productService = productService;
        _logger = logger;
    }

    /// <summary>
    /// 1. GET: products
    /// </summary>
    /// <returns>products</returns>
    [HttpGet]
    public ObjectResult GetAll()
    {
        try
        {
            IEnumerable<Product> products = new List<Product>();
            products = _productService.GetProducts();
            return Ok(products);
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Product.", ex);
        }
    }

    /// <summary>
    /// 2. GET /products?name={name} - Find products by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    
    [HttpGet("{name}")]
    public ObjectResult FindByName([FromQuery] string name)
    {
        var products = _productService.GetProductByName(name);
        return Ok(products);
    }

    /// <summary>
    /// 3. GET: products/5
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Product), 200)]
    [ProducesResponseType(typeof(string), 404)]
    
    public ObjectResult FindProductById(Guid id)
    {
        if (id==null)
        {
            _logger.LogError($"Error: the Id is null or missing.");
            return BadRequest(StatusCodes.Status400BadRequest);
        }
        var product = _productService.GetProductById(id);
        if (product == null)
        {
            var message = $"No products were found matching the ID: ${id}";

            _logger.LogWarning(message);
            return NotFound(message);
        }
        return Ok(product);
    }    

    /// <summary>
    /// 4. GET: products/CreateProduct
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    public ObjectResult CreateProduct([FromBody] Product product)
    {
        if (product == null)
        {
            _logger.LogError($"Error: The incoming product was invalid.");
            return BadRequest(StatusCodes.Status400BadRequest);
        }
        var requestWasSuccessful = _productService.PostProduct(product);
        if (requestWasSuccessful)
        {          

            var successMessage = $"The product was created successfully.";
            _logger.LogInformation($"Info: {successMessage}");
            return Created();
        }
        return StatusCode(StatusCodes.Status500InternalServerError, "There was an issue creating the product.");
    }

    /// <summary>
    /// 5. POST: products/Edit
    /// </summary>   
    /// <param name="product"></param>
    /// <returns></returns>
    [HttpPut]
    [ValidateAntiForgeryToken]
    public ObjectResult EditProduct([FromBody] Product product)
    {
        if (product == null || product.Id == null)
        {
            _logger.LogError($"Error: The incoming product was null or invalid.");
            return BadRequest(StatusCodes.Status400BadRequest);
        }
       
        if (!_productService.ProductExists(product.Id))
        {
            _logger.LogError($"Error: The incoming updatedFormContent is invalid.");
            return BadRequest(StatusCodes.Status400BadRequest);
        }
        var requestWasSuccessful = _productService.PutProduct(product);
        if (requestWasSuccessful)
        {

            var successMessage = $"The product with id {product.Id} was updated  successfully.";
            _logger.LogInformation($"Info: {successMessage}");
            return Ok(successMessage);
        }
        return StatusCode(StatusCodes.Status500InternalServerError, "There was an issue updating the product.");
    }

    /// <summary>
    /// 6. GET: products/Delete/5
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
     [HttpDelete("{id}")]
    public ObjectResult Delete(Guid Id)
    {
        if (Id == null)
        {
            _logger.LogError($"Error: the Id is null or missing.");
            return BadRequest(StatusCodes.Status400BadRequest);
        }
        if (!_productService.ProductExists(Id))
        {
            _logger.LogError($"Error: The incoming updatedFormContent is invalid.");
            return BadRequest(StatusCodes.Status400BadRequest);
        }
        var requestWasSuccessful = _productService.DeleteProduct(Id);
        if (requestWasSuccessful)
        {

            var successMessage = $"The product with id {Id} was updated  successfully.";
            _logger.LogInformation($"Info: {successMessage}");
            return Ok(successMessage);
        }
        return StatusCode(StatusCodes.Status500InternalServerError, "There was an issue updating the product.");
    } 
}

