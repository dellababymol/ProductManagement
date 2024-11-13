using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Database.Entities;

[Table("Products")]
public class Product : NameDescriptionBase
{  

    public decimal Price { get; set; }
    public decimal DeliveryPrice { get; set; }
    public List<ProductOption> ProductOptions { get; set; } = new List<ProductOption>();   
}
