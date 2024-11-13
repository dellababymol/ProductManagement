using ProductManagement.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Database.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Product GetProductWithProductOptions(Guid id);
    bool ProductExists(Guid id);
}

