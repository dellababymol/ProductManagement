using ProductManagement.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Database.Repositories;

public interface IProductOptionRepository : IRepository<ProductOption>
{
    IEnumerable<ProductOption> GetProductOptionsOfProduct(Guid productId);
    ProductOption GetProductOptionsWithProduct(Guid productId, Guid id);
}