using System.ComponentModel.DataAnnotations;

namespace GPUStoreAPI.Models.DTO
{
    public class GPUDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
