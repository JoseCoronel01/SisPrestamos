namespace SisPrestamos.Model
{
    public class Pago
    {
        public int Id { get; set; }
        public int CreditoId { get; set; }
        public decimal Importe { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
    }
}
