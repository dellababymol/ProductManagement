using ProductManagement.Database.Context;
using ProductManagement.Database.Entities;

namespace ProductManagement.Database.Repositories;

public class ProductOptionRepository : Repository<ProductOption>, IProductOptionRepository
{
    public ProductOptionRepository(ProductMgtContext context)
: base(context)
    {
    }

    public ProductMgtContext ProductManagementContext
    {
        get { return Context as ProductMgtContext; }
    }

    public ProductOption GetProductOptionsWithProduct(Guid productId, Guid id)
    {
        return ProductManagementContext.ProductOptions.SingleOrDefault(x => x.Id == id && x.ProductId == productId) ?? new ProductOption();
    }

    public IEnumerable<ProductOption> GetProductOptionsOfProduct(Guid productId)
    {
        List<ProductOption> ProductOptionlst = new List<ProductOption>();
        ProductOptionlst.AddRange(ProductManagementContext.ProductOptions.Where(x => x.ProductId == productId));
        return ProductOptionlst;
    }
}
