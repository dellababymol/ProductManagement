using ProductManagement.Database.Entities;
using ProductManagement.Database.Repositories;

namespace ProductManagement.API.Services;

public class ProductService : IProductService
{

    private readonly ILogger<ProductService> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IProductOptionRepository _productOptionRepository;   
    public ProductService( ILogger<ProductService> logger,IProductRepository productRepository, IProductOptionRepository productOptionRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
        _productOptionRepository = productOptionRepository;

    }
    public IEnumerable<Product> GetProducts()
    {
        try
        {

            return _productRepository.GetAll();
        }
        catch (Exception ex)
        {
            throw new Exception(" Error while getting Product.", ex);
        }
    }
    public IEnumerable<Product> GetProductByName(string name)
    {
        try
        {
            return _productRepository.Find(x => x.Name == name);
            // return _productRepository.GetProductByName(name);
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Product.", ex);
        }
    }
    public Product GetProductById(Guid id)
    {
        try
        {
            var product = _productRepository.Get(id);

            if (product == null)
            {
                //return
                throw new Exception("Not Found");
            }

            return product;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Product.", ex);
        }
    }
    public bool PostProduct(Product product)
    {
        try
        {
            _productRepository.Add(product);
            return true;

        }
        catch (Exception ex)
        {
            throw new Exception("Error while creating Product.", ex);
        }
    }
    public bool PutProduct(Product product)
    {
        try
        {            
            _productRepository.Update(product);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while updating Product.", ex);
        }
    }
    public bool DeleteProduct(Guid id)
    {
        try
        {
            var product = _productRepository.Find(e => e.Id == id).FirstOrDefault();  
            _productRepository.Remove(product);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while deleting Product and its Options.", ex);
        }
    }
    public IEnumerable<ProductOption> GetProductOptionsOfProduct(Guid productId)
    {
        try
        {
            if (productId != Guid.Empty && _productOptionRepository.Find(x => x.ProductId == productId) == null)
            {
                throw new Exception($"Product with Id:{productId} does not exist");
            }
            return _productOptionRepository.GetProductOptionsOfProduct(productId);
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Product Options", ex);
        }

    }
    public Product GetProductWithProductOptions(Guid productId)
    {
        try
        {
            return _productRepository.GetProductWithProductOptions(productId);
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Product and its Options.", ex);
        }
    }
    public ProductOption GetProductOptionsWithProduct(Guid productId, Guid id)
    {
        try
        {
            if (productId != Guid.Empty && _productOptionRepository.Find(x => x.ProductId == productId) == null)
            {
                throw new Exception($"Product with Id:{productId} doesnot exist");
            }
            return _productOptionRepository.GetProductOptionsWithProduct(productId, id);
        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Product Options", ex);
        }
    }
    public void CreateProductOption(Guid productId, ProductOption productOption)
    {
        try
        {
            if (productId == Guid.Empty)
            {
                throw new Exception($"Product Id is null");
            }

            var product = _productRepository.Find(x => x.Id == productId).FirstOrDefault();
            if (product == null)
            {
                throw new Exception($"Product with Id:{productId} doesnot exist");
            }
            if (productOption == null)
            {
                throw new Exception($"Incorrect request");
            }
            _productOptionRepository.Add(new ProductOption
            {
                Id = productOption.Id,
                Description = productOption.Description,
                Name = productOption.Name,
                ProductId = productId
            });
        }
        catch (Exception ex)
        {
            throw new Exception("Error while creating Product Options", ex);
        }
    }
    public void PutProductOption(Guid productId, ProductOption productOption, Guid optionId)
    {
        try
        {
            if (optionId != productOption.Id)
            {
                throw new Exception($"Incorrect request");
            }
            _productOptionRepository.Update(productOption);

        }
        catch (Exception ex)
        {
            throw new Exception("Error while updating Product Options", ex);
        }
    }
    public void DeleteProductOption(Guid optionId)
    {
        try
        {
            var productOption = _productOptionRepository.Find(x => x.Id == optionId).FirstOrDefault();
            if (productOption == null)
            {
                throw new Exception($"Product Option not found");
            }

            _productOptionRepository.Remove(productOption);

        }
        catch (Exception ex)
        {
            throw new Exception("Error while getting Product Options.", ex);
        }
    }
    public bool ProductExists(Guid id)
    {
        return _productRepository.ProductExists(id);
    }
}

