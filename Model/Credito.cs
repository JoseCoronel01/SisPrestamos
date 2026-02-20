namespace SisPrestamos.Model
{
    public class Credito
    {
        public int Id { get; set; }
        public int LineaId { get; set; }
        public decimal Importe { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
    }
}
