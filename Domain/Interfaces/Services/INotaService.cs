using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface INotaService
    {
        Task<IEnumerable<Nota>> ListarAsync();
        Task<Nota?> ObtenerAsync(int id);
        Task CrearAsync(Nota nota);
        Task ActualizarAsync(Nota nota);
        Task EliminarAsync(int id);
        Task<(IEnumerable<Nota>, int)> ListarPaginadoAsync(int page, int size);
    }
}
