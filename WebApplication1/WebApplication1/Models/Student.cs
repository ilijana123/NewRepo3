using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Indeks { get; set; }
        [Required]
        [StringLength(100)]
        public string ImePrezime { get; set; }

        public ICollection<Prisustvo>? Casovi { get; set; }

    }
}
