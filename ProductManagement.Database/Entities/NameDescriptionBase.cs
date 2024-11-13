using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Database.Entities;

public abstract class NameDescriptionBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(100)]
    public string Description { get; set; }

    public bool Active { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.Now;   
    public DateTime LastUpdatedOn { get; set; } = DateTime.Now;
    
}
