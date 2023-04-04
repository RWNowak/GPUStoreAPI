using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPUStoreAPI.Models
{
    public class GPU
    {
        [Required]
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public int Price { get; set; }
        [Required]
        [StringLength(50)]
        public string? Chip { get; set; }
        [Required]
        [StringLength(50)]
        public string? MemoryType { get; set; }
        [Required]
        [StringLength(50)]
        public string? Memory { get; set; }
    }
}
