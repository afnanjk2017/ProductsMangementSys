using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Models
{
    public class ProductDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]

        //[ForeignKey("Product")]
        public int Products_Id { get; set; }
        [Required]
        public string Images { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public decimal Price { get; set; }
       
       
    }
}
