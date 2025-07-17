namespace Application.DTOs.Nota
{
    public class NotaUpdateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int IdEstudiante { get; set; }
        public int IdProfesor { get; set; }
        public decimal Valor { get; set; }
    }
}
