using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Predmet
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ImePredmet { get; set; }
        [StringLength(100)]
        public string? Programa { get; set; }
    }
}
