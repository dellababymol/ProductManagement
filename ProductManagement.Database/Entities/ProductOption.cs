using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Database.Entities;

[Table("ProductOptions")]
public class ProductOption : NameDescriptionBase
{   

    [ForeignKey("Product")]
    public Guid ProductId { get; set; }    
    public  Product Product { get; set; }
}
