namespace Domain.Entities
{
    public class Profesor
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public ICollection<Nota> Notas { get; set; } = [];
    }
}
