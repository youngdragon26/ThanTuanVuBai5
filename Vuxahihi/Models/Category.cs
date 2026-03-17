using System.ComponentModel.DataAnnotations;

namespace Vuxahihi.Models
{
    public class Category
    {

            public int CategoryId { get; set; }
            [Key]
            public string CategoryName { get; set; }
            //Navigation property
            public List<Product> Products { get; set; }
        }

    }
