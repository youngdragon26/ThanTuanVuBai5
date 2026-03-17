using System.ComponentModel.DataAnnotations;

namespace Vuxahihi.Models
{
    public class Product
    {
      
            [Key]
            public int ProductId { get; set; }
            [Required]
            public string? ProductName { get; set; }
            [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
            public decimal Price { get; set; }
            //Foreign key
            public int CategoryId { get; set; }
            //Navigation property
            public Category? Category { get; set; }
        

    }
}
