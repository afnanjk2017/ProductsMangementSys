using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class Damagedproducts
    {

        [Key]
        public int Id { get; set; }
        [Required]

        //[ForeignKey("Product")]
        public int Products_Id { get; set; }
        [Required]
        public int Qty { get; set; }
        
    }
}
