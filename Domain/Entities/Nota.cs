namespace Domain.Entities
{
    public class Nota
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public int IdEstudiante { get; set; }
        public Estudiante Estudiante { get; set; } = null!;
        public int IdProfesor { get; set; }
        public Profesor Profesor { get; set; } = null!;
        public decimal Valor { get; set; }
    }
}
