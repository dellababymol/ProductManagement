using ProductManagement.Database.Entities;

namespace ProductManagement.API.Services;

public interface IProductService
{
    IEnumerable<Product> GetProducts();
    IEnumerable<Product> GetProductByName(string name);
    Product GetProductById(Guid id);
    bool PostProduct(Product product);
    bool PutProduct(Product product);
    bool DeleteProduct(Guid id);
    Product GetProductWithProductOptions(Guid productId);
    ProductOption GetProductOptionsWithProduct(Guid productId, Guid id);
    void CreateProductOption(Guid productId, ProductOption productOption);
    void PutProductOption(Guid productId, ProductOption productOption, Guid optionId);
    void DeleteProductOption(Guid optionId);
    IEnumerable<ProductOption> GetProductOptionsOfProduct(Guid productId);
    bool ProductExists(Guid id);
}

