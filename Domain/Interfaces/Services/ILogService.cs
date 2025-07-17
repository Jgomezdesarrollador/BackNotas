namespace Domain.Interfaces.Services
{
    public interface ILogService
    {
        Task RegistrarAsync(string nivel, string mensaje, string? detalles = null, string? origen = null);
    }
}
