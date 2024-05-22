namespace WebApplication1.Models
{
    public class Prisustvo
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CasoviId { get; set; }
        public Student? Student { get; set; }
        public Casovi? Casovi { get; set; }

    }

}