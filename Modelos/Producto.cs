
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    [Table("Producto")]
    public class Producto
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
        [Required]
        public int Status { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }
    }
}
