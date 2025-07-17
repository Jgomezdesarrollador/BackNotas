namespace Domain.Entities
{
    public class LogError
    {
        public int Id { get; set; }
        public string Nivel { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public string? Detalles { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string? Origen { get; set; }
    }
}
