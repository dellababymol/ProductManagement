using Microsoft.EntityFrameworkCore;
using ProductManagement.Database.Context;
using ProductManagement.Database.Entities;

namespace ProductManagement.Database.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ProductMgtContext context)
: base(context)
    {
    }

    public ProductMgtContext ProductManagementContext
    {
        get { return Context as ProductMgtContext; }
    }

    public Product GetProductWithProductOptions(Guid id)
    {
        return ProductManagementContext.Products.Include(a => a.ProductOptions).SingleOrDefault(a => a.Id == id);
    }

    public bool ProductExists(Guid id)
    {
        return ProductManagementContext.Products.Any(e => e.Id == id);
    }
}
