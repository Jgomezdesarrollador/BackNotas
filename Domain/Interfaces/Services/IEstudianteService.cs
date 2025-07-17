using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IEstudianteService
    {
        Task<IEnumerable<Estudiante>> ListarAsync();
        Task<Estudiante?> ObtenerAsync(int id);
        Task CrearAsync(Estudiante estudiante);
        Task ActualizarAsync(Estudiante estudiante);
        Task EliminarAsync(int id);
    }
}
