using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IProfesorService
    {
        Task<IEnumerable<Profesor>> ListarAsync();
        Task<Profesor?> ObtenerAsync(int id);
        Task CrearAsync(Profesor profesor);
        Task ActualizarAsync(Profesor profesor);
        Task EliminarAsync(int id);
        Task<(IEnumerable<Profesor>, int)> ListarPaginadoAsync(int page, int size);
    }
}
