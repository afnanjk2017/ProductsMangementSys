using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="غلط عده او دخله من جديد")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        
        public string Description { get; set; }
    }
}
