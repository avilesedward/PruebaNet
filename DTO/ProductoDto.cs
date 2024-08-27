using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class ProductoDto
    {
        [Key]
        public long ProductId { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public int Stock { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(10)]
        public string StatusName { get; set; }

        [Range(0, 100)]
        public int Discount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalPrice { get; set; }
    }
}
