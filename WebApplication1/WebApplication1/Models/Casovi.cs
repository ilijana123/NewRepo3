using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Casovi
    {
        public int Id { get; set; }
        public int PredmetId { get; set; }
        public Predmet? Predmet { get; set; }
        public DateTime Datum { get; set; }

        public int BrojCasovi { get; set; }
        public int TipCasovi { get; set; }

        [StringLength(100)]
        public string Naslov { get; set; }
        [StringLength(255)]
        public string? Opis { get; set; }
        public ICollection<Prisustvo>? Studenti { get; set; }

    }
}
