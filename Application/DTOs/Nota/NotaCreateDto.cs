namespace Application.DTOs.Nota
{
    public class NotaCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int IdEstudiante { get; set; }
        public int IdProfesor { get; set; }
        public decimal Valor { get; set; }
    }
}
