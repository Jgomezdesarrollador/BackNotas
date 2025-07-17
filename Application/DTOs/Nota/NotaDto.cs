namespace Application.DTOs.Nota
{
    public class NotaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Valor { get; set; }

        public int IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; } = string.Empty;

        public int IdProfesor { get; set; }
        public string NombreProfesor { get; set; } = string.Empty;
    }
}
