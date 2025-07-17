namespace Domain.Entities
{
    public class Estudiante
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public ICollection<Nota> Notas { get; set; } = [];
    }
}
